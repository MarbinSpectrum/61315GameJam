using UnityEngine;
using UnityEngine.Serialization;

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
    public BlockData _data { get; private set; }
    private Animation nowChObj;

    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init(BlockData data, int n, int m)
    {
        _data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, 2f - y, 0);
        
        foreach (var obj in characterAniObj)
            obj.gameObject.SetActive(false);

        int randomIdx = Random.Range(0, characterAniObj.Length);
        nowChObj = characterAniObj[randomIdx];
        nowChObj.gameObject.SetActive(true);

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

    public void OnEat()
    {
        nowChObj.Play("Eat");
    }

    public void OnAngry()
    {
        angryEmoji.Play();
    }

    public void OnNice()
    {
        owoEmoji.Play();
    }

}
