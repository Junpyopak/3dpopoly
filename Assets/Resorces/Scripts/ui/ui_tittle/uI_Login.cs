using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class uI_Login : MonoBehaviour
{
    [SerializeField] Text textInput;
    [SerializeField] InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        //���⼭ ������ �ҷ��ͼ� ������ ������ �ε�â(�κ�â)����
        //Shared.Scenemgr.GetPlayerPrefsStringKey("id");

        //if (Shared.Scenemgr.GetPlayerPrefsStringKey("id") != null)
        //{
        //    Debug.Log(PlayerPrefs.GetString("id"));
        //    Shared.Scenemgr.ChangeScene(eSCENE.LOBBY);
        //}

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void OnBtnLogin()
    {

        if (inputField.text == Shared.Scenemgr.GetPlayerPrefsStringKey("id"))
        {
            Shared.Scenemgr.ChangeScene(eSCENE.LOBBY);
        }
        else
        {
            Debug.Log("�α��� ���� ! ���̵� Ȯ���ϼ���");
        }
    }

    public void BtnJoin()
    {
        if (inputField.text.Length >= 2 || inputField.text.Length <= 7)
        {
            Shared.Scenemgr.SetPlayerPrefsStringKey("id", inputField.text);
        }
    }
}
