using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_Fill : MonoBehaviour
{
    private Image Fillimage;
    // Start is called before the first frame update
    void Start()
    {
        Fillimage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fillimage.fillAmount += Time.deltaTime;
    }
}
