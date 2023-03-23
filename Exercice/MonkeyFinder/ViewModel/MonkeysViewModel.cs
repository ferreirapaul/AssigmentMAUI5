using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Familly> Familly { get; } = new();
    MonkeyService monkeyService;
    IConnectivity connectivity;
    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity)
    {
        Title = "Game Of Thrones Characters";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
    }
    
    [RelayCommand]
    async Task GoToDetails(Familly familly)
    {
        if (familly == null)
        return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Familly", familly },
        });
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();

            if(Familly.Count != 0)
                Familly.Clear();

            foreach(var fam in monkeys)
                Familly.Add(fam);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }

    [RelayCommand]
    async Task GetClosestMonkey()
    {
        if (IsBusy || Familly.Count == 0)
            return;

        try
        { 
            await Shell.Current.DisplayAlert("Error Location", "Westeros is in another universe than yours can't compute the distance", "OK");

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
