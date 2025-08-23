using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatPooling : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;
    public Transform content;
    public GameObject messagePrefab;
    public ScrollRect scrollRect;

    [Header("Pooling Settings")]
    public int maxMessages = 20;   //메세지 폴링을위한 맥스
    public int initialPoolSize = 20;

    private Queue<GameObject> activeMessages = new Queue<GameObject>();
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        //폴링
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(messagePrefab, content);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrWhiteSpace(inputField.text))
            {
                AddMessage(inputField.text);
                inputField.text = "";
                inputField.ActivateInputField();
            }
        }
    }

    public void AddMessage(string msg)
    {
       //폴링 가져오기
        GameObject newMsg;
        if (pool.Count > 0)
        {
            newMsg = pool.Dequeue();
        }
        else
        {
            newMsg = Instantiate(messagePrefab, content);
        }

        //채팅시 엑티브 true로 보여주기
        newMsg.SetActive(true);
        newMsg.transform.SetAsLastSibling(); 
        TMP_Text txt = newMsg.GetComponent<TMP_Text>();
        if (txt != null)
            txt.text = msg;

        activeMessages.Enqueue(newMsg);

        //젤 오래된멕세지 false
        if (activeMessages.Count > maxMessages)
        {
            GameObject oldest = activeMessages.Dequeue();
            oldest.SetActive(false);
            pool.Enqueue(oldest);
        }

        //스크롤 하단에 고정
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
