namespace InternetHospital.BusinessLogic.Models.Notification
{
    public class NotificationSearchModel
    {
        private const int DEFAULT_PAGE = 1;
        private const int MAX_PAGE_COUNT = 15;
        public int Page { get; set; } = DEFAULT_PAGE;
        private int _pageCount = MAX_PAGE_COUNT;
        public int PageCount
        {
            get => _pageCount;
            set => _pageCount = (value > MAX_PAGE_COUNT) ? MAX_PAGE_COUNT : value;
        }
    }
}
