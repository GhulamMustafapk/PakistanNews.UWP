using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Rss;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using PakistanNews.Navigation;
using PakistanNews.ViewModels;

namespace PakistanNews.Sections
{
    public class SAMAANewsSection : Section<RssSchema>
    {
		private RssDataProvider _dataProvider;

		public SAMAANewsSection()
		{
			_dataProvider = new RssDataProvider();
		}

		public override async Task<IEnumerable<RssSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new RssDataConfig
            {
                Url = new Uri("http://www.samaa.tv/feed/"),
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
                    Title = "SAMAA News",

                    Page = typeof(Pages.SAMAANewsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.SAMAANewsDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<RssSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, RssSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<RssSchema>>
                {
                };

                return new DetailPageConfig<RssSchema>
                {
                    Title = "SAMAA News",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
