using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUi : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;  
    public Transform content;          
    public GameObject messagePrefab;    
    public ScrollRect scrollRect;      

    void Start()
    {
        inputField.onSubmit.AddListener(SendMessage);
    }

    void Update()
    {
       //����Ű�� �޼��� ����
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrWhiteSpace(inputField.text))
            {
                SendMessage(inputField.text);
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }
    }

    public void SendMessage(string msg)
    {
        if (string.IsNullOrWhiteSpace(msg)) return;

        // �޽��� ������ ����
        GameObject newMsg = Instantiate(messagePrefab, content);
        TMP_Text txt = newMsg.GetComponent<TMP_Text>();
        if (txt != null)
        {
            txt.text = msg;
        }


        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
