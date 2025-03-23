using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Minimap : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z;
    [SerializeField]
    private Transform Target;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Target != null)
            return;

        transform.position = new Vector3((x ? transform.position.x : Target.position.x),
            (y ? transform.position.y : Target.position.y),
            (z ? transform.position.z : Target.position.z));

    }
    private void FixedUpdate()
    {
        
    }
}
