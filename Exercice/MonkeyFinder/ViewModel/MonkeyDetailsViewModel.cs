namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Familly), "Familly")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    public MonkeyDetailsViewModel()
    { 
    }

    [ObservableProperty]
    Familly familly;

}
