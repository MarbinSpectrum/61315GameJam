using UnityEngine;
using UnityEngine.Serialization;

public class Eater : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private GameObject angryEmoji;
    [SerializeField] private GameObject owoEmoji;
    [SerializeField] private GameObject[] chObj;
    [SerializeField] private GameObject[] table;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    public BlockData _data { get; private set; }
    private GameObject nowChObj;

    // --------------------------------------------------
    // Functions
    // --------------------------------------------------
    public void Init(BlockData data, int n, int m)
    {
        _data = data;
        var x = data.col;
        var y = data.row;
        transform.position = new Vector3(x, n + 1 - y, 0);
        
        foreach (var obj in chObj)
            obj.SetActive(false);

        int randomIdx = Random.Range(0, chObj.Length);
        nowChObj = chObj[randomIdx];
        nowChObj.SetActive(true);

        foreach (var obj in table)
            obj.SetActive(false);

        if (data.dir == Define.EDirection.Right)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            if (y == n)
                table[2].SetActive(true);
            else if (y == 1)
                table[0].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Left)
        {
            transform.rotation = Quaternion.Euler(0, 0, +90);
            if (y == n)
                table[0].SetActive(true);
            else if (y == 1)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (x == 1)
                table[0].SetActive(true);
            else if (x == m)
                table[2].SetActive(true);
            else
                table[1].SetActive(true);
        }
        else if (data.dir == Define.EDirection.Down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
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
        nowChObj.GetComponent<Animation>().Play("Eat");
    }

    public void OnAngry()
    {
        angryEmoji.GetComponent<ParticleSystem>().Play();
    }

    public void OnNice()
    {
        owoEmoji.GetComponent<ParticleSystem>().Play();
    }

}
