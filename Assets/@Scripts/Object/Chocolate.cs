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
    private const float VIBRATION_TIME = 0.1f;
    private const float RESET_TIME = 0.5f;
    
    // ----- Normal
    private Vector3 _vibrationVector = new(VIBRATION_SCALE, VIBRATION_SCALE, 0);
    private Vector3 _originPos;
    private Vector3 _originRot;
    private Vector3 _originScale;
    private BlockData _data;
    private Vector3 _offset;
    private float _mouseZCoord;
    private bool _isDragging = false;

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
        if (_isDragging)
        {
            var pos = GetMouseWorldPos() + _offset;
            pos.z = 0f;
            if (_data.dir == Define.EDirection.Up || _data.dir == Define.EDirection.Down)
                pos.x = transform.position.x;
            else
                pos.y = transform.position.y;
            transform.position = pos;
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        ResetPosition();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Define.ETagType.Chocolate.ToString()) && _isDragging)
        {
            ResetPosition();
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
        transform.DOShakeScale(VIBRATION_TIME, _vibrationVector, 10, 0f).OnComplete(() =>
        {
            transform.DOScale(_originScale, VIBRATION_TIME);
        });
        transform.DOShakeRotation(VIBRATION_TIME, _vibrationVector, 10, 0f).OnComplete(() =>
        {
            transform.DORotate(_originRot, VIBRATION_TIME);
        });
    }
}