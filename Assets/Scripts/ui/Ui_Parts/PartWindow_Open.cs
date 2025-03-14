using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PartWindow_Open : MonoBehaviour
{
    private bool OpenWindow = false;
    public GameObject OpenParts;
    public GameObject UpgradeButton;
    public GameObject UpgradeWindow;


    void Start()
    {
        OpenParts.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenWindow = !OpenWindow;
            OpenParts.SetActive(OpenWindow);
        }
        if (OpenParts == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if(OpenWindow == false)
        {
            UpgradeWindow.SetActive(false);
        }
        
    }
    public void closeUpgrade()
    {
        UpgradeButton.SetActive(false);
    }
    public void OpenUpgrade()
    {
        Debug.Log("��ȭ�ϱ�");
        UpgradeWindow.SetActive(true);
        UpgradeButton.SetActive(false);
    }
   
    public void Btn_OpenWindow()
    {
        OpenWindow = true;
        OpenParts.SetActive(OpenWindow);
    }
    public void Btn_closeWindow()
    {
        OpenWindow = false;
        OpenParts.SetActive(OpenWindow);
    }
}
