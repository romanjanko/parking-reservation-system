namespace ParkingSystem.Core.ReservationRules
{
    //TODO
    public abstract class ReservationValidationResult
    {
        public bool Valid { get; set; }
        public bool FreeReservation { get; set; }
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
            FreeReservation = true;
        }
    }
    
    public class SuccessfullGarageReservationBeforeLimitExpiration : ReservationValidationResult
    {

        public SuccessfullGarageReservationBeforeLimitExpiration()
        {
            Valid = true;
            FreeReservation = false;
        }
    }

    public class SuccessfullGarageReservationAfterLimitExpiration : ReservationValidationResult
    {

        public SuccessfullGarageReservationAfterLimitExpiration()
        {
            Valid = true;
            FreeReservation = true;
        }
    }
}
