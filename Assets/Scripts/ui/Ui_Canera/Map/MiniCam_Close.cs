using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCam_Close : MonoBehaviour
{
    public static MiniCam_Close instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    public void Closemap()
    {
        gameObject.SetActive(false);
    }
    public void OpenMap()
    {
        gameObject.SetActive(true);
    }
}
