using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            ViewModel = new MainViewModel(12);            
            InitializeComponent();
            AdDuplex.AdDuplexClient.Initialize("41d29dec-906d-4859-9ad3-f81c71e4bdc3");
            NavigationCacheMode = NavigationCacheMode.Required;
			commandBar.DataContext = ViewModel;
			searchBox.SearchCommand = SearchCommand;
			this.SizeChanged += OnSizeChanged;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }		
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
			//Page cache requires set commandBar in code
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
            ShellPage.Current.ShellControl.SelectItem("Home");
        }

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            searchBox.SearchWidth = e.NewSize.Width > 640 ? 230 : 190;
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(text =>
                {
                    searchBox.Reset();
                    ShellPage.Current.ShellControl.CloseLeftPane();                    
                    NavigationService.NavigateToPage("SearchPage", text, true);
                },
                SearchViewModel.CanSearch);
                
            }
        }
        
    }
}
