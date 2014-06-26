using System.Collections.Generic;

namespace elp87.Finance.Filter
{
    abstract public class FilterSet
    {
        protected List<FilterDay> _filterDayList;
        protected string _filterName;
        protected Dictionary<string, double> _params;

        public FilterSet()
        {
            _filterDayList = new List<FilterDay>();
            _params = new Dictionary<string, double>();
        }

        public List<FilterDay> FilterDayList
        {
            get { return _filterDayList; }
            set { _filterDayList = value; }
        }

        public string FilterName
        {
            get { return _filterName; }
            set { _filterName = value; }
        }

        public Dictionary<string, double> FilterParametrs
        {
            get { return _params; }
            set { _params = value; }
        }
    }
}
