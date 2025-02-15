using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ChocolateManager
{
    private List<BlockData> _chocolateData = null;
    private List<Chocolate> _chocolateList = new();
    
    private ChocolateMelt meltEffect;
    private Queue<ChocolateMelt> meltQueue = new();
    private List<ChocolateMelt> meltList = new();
    
    public void Init()
    {
        meltEffect = Resources.Load<ChocolateMelt>("ChocolateMelted");
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
    
    public void DestroyChocolate(Chocolate chocolate)
    {
        _chocolateList.Remove(chocolate);
        Object.Destroy(chocolate.gameObject);
    }

    public void OnMeltingChocolates()
    {
        foreach (var chocolate in _chocolateList)
            chocolate.OnMelting();
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

    public void SpawnMelt(Vector3 pPos,BlockData pBlockData)
    {
        //pPos위치에 pBlockData에 해당하는 이펙트 출력
        ChocolateMelt newMelt = null;
        if (meltQueue.Count > 0)
        {
            //큐에 있는 이펙트만 사용
            newMelt = meltQueue.Dequeue();
        }

        if (newMelt == null)
        {
            //이펙트 추가생성
            newMelt = Object.Instantiate(meltEffect);
            meltList.Add(newMelt);
        }
        
        //이펙트 회전
        if (pBlockData.dir == Define.EDirection.Right)
            newMelt.transform.rotation = Quaternion.Euler(0, 0, -90);
        else if (pBlockData.dir == Define.EDirection.Left)
            newMelt.transform.rotation = Quaternion.Euler(0, 0, +90);
        else if (pBlockData.dir == Define.EDirection.Up)
            newMelt.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (pBlockData.dir == Define.EDirection.Down)
            newMelt.transform.rotation = Quaternion.Euler(0, 0, 180);
        
        
        newMelt.gameObject.SetActive(true);
        newMelt.SetMelt(pBlockData.color);
        newMelt.transform.position = pPos;
    }

    public void RemoveMelt(ChocolateMelt melt)
    {
        //이펙트 제거
        melt.gameObject.SetActive(false);
        meltQueue.Enqueue(melt);
    }

    public void ClearMelt()
    {
        //이펙트 리스트 비우기
        meltQueue.Clear();
        foreach (var melt in meltList)
            RemoveMelt(melt);
        meltList.Clear();
    }
    
    
}
