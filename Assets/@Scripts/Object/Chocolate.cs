using DG.Tweening;
using UnityEngine;

public class Chocolate : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private Transform transChocolateChild;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform transArrow;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Const
    private const float VIBRATION_SCALE = 0.05f;
    private const float VIBRATION_ROTATION = 5f;
    private const float VIBRATION_TIME = 0.1f;
    private const float RESET_TIME = 0.5f;
    private const float EATEN_MOVE_TIME = 0.2f;
    private const float MELTING_SCALE_PERCENT = 0.08f;
    private const float DRAG_THRESHOLD = 100f;
    private const float MOVE_SPEED = 10f;
    private const float MAX_RAY_DISTANCE = 50f;
    
    // ----- Normal
    private Vector3 _vibrationScaleVector = new(VIBRATION_SCALE, VIBRATION_SCALE, 0);
    private Vector3 _vibrationRotationVector = new(VIBRATION_ROTATION, VIBRATION_ROTATION, 0);
    private Vector3 _meltingScaleVector;
    
    private Vector3 _originPos;
    private Vector3 _originRot;
    private Vector3 _originScale;
    
    private Vector3 _dragStartpos;
    private Vector3 _moveDirection;
    private bool _isDragging = false;
    private bool _isMoving = false;
    
    private Vector3 _offset;
    private float _mouseZCoord;
    private int _chocolatePoint = 1;
    
    public BlockData Data { get; private set; }
    public bool CanMelting { get; set; } = true;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void OnMouseDown()
    {
        if (_isMoving)
            return;
        
        _isDragging = true;
        _dragStartpos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        // TODO : 적합하지 않은 이터 -> 초콜릿 원위치 및 애니메이션 연출
        if (!_isDragging || _isMoving)
            return;
        
        var dragVector = Input.mousePosition - _dragStartpos;
        var dragDistance = dragVector.magnitude;

        if (!(dragDistance >= DRAG_THRESHOLD)) 
            return;
        
        var mainCamera = Camera.main;
        var worldDragVector = mainCamera.transform.right * dragVector.x + mainCamera.transform.up * dragVector.y;
        worldDragVector.z = 0;
            
        var angle = Mathf.Atan2(worldDragVector.y, worldDragVector.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 90f) * 90f;
            
        var x = (Data.dir is Define.EDirection.Left or Define.EDirection.Right) ? Mathf.Cos(angle * Mathf.Deg2Rad) : 0;
        var y = (Data.dir is Define.EDirection.Up or Define.EDirection.Down) ? Mathf.Sin(angle * Mathf.Deg2Rad) : 0;
        _moveDirection = new Vector3(x, y, 0).normalized;

        var ray = new Ray(transform.position, _moveDirection);
        if (Physics.Raycast(ray, out _, MAX_RAY_DISTANCE))
        {
            _isMoving = true;
            _isDragging = false;
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

    private void Update()
    {
        if (!_isMoving) 
            return;

        CanMelting = true;
        var ray = new Ray(transform.position, _moveDirection);
        if (Physics.Raycast(ray, out var hit, MAX_RAY_DISTANCE))
        {
            var distanceToTarget = Vector3.Distance(transform.position, hit.point);
            var moveAmount = MOVE_SPEED * Time.deltaTime;

            if (moveAmount >= distanceToTarget)
            {
                transform.position = hit.point;
                _isMoving = false;
            }
            else
                transform.position += _moveDirection * moveAmount;
        }
        else
            _isMoving = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Define.ETagType.Chocolate.ToString()))
        {
            OnReset();
            OnVibration();
        }
        else if (other.gameObject.CompareTag(Define.ETagType.Eater.ToString()))
        {
            if (!other.gameObject.TryGetComponent(out Eater eater))
            {
                Debug.LogError("[Chocolate] OnCollisionEnter : Eater 컴포넌트가 없습니다.");
                return;
            }

            if (Data.color != eater._data.color)
            {
                
                OnReset();
                OnVibration();
                return;
            }

            OnEaten(eater);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_isMoving)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _moveDirection * MAX_RAY_DISTANCE);
        }
    }
    
    // --------------------------------------------------
    // Functions - Normal
    // --------------------------------------------------
    public void Init(BlockData data)
    {
        Data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, -y + 2, 0);
        
        ResetPivot();
        SetArrow();
        SetPoint();
        SetColor();
        
        _originPos = transform.position;
        _originRot = transform.eulerAngles;
        _originScale = transChocolateChild.localScale;
        
        _meltingScaleVector = new Vector3(_originScale.x * MELTING_SCALE_PERCENT, _originScale.y * MELTING_SCALE_PERCENT, 0);
    }

    public void OnMelting()
    {
        var targetScale = transChocolateChild.localScale - _meltingScaleVector;
        transChocolateChild.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            _originScale = transChocolateChild.localScale;
        });
    }

    private void OnReset()
    {
        _isMoving = false;

        var moveAmount = 0.5f;
        var type = Data.blockType;
        var dir = Data.dir;
        if ((type == Define.EBlockType.Chocolate2 && dir is Define.EDirection.Up or Define.EDirection.Down) ||
            (type == Define.EBlockType.Chocolate3 && dir is Define.EDirection.Left or Define.EDirection.Right) ||
            type == Define.EBlockType.Chocolate4)
        {
            moveAmount = 0f;
        }
            
        var targetPos = transform.position;
        var isX = _moveDirection.x != 0;
        var isY = _moveDirection.y != 0;
        var sign = (_moveDirection.x > 0 || _moveDirection.y > 0) ? 1 : -1;
        if (isX)
        {
            targetPos.x = (sign > 0) ? Mathf.Floor(targetPos.x) : Mathf.CeilToInt(targetPos.x);
            targetPos.x += sign * moveAmount;
        }
        if (isY)
        {
            targetPos.y = (sign > 0) ? Mathf.Floor(targetPos.y) : Mathf.CeilToInt(targetPos.y);
            targetPos.y += sign * moveAmount;
        }
            
        transform.DOMove(targetPos, RESET_TIME);
    }

    private void OnVibration()
    {
        transChocolateChild.DOShakeScale(VIBRATION_TIME, _vibrationScaleVector, 10, 0f).OnComplete(() =>
        {
            transChocolateChild.DOScale(_originScale, VIBRATION_TIME);
        });
        transform.DOShakeRotation(VIBRATION_TIME, _vibrationRotationVector, 100, 0f).OnComplete(() =>
        {
            transform.DORotate(_originRot, VIBRATION_TIME);
        });
    }

    private void OnEaten(Eater eater)
    {
        transform.DOMove(eater.CenterPosition, EATEN_MOVE_TIME).OnComplete(() =>
        {
            eater.ChangeState(Define.EEaterState.Eat);
            Managers.Game.IncreaseScore(_chocolatePoint);
            Managers.Chocolate.DestroyChocolate(this);
        });
    }

    private void SetPoint()
    {
        if (Data.blockType == Define.EBlockType.Chocolate1)
            _chocolatePoint = 1;
        else if (Data.blockType is Define.EBlockType.Chocolate2 or Define.EBlockType.Chocolate3)
            _chocolatePoint = 2;
        else if (Data.blockType == Define.EBlockType.Chocolate4)
            _chocolatePoint = 4;
    }

    private void ResetPivot()
    {
        var weightX = 0.5f;
        var weightY = 0.5f;
        if (Data.blockType == Define.EBlockType.Chocolate2)
            weightY += 0.5f;
        else if (Data.blockType == Define.EBlockType.Chocolate3)
            weightX += 0.5f; 
        else if (Data.blockType == Define.EBlockType.Chocolate4)
        {
            weightX += 0.5f;
            weightY += 0.5f;
        }
        var pos = transform.position;
        pos.x += weightX;
        pos.y -= weightY;
        transform.position = pos;
        transChocolateChild.localPosition = Vector3.zero;
        collider.center = Vector3.zero;
    }

    private void SetArrow()
    {
        if (Data.blockType == Define.EBlockType.Chocolate2)
        {
            if (Data.dir is Define.EDirection.Left or Define.EDirection.Right)
            {
                var rot = transArrow.rotation.eulerAngles;
                rot.z = 0;
                transArrow.rotation = Quaternion.Euler(rot);
                transArrow.localScale *= 0.5f;
            }
        }
        else
        {
            if (Data.dir is Define.EDirection.Up or Define.EDirection.Down)
            {
                var rot = transArrow.rotation.eulerAngles;
                rot.z = 90;
                transArrow.rotation = Quaternion.Euler(rot);
            }
        }
    }

    private void SetColor()
    {
        var colorIndex = Data.color;
        var materialColor = Resources.Load<Material>($"Materials/{colorIndex}");
        if (materialColor == null)
        {
            Debug.LogError($"[Chocolate] SetColor : {colorIndex} Material not exists");
            return;
        }
        
        meshRenderer.material = materialColor;
    }
}