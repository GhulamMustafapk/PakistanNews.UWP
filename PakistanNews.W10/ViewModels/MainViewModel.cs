using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using PakistanNews.Sections;


namespace PakistanNews.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel DAWNNews { get; private set; }
        public ListViewModel SAMAANews { get; private set; }
        public ListViewModel BBCUrdu { get; private set; }
        public ListViewModel VoiceOfAmerica { get; private set; }
        public ListViewModel DunyaNews { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Pakistan News";
            DAWNNews = ViewModelFactory.NewList(new DAWNNewsSection(), visibleItems);
            SAMAANews = ViewModelFactory.NewList(new SAMAANewsSection(), visibleItems);
            BBCUrdu = ViewModelFactory.NewList(new BBCUrduSection(), visibleItems);
            VoiceOfAmerica = ViewModelFactory.NewList(new VoiceOfAmericaSection(), visibleItems);
            DunyaNews = ViewModelFactory.NewList(new DunyaNewsSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return DAWNNews;
            yield return SAMAANews;
            yield return BBCUrdu;
            yield return VoiceOfAmerica;
            yield return DunyaNews;
        }
    }
}
