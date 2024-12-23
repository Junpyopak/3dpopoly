using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Attack1 = false;
    public bool Attack2;
    GameObject mProjectorAtt1;
    [SerializeField] List<GameObject> listPattern;//적의 종류
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack_1();
    }

    private void Attack_1()
    {
        if (Attack1 == true)
        {
            Vector3 Pos = transform.position;
            GameObject pattern = Instantiate(listPattern[0], Pos, Quaternion.identity);
            Attack1 = false;
        }
    }

}
