
public class GameManager
{
    public int score;
    public int limitTime;
    public int stageNum;

    public void Init()
    {
        score = 0;
        limitTime = 0;
        stageNum = 0;
    }

    public void CreateStage()
    {
        Managers.Map.CreateMap(stageNum);
        
        
        
    }
}
