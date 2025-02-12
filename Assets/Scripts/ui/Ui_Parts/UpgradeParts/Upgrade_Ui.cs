using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Ui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgaradeParts()
    {
        int ran = Random.Range(0, 10);
        if(ran <5 )
        {
            Debug.Log("Succes!!!");
        }
        else if(ran <8)
        {
            Debug.Log("Fail!!!");
        }
        else if (ran < 9)
        {
            Debug.Log("Fail!!!");
        }
        else if (ran < 10)
        {
            Debug.Log("Fail!!!");
        }

    }

    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
