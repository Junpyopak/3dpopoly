using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Text : Upgrade_Ui
{
    bool isSucces;
    bool isFail;
    Text ResultText;

    // Start is called before the first frame update
    void Start()
    {
        isSucces = Success;
        isFail = Failed;
        ResultText = GetComponent<Text>();
        if (Success == true)
        {
            ResultText.text = "<color=yellow>" + "��ȭ ���� !!" + "</color>";
        }
        else if(Failed == true)
        {
            ResultText.text = "<color=red>" + "��ȭ ����..." + "</color>";
        }
        Invoke("DeleteText",1f);
    }
    private void DeleteText()
    {
        Destroy(gameObject);
    }
}
