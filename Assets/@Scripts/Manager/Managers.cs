public class Managers : SingletonBase<Managers>
{
    private readonly UIManager _uiManager = new();
    private readonly ToastManager _toastManager = new();
    private readonly DataManager _dataManager = new();
    private readonly MapManager _mapManager = new();
    
    public static UIManager UI => Instance._uiManager;
    public static ToastManager Toast => Instance._toastManager;
    public static DataManager Data => Instance._dataManager;
    public static MapManager Map => Instance._mapManager;
}