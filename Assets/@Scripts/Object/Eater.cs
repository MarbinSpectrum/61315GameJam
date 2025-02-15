using DG.Tweening;
using UnityEngine;

public class Eater : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Transform objRotaion;
    [SerializeField] private ParticleSystem angryEmoji;
    [SerializeField] private ParticleSystem owoEmoji;
    [SerializeField] private Animation[] characterAniObj;
    [SerializeField] private GameObject[] table;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Const
    private const float ANIMATION_INTERVAL = 0.5f;
    // ----- Normal
    public BlockData _data { get; private set; }
    private Animation _animationCurrentCharactor;
    private Define.EEaterState _previousState = Define.EEaterState.Idle;
    private Define.EEaterState _currentState = Define.EEaterState.Idle;
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public Vector3 CenterPosition { get; private set; } = Vector3.zero;

    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init(BlockData data, int n, int m)
    {
        _data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, 2f - y, 0);
        CenterPosition = objRotaion.position;
        
        foreach (var obj in characterAniObj)
            obj.gameObject.SetActive(false);

        int randomIdx = Random.Range(0, characterAniObj.Length);
        _animationCurrentCharactor = characterAniObj[randomIdx];
        _animationCurrentCharactor.gameObject.SetActive(true);

        foreach (var obj in table)
            obj.SetActive(false);

        if (data.dir == Define.EDirection.Right)
        {
            objRotaion.rotation = Quaternion.Euler(0, 0, -90);
                
            if (y == n)
                table[2].SetActive(true);
            else if (y == 1)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Left)
        {
            objRotaion.rotation = Quaternion.Euler(0, 0, +90);
            
            if (y == n)
                table[0].SetActive(true);
            else if (y == 1)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Up)
        {
            objRotaion.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            if (x == 1)
                table[0].SetActive(true);
            else if (x == m)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Down)
        {
            objRotaion.transform.rotation = Quaternion.Euler(0, 0, 180);
            
            if (x == 1)
                table[2].SetActive(true);
            else if (x == m)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);

        }
    }

    public void ChangeState(Define.EEaterState state)
    {
        if (_currentState == state)
            return;

        _previousState = _currentState;
        _currentState = state;

        switch (state)
        {
            case Define.EEaterState.Eat:
                OnEat();
                break;
            case Define.EEaterState.Angry:
                OnAngry();
                break;
            case Define.EEaterState.Nice:
                OnNice();
                break;
        }

        DOVirtual.DelayedCall(ANIMATION_INTERVAL, () =>
        {
            _previousState = _currentState;
            _currentState = Define.EEaterState.Idle;
        });
    }

    private void OnEat()
    {
        _animationCurrentCharactor.Play();
    }

    private void OnAngry()
    {
        angryEmoji.Play();
    }

    private void OnNice()
    {
        owoEmoji.Play();
    }
}
