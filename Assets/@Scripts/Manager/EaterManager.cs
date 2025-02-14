using System.Collections.Generic;
using UnityEngine;

public class EaterManager
{
    private List<BlockData> _eaterData = null;
    private List<Eater> _eaterList = new();
    
    public void Init()
    {
        
    }
    
    public void SetEaters(int stageNum)
    {
        foreach (var chocolate in _eaterList)
        {
            Object.Destroy(chocolate.gameObject);
        }
        _eaterList.Clear();
        
        var mapData = Managers.Data.GetMapData(stageNum);
        _eaterData = Managers.Data.GetEaterDatas(stageNum);
        foreach (var data in _eaterData)
        {
            InstantiateChocolate(data, mapData.N, mapData.M);
        }
    }
    
    private void InstantiateChocolate(BlockData data, int n, int m)
    {
        var objectChocolate = Resources.Load<GameObject>(data.blockType.ToString());
        var eater = Object.Instantiate(objectChocolate).GetComponent<Eater>();
        eater.Init(data,n,m);
        
        _eaterList.Add(eater);
    }
}