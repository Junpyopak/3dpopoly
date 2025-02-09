using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPotion : MonoBehaviour
{
    Camera camera;
    // Start is called before the first frame update
    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Billboard();
    }

    public void Billboard()
    {
        transform.LookAt(transform.position+camera.transform.rotation * Vector3.forward,
         camera.transform.rotation * Vector3.up);
    }

}
