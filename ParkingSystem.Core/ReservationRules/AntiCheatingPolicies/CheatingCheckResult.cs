namespace ParkingSystem.Core.ReservationRules.AntiCheatingPolicies
{
    public abstract class CheatingCheckResult
    {
        public bool CheatingDetected { get; set; }
    }

    public class NoCheatingDetected : CheatingCheckResult
    {
        public NoCheatingDetected()
        {
            CheatingDetected = false;
        }
    }

    public class PossibleCheatingDetected : CheatingCheckResult
    {
        public PossibleCheatingDetected()
        {
            CheatingDetected = true;
        }
    }
}
