using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingSystem.Core.ReservationRules
{
    public abstract class ReservationValidationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SuccessfullReservationValidationResult : ReservationValidationResult
    {
        public SuccessfullReservationValidationResult()
        {
            Success = true;
        }
    }

    public class FailedReservationValidationResult : ReservationValidationResult
    {
        public FailedReservationValidationResult(string error)
        {
            Success = false;
            ErrorMessage = error;
        }
    }
}
