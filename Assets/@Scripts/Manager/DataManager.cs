using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DataManager
{ 
    private Dictionary<int,List<BlockData>> chocolateBlocks;
    private Dictionary<int,List<BlockData>> eaterBlocks;
    private Dictionary<int,MapData> mapData;
    
    public void Init()
    {
        chocolateBlocks ??= new Dictionary<int,List<BlockData>>();
        eaterBlocks ??= new Dictionary<int,List<BlockData>>();
        mapData ??= new Dictionary<int,MapData>();
        
        LoadBlockDatas(EDataTableType.block_info.ToString());
        LoadMapDatas(EDataTableType.stage_info.ToString());
    }
    
    private void LoadBlockDatas(string pFilePath)
    {
        //스테이지번호, 블록리스트
        
        //,자 형식으로 저장된 csv파일을 읽는다.
        TextAsset textAsset = Resources.Load<TextAsset>(pFilePath);
        if (textAsset == null)
            return;
        
        chocolateBlocks.Clear();
        eaterBlocks.Clear();
        
        //줄을 나눈다.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //아무것도 없는 객체
                continue;
            }

            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(row);
        }

        for (int r = 1; r < rowList.Count; r++)
        {
            //해당 줄부터 데이터다.
            string[] values = rowList[r].Split("\t");
            
            for (int c = 0; c < values.Length; c++)
                values[c] = values[c].Replace('\r', ' ').Trim();
            
            int stageNum = int.Parse(values[0]);
            int idx = int.Parse(values[1]);
            int row = int.Parse(values[2]);
            int col = int.Parse(values[3]);
            int unitType = int.Parse(values[4]);
            int blockColor = int.Parse(values[5]);
            int blockDir = int.Parse(values[6]);
            
            if(chocolateBlocks.ContainsKey(stageNum) == false)
                chocolateBlocks.Add(stageNum, new List<BlockData>());
            if(eaterBlocks.ContainsKey(stageNum) == false)
                eaterBlocks.Add(stageNum, new List<BlockData>());
            
            if(IsEaterBlock((EBlockType)unitType))
                eaterBlocks[stageNum].Add(new BlockData(idx,row,col,unitType,blockColor,blockDir));
            else
                chocolateBlocks[stageNum].Add(new BlockData(idx,row,col,unitType,blockColor,blockDir));
        }
    }

    private bool IsEaterBlock(EBlockType pEBlockType)
    {
        switch (pEBlockType)
        {
            case EBlockType.Eater1:
            case EBlockType.Eater2:
            return true;
        }

        return false;
    }
    
    private void LoadMapDatas(string pFilePath)
    {
        //스테이지번호, 맵 정보
        
        //,자 형식으로 저장된 csv파일을 읽는다.
        TextAsset textAsset = Resources.Load<TextAsset>(pFilePath);
        if (textAsset == null)
            return;

        mapData.Clear();
        
        //줄을 나눈다.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //아무것도 없는 객체
                continue;
            }

            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(row);
        }

        for (int r = 1; r < rowList.Count; r++)
        {
            //해당 줄부터 데이터다.
            string[] values = rowList[r].Split("\t");
            
            for (int c = 0; c < values.Length; c++)
                values[c] = values[c].Replace('\r', ' ').Trim();
            
            int stageNum = int.Parse(values[0]);
            int n = int.Parse(values[1]);
            int m = int.Parse(values[2]);
            int limitTime = int.Parse(values[3]);
            
            if(mapData.ContainsKey(stageNum) == false)
                mapData.Add(stageNum, new MapData(n,m,limitTime));

        }
    }

    public List<BlockData> GetChocolateDatas(int pStageNum)
    {
        if (!chocolateBlocks.TryGetValue(pStageNum, out var blockDatas))
        {
            Debug.LogError($"[DataManager] GetChocolateDatas() : {pStageNum}에 해당하는 초콜릿 블록이 없습니다.");
            return null;
        }
        
        return blockDatas;
    }

    public List<BlockData> GetEaterDatas(int pStageNum)
    {
        if (!eaterBlocks.TryGetValue(pStageNum, out var eaterDatas))
        {
            Debug.LogError($"[DataManager] GetEaterDatas() : {pStageNum}에 해당하는 먹는 블록이 없습니다.");
            return null;
        }
        
        return eaterDatas;
    }

    public MapData GetMapData(int pStageNum)
    {
        if (!mapData.TryGetValue(pStageNum, out var mapDatas))
        {
            Debug.LogError($"[DataManager] GetMapData() : {pStageNum}에 해당하는 맵 데이터가 없습니다.");
            return null;
        }
        
        return mapDatas;
    }
}
