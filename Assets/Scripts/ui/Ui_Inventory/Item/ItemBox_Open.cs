using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemBox_Open : MonoBehaviour
{
    GameObject player;

    Animator anim;
    bool isPlayerEnter; // Player�� ���� �ȿ� �Դ����� �Ǻ��� bool Ÿ�� ����
    [SerializeField]
    private Text ActionText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        isPlayerEnter = false;
    }

    void Update()
    {
        // �÷��̾ ���� �ȿ� �ְ� E Ű�� �����ٸ�
        if (isPlayerEnter && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Open");
        }

    }
    // �ݶ��̴��� ���� ��ü�� (Ʈ���ſɼ��� üũ��)�ݶ��̴� ���� ������ ���԰� �װ� �÷��̾��� 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = true;
            ActionText.gameObject.SetActive(true);
            ActionText.text = "���ڿ���" + "<color=yellow>" + "(E)" + "</color>";
        }
    }
    // �ݶ��̴��� ���� ��ü�� �ݶ��̴� ���� ������ ������ �� ��ü�� �÷��̾���
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
            ActionText.gameObject.SetActive(false);
        }
    }
}
