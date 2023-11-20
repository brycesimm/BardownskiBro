namespace BardownskiBro.Models
{
    public class Player
    {
        /*
         * {
            "id": 8476826,
            "headshot": "https://assets.nhle.com/mugs/nhl/20232024/SEA/8476826.png",
            "firstName": {
                "default": "Yanni"
            },
            "lastName": {
                "default": "Gourde"
            },
            "sweaterNumber": 37,
            "positionCode": "C",
            "shootsCatches": "L",
            "heightInInches": 69,
            "weightInPounds": 174,
            "heightInCentimeters": 175,
            "weightInKilograms": 79,
            "birthDate": "1991-12-15",
            "birthCity": {
                "default": "Saint-Narcisse"
            },
            "birthCountry": "CAN",
            "birthStateProvince": {
                "default": "QC"
            }
        }
        */
        public int Id { get; set; }
        public string? Headshot { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Number {  get; set; } 
        //Forward, Defenseman, Goalie
        public string? Position { get; set; }
        //L,R,C,D,G
        public string? PositionCode { get; set; }
        public string? DominantHand { get; set; }
        public string? HeightInches { get; set; }
        public string? weightPounds { get; set; }
        public string? Birthday { get; set; }
        public int AgeInYears()
        {
            DateTime now = DateTime.Now;
            int age = 0;
            if (Birthday != null)
            {
                try
                {
                    DateTime Bday = DateTime.Parse(Birthday);
                    age = now.Year - Bday.Year;

                    if(Bday.AddYears(age) > now)
                    {
                        age--;
                    }

                    return age;
                }
                catch(Exception e) 
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        public string? BirthCity { get; set; }
        public string? BirthStateProvince { get; set; }
        public string? BirthCountry { get; set; }

    }
}
