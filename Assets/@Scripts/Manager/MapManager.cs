using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private GameObject mapGround;
    
    
    public void Init()
    {
        mapGround = Resources.Load<GameObject>("GroundPath");
        
    }

    public void CreateMap(int pStageNum)
    {
        MapData mapData = null;
        for (int y = 0; y < mapData.N; y++)
        {
            for (int x = 0; x < mapData.M; x++)
            {
                //Object.Instantiate()
            }
        }
        
    }
}
