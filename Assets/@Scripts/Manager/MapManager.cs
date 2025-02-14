using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private GameObject ground;
    private List<GameObject> mapGroundList = new List<GameObject>();
    
    public void Init()
    {
        ground = Resources.Load<GameObject>("GroundPath");
    }

    public void CreateMap(int pStageNum)
    {
        MapData mapData = null;
        int createCnt = (mapData.N + 1) * (mapData.M + 1) - mapGroundList.Count;
        for (int i = 0; i < createCnt; i++)
            mapGroundList.Add(Object.Instantiate(ground));
        mapGroundList.ForEach((x)=>x.SetActive(false));

        int gIdx = 0;
        for (int y = 0; y < mapData.N; y++)
        {
            for (int x = 0; x < mapData.M; x++)
            {
                mapGroundList[gIdx].gameObject.SetActive(true);
                gIdx++;
            }
        }
        
    }
}
