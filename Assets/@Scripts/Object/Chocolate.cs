using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chocolate : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private Vector3 _originPos;
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
        int stageNum = Managers.Game.stageNum;
        var mapdata = Managers.Data.GetMapData(stageNum);
        transform.position = new Vector3(x, mapdata.N + 1 - y, 0);
        _originPos = transform.position;
    }
    
    private Vector3 GetMouseWorldPos()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void ResetPosition()
    {
        var scaleVec = new Vector3(0.05f, 0.05f, 0);
        transform.DOMove(_originPos, 0.5f);
        transform.DOShakeScale(0.2f, scaleVec, 10, 0f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one / 2f, 0.1f); 
        });
        // transform.DOShakeRotation(0.5f);
    }

    private void OnVibration()
    {
        // TODO : 띠용 연출
    }
}
