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
    public void AddItem(Item _item, int _count = 1)//������ ȹ��
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
    //public void AddItem(Item newItem, int newCount)
    //{
    //    Debug.Log($"[Slot] AddItem ȣ��� - {newItem?.ItemName}, ����: {newCount}");
    //    item = newItem;
    //    itemCount = newCount;
    //    UpdateUI();
    //    if (item.itemType != Item.ItemType.Equipment)
    //    {
    //        CountImage.SetActive(true);
    //        text_Count.text = itemCount.ToString();
    //    }
    //    else
    //    {
    //        text_Count.text = "0";
    //        CountImage.SetActive(false);
    //    }
    //    SetColor(1);
    //}
    public void SetSlotCount(int _count)//�����۰���
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        UpdateUI();
        if (itemCount <= 0)
        {
            ClearItemSlot();
        }
    }
    private void ClearItemSlot()//������ ���� �ʱ�ȭ
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
                    //����϶��� ����
                }
                else//������ �ƴҶ� �Ҹ�
                {
                    if(item!=null)
                    {
                        if(player.Hp<player.MaxHp)//�������ϋ� ü���� Ǯ�� �ƴҰ�쿡�� ����ϱ� ����.
                        {
                            itemEffect.Useitem(item);
                            Debug.Log(item.ItemName + "�� ����Ͽ����ϴ�.");
                            SetSlotCount(-1);
                        }
                        //itemEffect.Useitem(item);
                        //Debug.Log(item.ItemName + "�� ����Ͽ����ϴ�.");
                        //SetSlotCount(-1);
                        else
                        {
                            Debug.Log("���� ü���� �� ���ֽ��ϴ�");
                        }
                    }
                    
                    
                }
            }
        }
    }
    private void UpdateUI()
    {
        if (item != null)
        {
            itemImage.sprite = item.ItemImage;
            itemImage.enabled = true;
            text_Count.text = itemCount.ToString();
        }
        else
        {
            itemImage.enabled = false;
            text_Count.text = "";
        }
    }
}
