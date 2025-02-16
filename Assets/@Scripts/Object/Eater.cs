using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Eater : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Transform objRotaion;
    [SerializeField] private EaterControl[] eaters;
    [SerializeField] private BoxCollider boxCollider;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Const
    private const float ANIMATION_INTERVAL = 0.5f;
    // ----- Normal
    public BlockData _data { get; private set; }
    private Define.EEaterState _previousState = Define.EEaterState.Idle;
    private Define.EEaterState _currentState = Define.EEaterState.Idle;
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public Vector3 CenterPosition { get; private set; } = Vector3.zero;
    private bool getBaseColliderCenter = false;
    private Vector2 baseColliderCenter;
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init(BlockData data, int n, int m)
    {
        _data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, 2f - y, 0);

        if (getBaseColliderCenter == false)
        {
            getBaseColliderCenter = true;
            baseColliderCenter = boxCollider.center;
        }
        
        if (data.dir == Define.EDirection.Right)
        {
            objRotaion.rotation = Quaternion.Euler(0, 0, -90);
            boxCollider.center = baseColliderCenter + new Vector2(0, 1f);
        }
        else if (data.dir == Define.EDirection.Left)
        {
            objRotaion.rotation = Quaternion.Euler(0, 0, +90);
            boxCollider.center = baseColliderCenter + new Vector2(-1, 0);
        }
        else if (data.dir == Define.EDirection.Up)
        {
            objRotaion.transform.rotation = Quaternion.Euler(0, 0, 0);
            boxCollider.center = baseColliderCenter + new Vector2(0, 0f);
        }
        else if (data.dir == Define.EDirection.Down)
        {
            objRotaion.transform.rotation = Quaternion.Euler(0, 0, 180);
            boxCollider.center = baseColliderCenter + new Vector2(-1, 1f);
        }
        
        foreach (var obj in eaters)
            obj.SetEater(x,y,n,m,data.dir,data.color);

        CalculateCenterPos();
    }

    private void CalculateCenterPos()
    {
        CenterPosition = Vector3.zero;
        foreach (var eater in eaters)
            CenterPosition += eater.transform.position;
        CenterPosition /= eaters.Length;
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
        }

        DOVirtual.DelayedCall(ANIMATION_INTERVAL, () =>
        {
            _previousState = _currentState;
            _currentState = Define.EEaterState.Idle;
        });
    }

    private void OnEat()
    {
        foreach (var eater in eaters)
        {
            eater.OnEat();
            eater.OnOWO();
        }
        Managers.Sound.Play("Cb_Eat");
    }

    private void OnAngry()
    {
        foreach (var eater in eaters)
            eater.OnAngry();
        Managers.Sound.Play("Cb_Hit");
    }
}