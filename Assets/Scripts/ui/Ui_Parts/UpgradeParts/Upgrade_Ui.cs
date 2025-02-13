using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Upgrade_Ui : MonoBehaviour
{
    public UpgradeSlot[] slots;
    [SerializeField] List<GameObject> listWeapon;
    [SerializeField]
    private Text level;
    int Level=0;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<UpgradeSlot>();
        DoPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgaradeParts()
    {
        int ran = Random.Range(0, 10);
        if(ran <5 )
        {
            Level++;
            Debug.Log("Succes!!!");
            level.text = $"Lv.{((int)Level)} ";
        }
        else if(ran <8)
        {
            Debug.Log("Fail!!!");
        }
        else if (ran < 9)
        {
            Debug.Log("Fail!!!");
        }
        else if (ran < 10)
        {
            Debug.Log("Fail!!!");
        }

    }
    public void AddSlotItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    public void CloseTab()
    {
        gameObject.SetActive(false);
    }
    private void DoPickup()
    {
        AddSlotItem(listWeapon[0].transform.GetComponent<GetItem>().item);
    }
}
