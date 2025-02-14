using UnityEngine;

public class Eater : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Animation animation;
    [SerializeField] private GameObject objectEmoji;
    [SerializeField] private GameObject[] table;
    
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
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, y, 0);

        foreach (var obj in table)
            obj.SetActive(false);

        if (x == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            if(y == 1)
                table[2].SetActive(true);
            else   if(y == n)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (x == m + 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, +90);
            if(y == 1)
                table[0].SetActive(true);
            else   if(y == n)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if(x == 1)
                table[0].SetActive(true);
            else   if(x == m)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (y == n + 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            if(x == 1)
                table[2].SetActive(true);
            else   if(x == m)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
            
        }
        

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
