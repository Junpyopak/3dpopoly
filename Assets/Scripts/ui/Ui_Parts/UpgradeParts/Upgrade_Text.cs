using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Text : MonoBehaviour
{
    public bool isSucces;
    public bool isFail;
    public bool Downgrade;
    Text ResultText;

    private void Awake()
    {
        ResultText = GetComponentInChildren<Text>();
        if (isSucces == true)
        {
            ResultText.text = "<color=yellow>" + "강화 성공 !!" + "</color>";
        }
        else if (isFail == true)
        {
            ResultText.text = "<color=red>" + "강화 실패..." + "</color>";
        }
        else if(Downgrade == true)
        {
            ResultText.text = "<color=black>" + "강화 하락..." + "</color>";
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        Invoke("DeleteText", 0.5f);
    }
    private void DeleteText()
    {
        Destroy(gameObject);
    }
}
