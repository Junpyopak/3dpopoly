using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemBox_Open : MonoBehaviour
{
    GameObject player;
    GameObject PotionImg;
    Animator anim;
    bool isPlayerEnter; // Player�� ���� �ȿ� �Դ����� �Ǻ��� bool Ÿ�� ����
    bool boxOpening = false;
    [SerializeField]
    private Text ActionText;
    [SerializeField]
    GameObject potion;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        PotionImg = GameObject.Find("PotionUp");
        isPlayerEnter = false;
        PotionImg .SetActive(false);
    }

    IEnumerator BoxOpen()
    {
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
    IEnumerator PotionImageUp()
    {
        yield return new WaitForSeconds(3f);
        PotionImg.SetActive(true);
    }
    void Update()
    {
        // �÷��̾ ���� �ȿ� �ְ� E Ű�� �����ٸ�
        if (boxOpening==false&&isPlayerEnter && Input.GetKeyDown(KeyCode.E))
        {
            boxOpening = true;
            anim.SetTrigger("Open");
           StartCoroutine(BoxOpen());
            StartCoroutine(PotionImageUp());
            Instantiate(potion,transform.position,Quaternion.identity);
            ActionText.gameObject.SetActive(false);
        }

    }
    // �ݶ��̴��� ���� ��ü�� (Ʈ���ſɼ��� üũ��)�ݶ��̴� ���� ������ ���԰� �װ� �÷��̾��� 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player&&boxOpening==false)
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
