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
        //여기서 데이터 불러와서 계정이 있으면 로딩창(로비창)으로
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
            Debug.Log("로그인 오류 ! 아이디를 확인하세요");
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
