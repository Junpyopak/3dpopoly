using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Vector3 oriPos;
    private Button slotButton;
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;
    private ItemEffect itemEffect;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("character").GetComponent<Player>();
        itemEffect = FindObjectOfType<ItemEffect>();
        oriPos = transform.position;
        slotButton = GetComponent<Button>();
        slotButton.interactable = false;
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1)//아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.ItemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
        }
        SetColor(1);
    }
    public void SetSlotCount(int _count)//아이템개수
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount <= 0)
        {
            ClearItemSlot();
        }
    }
    private void ClearItemSlot()//아이템 슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    //장비일때는 장착
                }
                else//장비류가 아닐때 소모
                {
                    if(item!=null)
                    {
                        if(player.Hp<player.MaxHp)//힐포션일떄 체력이 풀이 아닐경우에만 사용하기 위함.
                        {
                            itemEffect.Useitem(item);
                            Debug.Log(item.ItemName + "을 사용하였습니다.");
                            SetSlotCount(-1);
                        }
                        //itemEffect.Useitem(item);
                        //Debug.Log(item.ItemName + "을 사용하였습니다.");
                        //SetSlotCount(-1);
                        else
                        {
                            Debug.Log("현재 체력이 꽉 차있습니다");
                        }
                    }
                    
                    
                }
            }
        }
    }
}
