using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Rss;

using PakistanNews.Navigation;
using PakistanNews.ViewModels;
using System.Collections;

namespace PakistanNews.Sections
{
    public class ExpressNewsSection : Section<RssSchema>
    {
		private RssDataProvider _dataProvider;
        private List<ActionConfig<RssSchema>> actions;

        public ExpressNewsSection()
		{
			_dataProvider = new RssDataProvider();
		}

		public override async Task<IEnumerable<RssSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new RssDataConfig
            {
                Url = new Uri("https://tribune.com.pk/pakistan/feed/"),
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
                    Title = "Express News",

                    Page = typeof(Pages.ExpressNewsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavInfo.FromPage<Pages.ExpressNewsDetailPage>(true);
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
                    viewModel.PageTitle = item.Author.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

               ;

                return new DetailPageConfig<RssSchema>
                {
                    Title = "Express News",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
