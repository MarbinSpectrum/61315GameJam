using DG.Tweening;
using UnityEngine;

public class Chocolate : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Const
    private const float VIBRATION_SCALE = 0.05f;
    private const float VIBRATION_ROTATION = 5f;
    private const float VIBRATION_TIME = 0.1f;
    private const float RESET_TIME = 0.5f;
    private const float EATEN_MOVE_TIME = 0.2f;
    
    // ----- Normal
    private Vector3 _scaleVector = new(VIBRATION_SCALE, VIBRATION_SCALE, 0);
    private Vector3 _rotationVector = new(VIBRATION_ROTATION, VIBRATION_ROTATION, 0);
    private Vector3 _originPos;
    private Vector3 _originRot;
    private Vector3 _originScale;
    private BlockData _data;
    private Vector3 _offset;
    private float _mouseZCoord;
    private bool _isDragging = false;
    private int _chocolatePoint = 1;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void OnMouseDown()
    {
        _mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        _offset = gameObject.transform.position - GetMouseWorldPos();
        _isDragging = true;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    private void OnMouseDrag()
    {
        // TODO : 적합하지 않은 이터 -> 초콜릿 원위치 및 애니메이션 연출
        if (_isDragging == false)
            return;
        
        var pos = GetMouseWorldPos() + _offset;
        pos.z = 0f;
        if (_data.dir is Define.EDirection.Up or Define.EDirection.Down)
            pos.x = transform.position.x;
        else
            pos.y = transform.position.y;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        if (_isDragging == false)
            return;
        
        _isDragging = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        ResetPosition();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Define.ETagType.Chocolate.ToString()) && _isDragging)
        {
            OnMouseUp();
        }
        else if (other.gameObject.CompareTag(Define.ETagType.Eater.ToString()))
        {
            if (!other.gameObject.TryGetComponent(out Eater eater))
            {
                Debug.LogError("[Chocolate] OnCollisionEnter : Eater 컴포넌트가 없습니다.");
                return;
            }

            OnEaten(eater);
        }
    }
    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public void Init(BlockData data)
    {
        _data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, -y + 2, 0);
        
        _originPos = transform.position;
        _originRot = transform.eulerAngles;
        _originScale = transform.localScale;
        
        SetPoint();
    }

    private void OnMelting()
    {
        
    }

    private Vector3 GetMouseWorldPos()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZCoord;
        if (Camera.main == null)
        {
            Debug.LogError("[Chocolate] GetMouseWorldPos : Camera.main is null");
            return Vector3.zero;
        }
        
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void ResetPosition()
    {
        transform.DOMove(_originPos, RESET_TIME);
        OnVibration();
    }
    
    private void OnVibration()
    {
        transform.DOShakeScale(VIBRATION_TIME, _scaleVector, 10, 0f).OnComplete(() =>
        {
            transform.DOScale(_originScale, VIBRATION_TIME);
        });
        transform.DOShakeRotation(VIBRATION_TIME, _rotationVector, 100, 0f).OnComplete(() =>
        {
            transform.DORotate(_originRot, VIBRATION_TIME);
        });
    }

    private void OnEaten(Eater eater)
    {
        transform.DOMove(eater.transform.position, EATEN_MOVE_TIME).OnComplete(() =>
        {
            eater.ChangeState(Define.EEaterState.Eat);
            Managers.Game.IncreaseScore(_chocolatePoint);
            Managers.Chocolate.DestroyChocolate(this);
        });
    }

    private void SetPoint()
    {
        if (_data.blockType == Define.EBlockType.Chocolate1)
            _chocolatePoint = 1;
        else if (_data.blockType is Define.EBlockType.Chocolate2 or Define.EBlockType.Chocolate3)
            _chocolatePoint = 2;
        else if (_data.blockType == Define.EBlockType.Chocolate4)
            _chocolatePoint = 4;
    }
}