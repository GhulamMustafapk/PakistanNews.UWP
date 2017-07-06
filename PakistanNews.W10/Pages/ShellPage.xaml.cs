using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using PakistanNews.Navigation;

namespace PakistanNews.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"21bf126d-b4ed-4c44-8cae-b16ed03c3824",
                "DAWN News",                
                AppNavigation.ActionFromPage("DAWNNewsListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/dawn_news-600x400.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"fd516ed9-aea6-494d-ad4b-7984b00771f4",
                "SAMAA News",                
                AppNavigation.ActionFromPage("SAMAANewsListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/samaa-urdu-news-tv.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"28f0b72e-77b1-4495-9298-aaa9fcad8cf7",
                "BBC Urdu",                
                AppNavigation.ActionFromPage("BBCUrduListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/BBC-urdu2.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"d11ec815-b211-476e-9a30-cd685f99e4ba",
                "Voice of America",                
                AppNavigation.ActionFromPage("VoiceOfAmericaListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/gov.bbg.voa.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"c7d1ed33-8c13-4c5c-b89c-3be8a8ecb49a",
                " Dunya News",                
                AppNavigation.ActionFromPage("DunyaNewsListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/unnamed.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"5396767e-72a3-4847-b538-b9ea3a5a38ec",
                "Express Tribune",                
                AppNavigation.ActionFromPage("ExpressTribuneListPage"),
				AppNavigation.IconFromGlyph("\ue12a")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"a1d872c7-34a7-4f74-a3be-779ec386a9ff",
                "Pakistan Today",                
                AppNavigation.ActionFromPage("PakistanTodayListPage"),
				AppNavigation.IconFromGlyph("\ue12a")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"817f4f95-eb36-42c3-9468-e2f2b8643b78",
                "Bussiness Recorder",                
                AppNavigation.ActionFromPage("BussinessRecorderListPage"),
				AppNavigation.IconFromGlyph("\ue12a")));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
