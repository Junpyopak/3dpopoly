using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windo_dontdestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
