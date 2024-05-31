namespace Components.Utilities
{
    public static class Utilities
    {

        public static bool ValidateDateTime(this DateTime dateTime)
        {
            //Date time must be after 1955
            if (dateTime.Year < Constants.minimumReleaseYear)
            {
                return false;
            }

            //Date time must be before now
            if (DateTime.Compare(dateTime, DateTime.Now.ToUniversalTime()) > 0)
            {
                return false;
            }
            return true;
        }
        
        public static bool ValidateDateOnly(this DateOnly date)
        {
            //Date time must be after 1955
            if (date.Year < Constants.minimumReleaseYear)
            {
                return false;
            }

            //Date time must be before now
            if (DateTime.Compare(date.ToDateTime(TimeOnly.Parse("12:00AM")), DateTime.Now.ToUniversalTime()) > 0)
            {
                return false;
            }
            return true;
        }
    }
}