//---------------------------------------------------------------------------
//
// <copyright file="DunyaNewsDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/6/2017 5:33:38 AM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Rss;
using PakistanNews.Sections;
using PakistanNews.Navigation;
using PakistanNews.ViewModels;
using AppStudio.Uwp;

namespace PakistanNews.Pages
{
    public sealed partial class DunyaNewsDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public DunyaNewsDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new DunyaNewsSection());
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
