using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public readonly int N; //세로
    public readonly int M; //가로
    
    public MapData(){}

    public MapData(int pN, int pM)
    {
        N = pN;
        M = pM;
    }
}
