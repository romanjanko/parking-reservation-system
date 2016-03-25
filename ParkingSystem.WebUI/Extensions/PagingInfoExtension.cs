using ParkingSystem.Core.Models;

namespace ParkingSystem.WebUI.Extensions
{
    public static class PagingInfoExtension
    {
        public static bool HidePagingNavigation(this PagingInfo pagingInfo)
        {
            return pagingInfo.TotalPages <= 1 && pagingInfo.CurrentPage == 1;
        }
    }
}