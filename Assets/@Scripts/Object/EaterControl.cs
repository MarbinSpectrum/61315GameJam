using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class EaterControl : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private ParticleSystem angryEmoji;
    [SerializeField] private ParticleSystem owoEmoji;
    [SerializeField] private Animation[] characterAniObj;
    [SerializeField] private GameObject[] table;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private Animation _animationCurrentCharactor;
    private bool getBasePos = false;
    private Vector3 basePosition;
    private const int EAT_CNT = 4;
    private IEnumerator eatCor;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float eatAniSpeed = 0.5f;
    
    public void SetEater(int x,int y,int n,int m,EDirection dir,EColor color)
    {
        Vector2 newOffset = offset;
        if(dir == EDirection.Up)
            newOffset  = Quaternion.Euler(0f,0f,0f)*offset;
        else if(dir == EDirection.Down)
            newOffset  = Quaternion.Euler(0f,0f,180f)*offset;    
        else  if(dir == EDirection.Left)
            newOffset  = Quaternion.Euler(0f,0f,-90f)*offset;
        else if(dir == EDirection.Right)
            newOffset  = Quaternion.Euler(0f,0f,90f)*offset;    
        
        int newX = x + (int)newOffset.x;
        int newY = y + (int)newOffset.y;
        
        if (getBasePos == false)
        {
            getBasePos = true;
            basePosition = transform.localPosition;
        }
        
        foreach (var obj in characterAniObj)
            obj.gameObject.SetActive(false);

        int randomIdx = Random.Range(0, characterAniObj.Length);
        _animationCurrentCharactor = characterAniObj[randomIdx];
        _animationCurrentCharactor.gameObject.SetActive(true);

        foreach (var obj in table)
            obj.SetActive(false);

        SetColor(color); 
        
        if (dir == Define.EDirection.Right)
        {
            if (newY == n)
                table[2].SetActive(true);
            else if (newY == 1)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
            transform.localPosition = basePosition + new Vector3(0,1,0);
        }
        else if (dir == Define.EDirection.Left)
        {
            if (newY == n)
                table[0].SetActive(true);
            else if (newY == 1)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
            transform.localPosition = basePosition + new Vector3(-1,0,0);
        }
        else if (dir == Define.EDirection.Up)
        {
            if (newX == 1)
                table[0].SetActive(true);
            else if (newX == m)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
            transform.localPosition = basePosition + new Vector3(0,0,0);
        }
        else if (dir == Define.EDirection.Down)
        {
            if (newX == 1)
                table[2].SetActive(true);
            else if (newX == m)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
            transform.localPosition = basePosition + new Vector3(-1,1,0);
        }
    }
    
    public void OnEat()
    {
        _animationCurrentCharactor.Play();
        
        if (eatCor != null)
        {
            StopCoroutine(eatCor);
            eatCor = null;
        }

        eatCor = EatCor();
        StartCoroutine(eatCor);
    }

    private IEnumerator EatCor()
    {
        //EAT_CNT만큼 씹는 모션이 나옴
        for (int i = 0; i < EAT_CNT; i++)
        {
            foreach (AnimationState state in _animationCurrentCharactor)
                state.speed = eatAniSpeed;
            _animationCurrentCharactor.Play();
            yield return new WaitWhile(() => _animationCurrentCharactor.isPlaying);
        }
    }

    public void OnAngry()
    {
        angryEmoji.Play();
    }

    public void OnOWO()
    {
        owoEmoji.Play();
    }
    
    private void SetColor(EColor color)
    {
        var materialColor = Resources.Load<Material>($"Materials/{color}");
        if (materialColor == null)
        {
            Debug.LogError($"[Eater] SetColor : {color} Material not exists");
            return;
        }

        foreach (var mesh in skinnedMeshRenderers)
        {
            var mats = mesh.materials; 
            mats[4] = materialColor;
            mesh.materials = mats;
        }
    }
}
