namespace MvcSolution
{
    public class PageRequest
    {
        private int _pageSize;

        public int PageIndex { get; set; }

        public int PageSize
        {
            get { return _pageSize ; }
            set
            {
                if (value > 50)
                {
                    _pageSize = 50;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }

        public string Sort { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
