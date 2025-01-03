using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndAttack", 5f);
    }
    void EndAttack()
    {
        Destroy(gameObject);
    }
}
