//---------------------------------------------------------------------------
//
// <copyright file="SearchPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>6/13/2017 7:49:10 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Pages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            ViewModel = new SearchViewModel();
            this.InitializeComponent();
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }
        public SearchViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.SearchDataAsync(e.Parameter.ToString());
        }
    }    
}
