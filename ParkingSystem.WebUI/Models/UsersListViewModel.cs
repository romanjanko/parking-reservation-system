using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.WebUI.Models
{
    public class UsersListViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}