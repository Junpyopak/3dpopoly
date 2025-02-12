using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1)//æ∆¿Ã≈€ »πµÊ
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.ItemImage;
        SetColor(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
