using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    Inventory inven;
    MiniCam_Close MiniCam_close;
    public GameObject Inven;
    private bool OpenInvebtory = false;
    public Slot[] slots;
    public Transform SlotHolder;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        inven = Inventory.instance;
        slots = GetComponentsInChildren<Slot>();
        inven.changeSlotCount += SlotChange;
        Inven.SetActive(false);
        MiniCam_close = MiniCam_Close.instance;
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCount)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInvebtory = !OpenInvebtory;
            Inven.SetActive(OpenInvebtory);
            if (inven.SlotCount == 0)
            {
                inven.SlotCount = 4;
            }
        }
        //if (OpenInvebtory == true)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
    }
    public void AddSlot()
    {
        inven.SlotCount += 4;
    }

    public void AddSlotItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.ItemName == _item.ItemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
    
    public void OpenInven()
    {
        OpenInvebtory = true;
        Inven.SetActive(OpenInvebtory);
        MiniCam_close.Closemap();
        if (inven.SlotCount == 0)
        {
            inven.SlotCount = 4;
        }
    }
    public void CloseInven()
    {
        OpenInvebtory = false;
        Inven.SetActive(OpenInvebtory);
        MiniCam_close.OpenMap();
    }
}
