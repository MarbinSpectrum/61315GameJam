using UnityEngine;

public class Eater : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Animation animation;
    [SerializeField] private GameObject objectEmoji;
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // private BlockData _data;
    
    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init()
    {
        // TODO : 매개변수로 BlockData 받아오기
        // _data = data;
    }
    
    public void OnEat()
    {
        // TODO : 와그작 애니메이션 재생
    }

    public void OnAngry()
    {
        // TODO : 화나는 이모지 연출
    }
}
