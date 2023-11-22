using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BardownskiBro.HelperClasses
{
    public static class ConvertBDayToAge
    {
        public static int ConvertDateStringToAge(string BDay)
        {
            DateTime now = DateTime.Now;
            int age = 0;
            if (BDay != null)
            {
                try
                {
                    DateTime Bday = DateTime.Parse(BDay);
                    age = now.Year - Bday.Year;

                    if (Bday.AddYears(age) > now)
                    {
                        age--;
                    }

                    return age;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}
