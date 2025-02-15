using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EaterModel : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material[] _material;
    
    public void UpdateModel(BlockData pBlockData)
    {
        EColor color = pBlockData.color;
        _skinnedMeshRenderer.materials[4] = _material[(int)(color-1)];
    }
}
