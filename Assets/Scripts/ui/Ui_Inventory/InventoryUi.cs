using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    Inventory inven;
    public GameObject Inven;
    private bool OpenInvebtory = false;
    public Slot[] slots;
    public Transform SlotHolder;
    // Start is called before the first frame update
    void Start()
    {
        inven = Inventory.instance;
        slots = GetComponentsInChildren<Slot>();
        inven.changeSlotCount += SlotChange;
        Inven.SetActive(false);
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i<inven.SlotCount)
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
        }
        if (OpenInvebtory == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void AddSlot()
    {
        inven.SlotCount+=4;
    }
}
