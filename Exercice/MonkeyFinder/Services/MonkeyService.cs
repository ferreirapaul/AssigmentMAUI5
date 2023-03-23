using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }

    List<Familly> famillyList;
    public async Task<List<Familly>> GetMonkeys()
    {
        if (famillyList?.Count > 0)
            return famillyList;

        // Online
        /*
        var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        if (response.IsSuccessStatusCode)
        {
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }*/
        // Offline
        using var stream = await FileSystem.OpenAppPackageFileAsync("got.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        famillyList = JsonSerializer.Deserialize<List<Familly>>(contents);
        
        return famillyList;
    }
}
