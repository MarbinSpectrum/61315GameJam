using UnityEngine;
using UnityEngine.EventSystems;

public class Chocolate : MonoBehaviour
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private BlockData _data;
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init(BlockData data, int n, int m)
    {
        _data = data;
        var x = (data.col - 1);
        var y = -(data.row - 1);
        transform.position = new Vector3(x, y, 0);
        
        Utils.AddUIEvent(gameObject, OnDragEvent, Define.EUIEvent.Drag);
        Utils.AddUIEvent(gameObject, OnDropEvent, Define.EUIEvent.Drop);
    }
    

    private void OnDragEvent(PointerEventData eventData)
    {
        // TODO : 드래그 시 초콜릿 움직임 구현
        // TODO : 이동 불가 or 적합하지 않은 이터 -> 초콜릿 원위치 및 애니메이션 연출
    }

    private void OnDropEvent(PointerEventData eventData)
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        // TODO : originPos로 Slerp를 이용하여 부드럽게 이동
    }

    private void OnVibration()
    {
        // TODO : 띠용 연출
    }

    private void CanMove()
    {
        // TODO : 이동 가능 여부 판단
    }
}
