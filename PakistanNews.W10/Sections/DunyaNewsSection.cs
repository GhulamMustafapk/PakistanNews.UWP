using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Rss;

using PakistanNews.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Sections
{
    public class DunyaNewsSection : Section<RssSchema>
    {
		private RssDataProvider _dataProvider;

		public DunyaNewsSection()
		{
			_dataProvider = new RssDataProvider();
		}

		public override async Task<IEnumerable<RssSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new RssDataConfig
            {
                Url = new Uri("http://dunyanews.tv/news.xml"),
				OrderBy = "PublishDate",
				OrderDirection = SortDirection.Descending
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<RssSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<RssSchema> ListPage
        {
            get
            {
                return new ListPageConfig<RssSchema>
                {
                    Title = " Dunya News",

                    Page = typeof(Pages.DunyaNewsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Header = item.PublishDate.ToString(DateTimeFormat.FullLongDate);
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                        viewModel.Footer = item.Author.ToSafeString();

                        viewModel.GroupBy = item.PublishDate.SafeType().Date;

                        viewModel.OrderBy = item.PublishDate;
                    },
                    OrderType = OrderType.Ascending,
                    DetailNavigation = (item) =>
                    {
                        return NavInfo.FromPage<Pages.DunyaNewsDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<RssSchema> DetailPage
        {
            get
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var bindings = new List<Action<ItemViewModel, RssSchema>>();
#pragma warning restore IDE0028 // Simplify collection initialization
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Dunya News";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<RssSchema>>
                {
                };

                return new DetailPageConfig<RssSchema>
                {
                    Title = " Dunya News",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
