using System.Collections.Generic;
using UnityEngine;

public class EaterManager
{
    private Dictionary<int, Eater> _eaterDict = new();
    
    public void Init()
    {
        
    }
    
    public void SetEaters(int stageNum)
    {
        foreach (var eater in _eaterDict.Values)
        {
            Object.Destroy(eater.gameObject);
        }
        _eaterDict.Clear();
        
        var mapData = Managers.Data.GetMapData(stageNum);
        var eaterData = Managers.Data.GetEaterDatas(stageNum);
        foreach (var data in eaterData)
        {
            InstantiateEater(data, mapData.N, mapData.M);
        }
    }
    
    private void InstantiateEater(BlockData data, int n, int m)
    {
        var objectEater = Resources.Load<GameObject>(data.blockType.ToString());
        var eater = Object.Instantiate(objectEater).GetComponent<Eater>();
        eater.Init(data, n, m);
        
        _eaterDict.Add(data.idx ,eater);
    }

    private Eater GetEater(int pIdx)
    {
        if (!_eaterDict.TryGetValue(pIdx, out var eater))
        {
            Debug.LogError($"[EaterManager] GetEater() : {pIdx}에 해당하는 Eater가 없습니다.");
            return null;
        }
        return eater;
    }
}