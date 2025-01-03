using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public GameObject Range;
    private bool  RockAttack = false;
    float speed = 0.05f;
    Vector3 Vector3 = new Vector3(3, 0, 0);
    [SerializeField] List<GameObject> ListAttack;//µ¹°ø°Ý
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndRange", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Size();
        CreateRock();
    }

    void Size()
    {
        if (Range.transform.localScale.x <= Vector3.x)
        {
            Range.transform.localScale += new Vector3(0.3f, 0.3f, 0) * speed;
            RockAttack = true;
        }
    }

    void CreateRock()
    {
        if(Range.transform.localScale.x >= Vector3.x&& RockAttack == true)
        {
            Vector3 Pos = new Vector3(0, 0.5f, 9.42f);
            GameObject Rock = Instantiate(ListAttack[0], Pos, Quaternion.identity);
            Rock.name = "RockAttack";
            RockAttack = false;
        }
    }
    void EndRange()
    {
        Destroy(Range);
    }
}
