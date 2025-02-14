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
        Block1x1 = 1,//한칸짜리
        Block1x2 = 2,//세로 두칸짜리
        Blcok2x1 = 3,//가로 두칸짜리
        block2x2 = 4,//4칸 정사각형 블록
    
        Eater = 10, //블록 먹는애
    }

    public enum EColor
    {
        Red = 1,
        Blue = 2,
    }

    public enum EDirection
    {
        //(단, 초콜릿은 상=하, 좌=우 임)
    
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }
}
