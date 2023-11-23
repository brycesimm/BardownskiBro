namespace BardownskiBro.Models
{
    public class PlayerStats
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
        public int? playerId { get; set; }
        public string? headshot { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set;}
        //need to get from separate API endpoint
        public string? sweaterNumber {  get; set; } 
        //Forward, Defenseman, Goalie; get based on positionCode
        public string? position { get; set; }
        //L,R,C,D,G
        public string? positionCode { get; set; }
        public string? gamesPlayed { get; set; }
        public string? goals {  get; set; }
        public string? assists { get; set; }
        public string? points { get; set; }
        public string? plusMinus { get; set; }
        public string? penaltyMinutes { get; set; }
        public string? powerPlayGoals { get; set; }
        public string? shorthandedGoals { get; set; }
        public string? gameWinningGoals { get; set; }
        public string? overtimeGoals { get; set; }
        public string? shots { get; set; }
        public string? shootingPctg { get; set; }
        public string? avgTimeOnIcePerGame { get; set; }
        public string? avgShiftsPerGame { get; set; }
        public string? faceoffWinPctg { get; set; }
        //Goalie-specific stats
        public string? gamesStarted { get; set; }
        public string? wins { get; set; }
        public string? losses { get; set; }
        public string? ties { get; set; }
        public string? overtimeLosses { get; set; }
        public string? goalsAgainstAverage { get; set; }
        public string? savePercentage { get; set; }
        public string? shotsAgainst { get; set; }
        public string? saves { get; set; }
        public string? goalsAgainst { get; set; }
        public string? shutouts { get; set; }
        public string? timeOnIce { get; set; }
    }
}
