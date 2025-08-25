using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    Inventory inven;
    MiniCam_Close MiniCam_close;
    HpGauge hpGauge;
    public GameObject Inven;
    private bool OpenInvebtory = false;
    public Slot[] slots;
    public Transform SlotHolder;
    public GameObject CharacterInfo;
    public GameObject Chatting;
    public GameObject SkillCan;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (slots == null || slots.Length == 0)
        {
            slots = GetComponentsInChildren<Slot>();
            Debug.Log($"[InventoryUi] 슬롯 자동 연결됨: {slots.Length}개");
        }
    }
    void Start()
    {
        inven = Inventory.instance;
        slots = GetComponentsInChildren<Slot>();
        inven.changeSlotCount += SlotChange;
        Inven.SetActive(false);
        MiniCam_close = MiniCam_Close.instance;
        hpGauge = HpGauge.instance;
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
            if (CharacterInfo != null)
                CharacterInfo.SetActive(!OpenInvebtory);
            if(Chatting!=null)
                Chatting.SetActive(!OpenInvebtory);
            if (SkillCan != null)
                SkillCan.SetActive(!OpenInvebtory);

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

    //public void AddSlotItem(Item _item, int _count = 1)
    //{
    //    if (Item.ItemType.Equipment != _item.itemType)
    //    {
    //        for (int i = 0; i < slots.Length; i++)
    //        {
    //            if (slots[i].item != null)
    //            {
    //                if (slots[i].item.ItemName == _item.ItemName)
    //                {
    //                    slots[i].SetSlotCount(_count);
    //                    return;
    //                }
    //            }
    //        }
    //    }

    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (slots[i].item == null)
    //        {
    //            slots[i].AddItem(_item, _count);
    //            return;
    //        }
    //    }
    //}
    public void AddSlotItem(Item _item, int _count = 1)
    {
        Debug.Log($"[AddSlotItem] 슬롯 수: {(slots != null ? slots.Length : -1)}");

        if (slots == null )
        {
            Debug.LogError(" [AddSlotItem] 슬롯 배열이 비어 있음!");
            return;
        }

        Debug.Log($"[AddSlotItem] 요청된 아이템: {_item?.ItemName}, 수량: {_count}");

        if (slots == null)
        {
            Debug.LogError("[AddSlotItem] slots 배열이 null 또는 비어 있습니다!");
            return;
        }

        if (_item == null)
        {
            Debug.LogError("[AddSlotItem] 전달된 Item이 null입니다!");
            return;
        }

        // 중복 아이템 수량 증가
        if (_item.itemType != Item.ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.ItemName == _item.ItemName)
                {
                    Debug.Log($"[AddSlotItem] 기존 슬롯[{i}]에서 수량 증가");
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        // 빈 슬롯에 추가
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                Debug.Log($"[AddSlotItem] 빈 슬롯[{i}]에 아이템 추가");
                slots[i].AddItem(_item, _count);
                return;
            }
        }

        Debug.LogWarning("[AddSlotItem] 빈 슬롯 없음, 추가 실패");
    }


    public void OpenInven()
    {
        OpenInvebtory = true;
        Inven.SetActive(OpenInvebtory);
        MiniCam_close.Closemap();
        hpGauge.BtnOff_Hp();
        if (CharacterInfo != null)
            CharacterInfo.SetActive(false);
        if (Chatting != null)
            Chatting.SetActive(false);
        if (SkillCan != null)
            SkillCan.SetActive(false);

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
        hpGauge.BtnOn_Hp();
        if (CharacterInfo != null)
            CharacterInfo.SetActive(true);
        if (Chatting != null)
            Chatting.SetActive(true);
        if (SkillCan != null)
            SkillCan.SetActive(true);
    }
}
