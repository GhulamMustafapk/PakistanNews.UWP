//---------------------------------------------------------------------------
//
// <copyright file="DAWNNewsDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>6/13/2017 7:49:10 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PakistanNews.Sections;
using PakistanNews.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Pages
{
    public sealed partial class DAWNNewsDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public DAWNNewsDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new DAWNNewsSection());
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
