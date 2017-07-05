using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PakistanNews.Layouts.Detail
{
    public sealed partial class YouTubeDetailLayout : BaseDetailLayout
    {
        public YouTubeDetailLayout()
        {
            InitializeComponent();
        }

        private void WebView_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView webView = sender as WebView;
            if (webView != null) webView.NavigateToString(string.Empty);
        }
    }
}
