using System.Collections.Generic;
using UnityEngine;

public class ChocolateManager
{
    private List<BlockData> _chocolateData = null;
    private List<Chocolate> _chocolateList = new();
    
    public void Init()
    {
        var stageNum = Managers.Game.stageNum;
        var mapData = Managers.Data.GetMapData(stageNum);
        _chocolateData = Managers.Data.GetChocolateDatas(stageNum);
        _chocolateList.Clear();

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
