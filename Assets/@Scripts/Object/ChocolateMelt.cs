using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ChocolateMelt : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Animation meltAnimation;
        
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private IEnumerator meltCor;
    
    public void SetMelt(EColor pColor)
    {
        var materialColor = Resources.Load<Material>($"Materials/{pColor}");
        if (materialColor == null)
        {
            Debug.LogError($"[ChocolateMelt] SetMelt : {pColor} Material not exists");
            return;
        }
        
        skinnedMeshRenderer.material = materialColor;

        if (meltCor != null)
        {
            StopCoroutine(meltCor);
            meltCor = null;
        }

        meltCor = MeltCor();
        StartCoroutine(meltCor);
    }

    private IEnumerator MeltCor()
    {
        meltAnimation.Play();
        yield return new WaitWhile(() => meltAnimation.isPlaying);
        
        Managers.Chocolate.RemoveMelt(this);
    }
}
