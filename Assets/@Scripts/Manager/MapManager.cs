using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private GameObject ground;
    private List<GameObject> mapGroundList = new List<GameObject>();
    
    public void Init()
    {
        ground = Resources.Load<GameObject>("ground");
    }

    public void CreateMap(int pStageNum)
    {
        MapData mapData = Managers.Data.GetMapData(pStageNum);
        int createCnt = (mapData.N + 1) * (mapData.M + 1) - mapGroundList.Count;
        for (int i = 0; i < createCnt; i++)
            mapGroundList.Add(Object.Instantiate(ground));
        mapGroundList.ForEach((x)=>x.SetActive(false));

        int gIdx = 0;
        for (int y = 1; y <= mapData.N; y++)
        {
            for (int x = 1; x <= mapData.M; x++)
            {
                GameObject groundObj = mapGroundList[gIdx];
                groundObj.gameObject.SetActive(true);
                groundObj.transform.position = new Vector3(x ,2f - y , 0);
                gIdx++;
            }
        }
        
    }
}
