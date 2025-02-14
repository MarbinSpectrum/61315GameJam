using System.Collections.Generic;
using UnityEngine;

public class ChocolateManager
{
    private List<BlockData> _chocolateData = null;
    private List<Chocolate> _chocolateList = new();
    
    public void Init()
    {
        
    }
    
    public void SetChocolates(int stageNum)
    {
        foreach (var chocolate in _chocolateList)
        {
            Object.Destroy(chocolate.gameObject);
        }
        _chocolateList.Clear();
        
        var mapData = Managers.Data.GetMapData(stageNum);
        _chocolateData = Managers.Data.GetChocolateDatas(stageNum);
        foreach (var data in _chocolateData)
        {
            InstantiateChocolate(data, mapData.N, mapData.M);
        }
    }
    
    private void InstantiateChocolate(BlockData data, int n, int m)
    {
        var objectChocolate = Resources.Load<GameObject>(data.blockType.ToString());
        var chocolate = Object.Instantiate(objectChocolate).GetComponent<Chocolate>();
        chocolate.Init(data, n, m);
        _chocolateList.Add(chocolate);
    }
}
