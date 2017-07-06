//---------------------------------------------------------------------------
//
// <copyright file="SAMAANewsListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class SAMAANewsListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public SAMAANewsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new SAMAANewsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("fd516ed9-aea6-494d-ad4b-7984b00771f4");
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
