using ParkingSystem.Core.Models;
using ParkingSystem.Core.Services;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Models;
using System.Web.Mvc;

namespace ParkingSystem.WebUI.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ParkingSpotsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;
        private readonly int pageSize = 8;

        public ParkingSpotsController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        public ActionResult Index(int page = 1)
        {
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize };
            var parkingSpots = _parkingSpotService.GetParkingSpots(pagingInfo);

            return View(
                new ParkingSpotsListViewModel
                {
                    ParkingSpots = parkingSpots.CurrentParkingSpots,
                    PagingInfo = parkingSpots.PagingInfo
                });
        }

        public ActionResult AddParkingSpot()
        {
            return PartialView("_AddParkingSpot", new ParkingSpot());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddParkingSpot(ParkingSpot parkingSpot)
        {
            if (ModelState.IsValid)
            {
                if (_parkingSpotService.TestIfParkingSpotWithSameNameAlreadyExists(parkingSpot))
                {
                    ModelState.AddModelError("", "Parking spot with the same name already exists.");
                }
                else
                {
                    _parkingSpotService.AddParkingSpot(parkingSpot);
                    TempData["messageSuccess"] = string.Format("The new parking spot {0} has been successfully created.", 
                        parkingSpot.Name);

                    return Json(new { success = true });
                }
            }

            return PartialView("_AddParkingSpot");
        }

        public ActionResult EditParkingSpot(int id)
        {
            var parkingSpot = _parkingSpotService.GetParkingSpot(id);
            
            if (parkingSpot == null)
                return HttpNotFound();

            var model = new ParkingSpotEditViewModel
            {
                ParkingSpot = parkingSpot,
                ParkingTypeId = (int) parkingSpot.Type,
                ParkingTypes = new SelectList(
                    new[]
                    {
                        new {Value = 0, Text = "Garage"},
                        new {Value = 1, Text = "Outside"}
                    }, "Value", "Text")
            };

            return PartialView("_EditParkingSpot", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditParkingSpot(ParkingSpotEditViewModel model)
        {
            //TODO - workaround for an issue with DropDownList selection
            model.ParkingSpot.Type = (ParkingSpotType)model.ParkingTypeId;
            model.ParkingTypes = new SelectList(
                new[]
                {
                    new {Value = 0, Text = "Garage"},
                    new {Value = 1, Text = "Outside"}
                }, "Value", "Text");

            if (_parkingSpotService.TestIfParkingSpotWithSameNameAlreadyExists(model.ParkingSpot))
            {
                ModelState.AddModelError("", "Parking spot with the same name already exists.");
            }
            else
            { 
                _parkingSpotService.UpdateParkingSpot(model.ParkingSpot);
                TempData["messageSuccess"] = string.Format("The parking spot {0} has been successfully updated.",
                    model.ParkingSpot.Name);

                return Json(new { success = true });
            }

            return PartialView("_EditParkingSpot", model);
        }

        public ActionResult DeleteParkingSpot(int id)
        {
            var parkingSpot = _parkingSpotService.GetParkingSpot(id);

            if (parkingSpot == null)
                return HttpNotFound();

            return PartialView("_DeleteParkingSpot", parkingSpot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteParkingSpot(ParkingSpot parkingSpot)
        {
            var parkingSpotToDelete = _parkingSpotService.GetParkingSpot(parkingSpot.Id);

            if (parkingSpotToDelete == null)
                return HttpNotFound();

            TempData["messageSuccess"] = string.Format("The parking spot {0} has been successfully deleted.", 
                parkingSpotToDelete.Name);
            _parkingSpotService.DeleteParkingSpot(parkingSpotToDelete.Id);

            return Json(new { success = true });
        }
    }
}
