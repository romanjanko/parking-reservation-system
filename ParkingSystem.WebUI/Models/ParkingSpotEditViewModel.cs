using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.WebUI.Models
{
    public class ParkingSpotEditViewModel
    {
        public ParkingSpot ParkingSpot { get; set; }
        public int ParkingTypeId { get; set; }
        public IEnumerable<SelectListItem> ParkingTypes { get; set; }
    }
}