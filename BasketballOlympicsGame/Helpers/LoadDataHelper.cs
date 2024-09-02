using BasketballOlympicsGame.Models;
using Newtonsoft.Json;

namespace BasketballOlympicsGame.Utilities
{
    public static class LoadDataHelper
    {
        public static void LoadTeams(List<Group> groups)
        {
            string json = File.ReadAllText("Data\\groups.json");
            Dictionary<string, List<Team>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<Team>>>(json);

            foreach (KeyValuePair<string, List<Team>> keyValuePair in dictionary)
            {
                Group group = new Group
                {
                    Name = keyValuePair.Key,
                    Teams = keyValuePair.Value
                };

                groups.Add(group);
            }

            foreach (Group group in groups)
            {
                foreach (Team team in group.Teams)
                {
                    team.GroupName = group.Name;
                }
            }
        }

        public static Dictionary<string, List<MatchResult>> LoadExibitions()
        {
            string json = File.ReadAllText("Data\\exibitions.json");
            return JsonConvert.DeserializeObject<Dictionary<string, List<MatchResult>>>(json);
        }
    }
}
