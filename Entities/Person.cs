namespace Detailing
{
    ///Base clasee
    public abstract class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PreferredName { get; set; }

        public DateTime DOB { get; set; }

        public string MobileNumber { get; set; }
    }
}