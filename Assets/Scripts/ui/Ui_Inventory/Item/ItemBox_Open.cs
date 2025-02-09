using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemBox_Open : MonoBehaviour
{
    GameObject player;

    Animator anim;
    bool isPlayerEnter; // Player가 범위 안에 왔는지를 판별할 bool 타입 변수
    bool boxOpening = false;
    [SerializeField]
    private Text ActionText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        isPlayerEnter = false;
    }

    IEnumerator BoxOpen()
    {
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
    void Update()
    {
        // 플레이어가 범위 안에 있고 E 키를 누른다면
        if (boxOpening==false&&isPlayerEnter && Input.GetKeyDown(KeyCode.E))
        {
            boxOpening = true;
            anim.SetTrigger("Open");
           StartCoroutine(BoxOpen());
            ActionText.gameObject.SetActive(false);
        }

    }
    // 콜라이더를 가진 객체가 (트리거옵션이 체크된)콜라이더 범위 안으로 들어왔고 그게 플레이어라면 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player&&boxOpening==false)
        {
            isPlayerEnter = true;
            ActionText.gameObject.SetActive(true);
            ActionText.text = "상자열기" + "<color=yellow>" + "(E)" + "</color>";
        }
    }
    // 콜라이더를 가진 객체가 콜라이더 범위 밖으로 나갔고 그 객체가 플레이어라면
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
            ActionText.gameObject.SetActive(false);
        }
    }
}
