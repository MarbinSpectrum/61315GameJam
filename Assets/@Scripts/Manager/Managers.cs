public class Managers : SingletonBase<Managers>
{
    private readonly UIManager _uiManager = new();
    private readonly ToastManager _toastManager = new();
    
    public static UIManager UI => Instance._uiManager;
    public static ToastManager Toast => Instance._toastManager;
}