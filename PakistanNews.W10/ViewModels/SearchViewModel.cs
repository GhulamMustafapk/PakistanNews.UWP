using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PakistanNews.Sections;
namespace PakistanNews.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Pakistan News";
            DAWNNews = ViewModelFactory.NewList(new DAWNNewsSection());
            SAMAANews = ViewModelFactory.NewList(new SAMAANewsSection());
            BBCUrdu = ViewModelFactory.NewList(new BBCUrduSection());
            VoiceOfAmerica = ViewModelFactory.NewList(new VoiceOfAmericaSection());
            DunyaNews = ViewModelFactory.NewList(new DunyaNewsSection());
            ExpressTribune = ViewModelFactory.NewList(new ExpressTribuneSection());
            PakistanToday = ViewModelFactory.NewList(new PakistanTodaySection());
            BussinessRecorder = ViewModelFactory.NewList(new BussinessRecorderSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel DAWNNews { get; private set; }
        public ListViewModel SAMAANews { get; private set; }
        public ListViewModel BBCUrdu { get; private set; }
        public ListViewModel VoiceOfAmerica { get; private set; }
        public ListViewModel DunyaNews { get; private set; }
        public ListViewModel ExpressTribune { get; private set; }
        public ListViewModel PakistanToday { get; private set; }
        public ListViewModel BussinessRecorder { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return DAWNNews;
            yield return SAMAANews;
            yield return BBCUrdu;
            yield return VoiceOfAmerica;
            yield return DunyaNews;
            yield return ExpressTribune;
            yield return PakistanToday;
            yield return BussinessRecorder;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
