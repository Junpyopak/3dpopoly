using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Button slotButton;
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;
    // Start is called before the first frame update
    void Start()
    {
        slotButton = GetComponent<Button>();
        slotButton.interactable = false;
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item,int _count =1)//아이템 획득
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.ItemImage;

        if(item.itemType != Item.ItemType.Equipment)
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
        if(itemCount<=0)
        {
            ClearItemSlot();
        }
    }
    private void ClearItemSlot()//아이템 슬롯 초기화
    {
        item =null;
        itemCount =0;
        itemImage.sprite =null;
        SetColor(0);
        
        text_Count.text = "0";
        CountImage.SetActive(false);
    }
}
