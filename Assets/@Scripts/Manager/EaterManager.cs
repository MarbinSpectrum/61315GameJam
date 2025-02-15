using System.Collections.Generic;
using UnityEngine;

public class EaterManager
{
    private List<BlockData> _eaterData = null;
    private List<Eater> _eaterList = new();
    private Dictionary<int, Eater> _eaterDictionary = new();
    
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
        _eaterDictionary.Clear();
        
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
        
        _eaterDictionary.Add(data.idx ,eater);
        _eaterList.Add(eater);
    }

    public Eater GetEater(int pIdx)
    {
        if (_eaterDictionary.ContainsKey(pIdx) == false)
        {
            Debug.LogError($"[EaterManager] GetEater() : {pIdx}에 해당하는 Eater가 없습니다.");
            return null;
        }
        return _eaterDictionary[pIdx];
    }
}