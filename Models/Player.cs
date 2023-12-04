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
        public int? id { get; set; }
        public string? headshot { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set;}
        public string? sweaterNumber {  get; set; } 
        //Forward, Defenseman, Goalie
        public string? position { get; set; }
        //L,R,C,D,G
        public string? positionCode { get; set; }
        public string? shootsCatches { get; set; }
        public string? heightInInches { get; set; }
        public string? weightInPounds { get; set; }
        public string? birthDate { get; set; }
        public int? age { get; set; }
        //public int ageInYears()
        //{
        //    DateTime now = DateTime.Now;
        //    int age = 0;
        //    if (birthDate != null)
        //    {
        //        try
        //        {
        //            DateTime Bday = DateTime.Parse(birthDate);
        //            age = now.Year - Bday.Year;

        //            if(Bday.AddYears(age) > now)
        //            {
        //                age--;
        //            }

        //            return age;
        //        }
        //        catch(Exception e) 
        //        {
        //            return -1;
        //        }
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}
        public string? birthCity { get; set; }
        public string? birthStateProvince { get; set; }
        public string? birthCountry { get; set; }

    }
}
