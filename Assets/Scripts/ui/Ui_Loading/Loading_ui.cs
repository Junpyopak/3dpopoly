using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_ui : MonoBehaviour
{
    public Image[] images;
    public float fillSpeed;
    private int FillIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Fill();
    }
    private void Fill()
    {
        //for (int j = 0; j < images.Length; j++)
        //{
        //    if (images[j].GetComponent<Image>().fillAmount < 1)
        //    {
        //        images[j].GetComponent<Image>().fillAmount += Time.deltaTime * 0.4f;
        //    }
        //}
        if (FillIndex >= images.Length) return;
        Image image = images[FillIndex];
        if(image.fillAmount<1)
        {
            image.fillAmount += Time.deltaTime * fillSpeed;
        }
        else
        {
            FillIndex++;
            RanSpeed();
        }
    }
    private void RanSpeed()
    {
        fillSpeed = Random.Range(0.4f, 1f);
    }
}
