//---------------------------------------------------------------------------
//
// <copyright file="VoiceOfAmericaListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/6/2017 5:33:38 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using PakistanNews.Sections;
using PakistanNews.ViewModels;
using AppStudio.Uwp;

namespace PakistanNews.Pages
{
    public sealed partial class VoiceOfAmericaListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public VoiceOfAmericaListPage()
        {
			ViewModel = ViewModelFactory.NewList(new VoiceOfAmericaSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("d11ec815-b211-476e-9a30-cd685f99e4ba");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
