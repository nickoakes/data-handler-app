using System.Collections.Generic;

namespace data_handler_app.HubSpotModels
{
    public class ClientPersona
    {
        public Dictionary<string, int> Brackets { get; set; }
        public ClientPersona(List<EngagementsDTO> allEngagements)
        {
            Dictionary<string, int> personaData = new Dictionary<string, int>();

            personaData.Add("05:00 - 08:59", 0);
            personaData.Add("09:00 - 11:59", 0);
            personaData.Add("12:00 - 14:59", 0);
            personaData.Add("15:00 - 17:59", 0);
            personaData.Add("18:00 - 20:59", 0);
            personaData.Add("21:00 - 04:49", 0);

            for (int i = 0; i < allEngagements.Count; i++)
            {
                int hour = allEngagements[i].LastUpdatedAt.Hour;

                if (string.Equals(allEngagements[i].Direction, "Inbound"))
                {
                    if (hour >= 5 && hour < 9)
                    {
                        personaData["05:00 - 08:59"] += 1;
                    }
                    else if (hour >= 9 && hour < 12)
                    {
                        personaData["09:00 - 11:59"] += 1;
                    }
                    else if (hour >= 12 && hour < 15)
                    {
                        personaData["12:00 - 14:59"] += 1;
                    }
                    else if (hour >= 15 && hour < 18)
                    {
                        personaData["15:00 - 17:59"] += 1;
                    }
                    else if (hour >= 18 && hour < 21)
                    {
                        personaData["18:00 - 20:59"] += 1;
                    }
                    else
                    {
                        personaData["21:00 - 04:49"] += 1;
                    }
                }
            }

            Brackets = personaData;
        }
    }

    public class Bracket
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
        public Bracket(string name, int frequency)
        {
            Name = name;
            Frequency = frequency;
        }
    }
}