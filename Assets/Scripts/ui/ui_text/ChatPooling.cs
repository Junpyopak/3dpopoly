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
    public int maxMessages = 20;   //�޼��� ���������� �ƽ�
    public int initialPoolSize = 20;

    private Queue<GameObject> activeMessages = new Queue<GameObject>();
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        //����
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
       //���� ��������
        GameObject newMsg;
        if (pool.Count > 0)
        {
            newMsg = pool.Dequeue();
        }
        else
        {
            newMsg = Instantiate(messagePrefab, content);
        }

        //ä�ý� ��Ƽ�� true�� �����ֱ�
        newMsg.SetActive(true);
        newMsg.transform.SetAsLastSibling(); 
        TMP_Text txt = newMsg.GetComponent<TMP_Text>();
        if (txt != null)
            txt.text = msg;

        activeMessages.Enqueue(newMsg);

        //�� �����ȸ߼��� false
        if (activeMessages.Count > maxMessages)
        {
            GameObject oldest = activeMessages.Dequeue();
            oldest.SetActive(false);
            pool.Enqueue(oldest);
        }

        //��ũ�� �ϴܿ� ����
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
