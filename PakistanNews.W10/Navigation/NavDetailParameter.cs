using AppStudio.DataProviders;
using System.Collections.Generic;

namespace PakistanNews.Navigation
{
    public class NavDetailParameter
    {
        public IEnumerable<SchemaBase> Items { get; set; }
        public string SelectedId { get; set; }
    }
}