namespace MonkeyFinder.Model;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public class Familly
{
    public string name { get; set; }
    public string image { get; set; }
    public List<string> characters { get; set; }
}

