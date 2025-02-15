using System.Collections.Generic;
using UnityEngine;

public class EaterManager
{
    private List<BlockData> _eaterData = null;
    private List<Eater> _eaterList = new();
    private Dictionary<Vector2Int, Eater> _eaterDictionary = new();
    
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
        
        _eaterDictionary.Add(new Vector2Int(data.row,data.col),eater);
        _eaterList.Add(eater);
    }

    public Eater GetEater(int row, int col)
    {
        Vector2Int key = new Vector2Int(row, col);
        if (_eaterDictionary.ContainsKey(key) == false)
        {
            Debug.LogError($"[EaterManager] GetEater() : {row},{col}에 해당하는 Eater가 없습니다.");
            return null;
        }
        return _eaterDictionary[key];
    }
}