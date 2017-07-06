//---------------------------------------------------------------------------
//
// <copyright file="AboutPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/6/2017 5:33:38 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Pages
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            AboutThisAppModel = new AboutThisAppViewModel();

            this.InitializeComponent();
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        public AboutThisAppViewModel AboutThisAppModel { get; private set; }
		
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ShellPage.Current.ShellControl.SelectItem("About");
            base.OnNavigatedTo(e);
        }
    }
}
