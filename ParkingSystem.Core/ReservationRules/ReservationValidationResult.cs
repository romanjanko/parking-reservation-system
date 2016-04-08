namespace ParkingSystem.Core.ReservationRules
{
    public abstract class ReservationValidationResult
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FailedReservation : ReservationValidationResult
    {
        public FailedReservation(string error)
        {
            Valid = false;
            ErrorMessage = error;
        }
    }

    public class SuccessfullCommonReservation : ReservationValidationResult
    {
        public SuccessfullCommonReservation()
        {
            Valid = true;
        }
    }
    
    public class SuccessfullNonFreeGarageReservation : ReservationValidationResult
    {

        public SuccessfullNonFreeGarageReservation()
        {
            Valid = true;
        }
    }

    public class SuccessfullFreeGarageReservation : ReservationValidationResult
    {

        public SuccessfullFreeGarageReservation()
        {
            Valid = true;
        }
    }
}
