using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Retry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBtnReTry()
    {
        Shared.Scenemgr.ChangeScene(eSCENE.LOADING);
    }
}
