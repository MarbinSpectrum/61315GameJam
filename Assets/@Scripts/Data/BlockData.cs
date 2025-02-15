using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BlockData
{
    public readonly int idx;
    public readonly int row; //세로
    public readonly int col; //가로
    public readonly EBlockType blockType; //블록종류
    public readonly EColor color; //블록색상
    public readonly EDirection dir; //블록방향
    
    public BlockData(){}
    
    public BlockData(int pIdx, int pRow, int pCol, int pBlockType,int pColor,int pDir)
    {
        idx = pIdx;
        row = pRow;
        col = pCol;
        blockType = (EBlockType)pBlockType;
        color = (EColor)pColor;
        dir = (EDirection)pDir;
    }
}
