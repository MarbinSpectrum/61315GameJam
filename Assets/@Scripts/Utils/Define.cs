public static class Define
{
    public enum ESceneType
    {
        MainScene,
    }
    
    public enum EUIEvent
    {
        PointerDown,
        PointUp,
        Click,
        BeginDrag,
        Drag,
        Drop,
        EndDrag,
        PointEnter,
        PointExit,
    }
    
    public enum ESoundType
    {
        BGM = 0,
        EFFECT = 1,
        MAX = 2,
    }
    
    public enum EHapticType
    {
        Light,
        Medium,
        Heavy
    }
    
    public enum EBlockType
    {
        Chocolate1 = 1, // 한칸짜리
        Chocolate2 = 2, // 세로 두칸짜리
        Chocolate3 = 3, // 가로 두칸짜리
        Chocolate4 = 4, // 4칸 정사각형 블록
    
        Eater = 10, // 이터
    }

    public enum EColor
    {
        Brown = 1,
        White = 2,
        Orange = 3,
        Green = 4,
        Yellow = 5,
        Black = 6,
    }

    public enum EDirection
    {
        //(단, 초콜릿은 상=하, 좌=우 임)
    
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }

    public enum ETagType
    {
        Chocolate,
        Eater,
    }
    
    public enum EEaterState
    {
        Idle,
        Eat,
        Angry,
        Nice,
    }

    public enum EDataTableType
    {
        block_info,
        stage_info,
    }
}
