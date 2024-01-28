using Newtonsoft.Json;
using NUnit.Framework;

namespace CompetitionTaskMars.Data
{
    public class EducationDataHelper
    {
        public static List<EducationData> ReadEducationData(string jsonFileName)
        {
            string currentDirectory = TestContext.CurrentContext.TestDirectory;
            string filePath = Path.Combine(currentDirectory, "Data", jsonFileName);
            string jsonContent = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<EducationData>>(jsonContent) ?? new List<EducationData>();
        }
    }
}
        
