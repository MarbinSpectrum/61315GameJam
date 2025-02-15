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
        
        _chocolateData = Managers.Data.GetChocolateDatas(stageNum);
        foreach (var data in _chocolateData)
        {
            InstantiateChocolate(data);
        }
    }
    
    private void InstantiateChocolate(BlockData data)
    {
        var objectChocolate = Resources.Load<GameObject>(data.blockType.ToString());
        if (objectChocolate == null)
        {
            Debug.LogError($"[ChocolateManager] InstantiateChocolate : {data.blockType} Prefab not exists");
            return;
        }
        var chocolate = Object.Instantiate(objectChocolate).GetComponent<Chocolate>();
        chocolate.Init(data);
        _chocolateList.Add(chocolate);
    }
}
