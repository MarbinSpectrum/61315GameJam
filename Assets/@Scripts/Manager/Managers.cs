public class Managers : SingletonBase<Managers>
{
    private readonly UIManager _uiManager = new();
    private readonly ToastManager _toastManager = new();
    private readonly DataManager _dataManager = new();
    private readonly ChocolateManager _chocolateManager = new();
    private readonly EaterManager _eaterManager = new();
    private readonly MapManager _mapManager = new();
    private readonly GameManager _gameManager = new();
    private readonly SoundManager _soundManager = new();
    
    public static UIManager UI => Instance._uiManager;
    public static ToastManager Toast => Instance._toastManager;
    public static DataManager Data => Instance._dataManager;
    public static ChocolateManager Chocolate => Instance._chocolateManager;
    public static EaterManager Eater => Instance._eaterManager;
    public static MapManager Map => Instance._mapManager;
    public static GameManager Game => Instance._gameManager;
    public static SoundManager Sound => Instance._soundManager;
}