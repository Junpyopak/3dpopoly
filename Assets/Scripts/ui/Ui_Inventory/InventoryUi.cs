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
            Debug.Log($"[InventoryUi] ���� �ڵ� �����: {slots.Length}��");
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
        Debug.Log($"[AddSlotItem] ���� ��: {(slots != null ? slots.Length : -1)}");

        if (slots == null )
        {
            Debug.LogError(" [AddSlotItem] ���� �迭�� ��� ����!");
            return;
        }

        Debug.Log($"[AddSlotItem] ��û�� ������: {_item?.ItemName}, ����: {_count}");

        if (slots == null)
        {
            Debug.LogError("[AddSlotItem] slots �迭�� null �Ǵ� ��� �ֽ��ϴ�!");
            return;
        }

        if (_item == null)
        {
            Debug.LogError("[AddSlotItem] ���޵� Item�� null�Դϴ�!");
            return;
        }

        // �ߺ� ������ ���� ����
        if (_item.itemType != Item.ItemType.Equipment)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.ItemName == _item.ItemName)
                {
                    Debug.Log($"[AddSlotItem] ���� ����[{i}]���� ���� ����");
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        // �� ���Կ� �߰�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                Debug.Log($"[AddSlotItem] �� ����[{i}]�� ������ �߰�");
                slots[i].AddItem(_item, _count);
                return;
            }
        }

        Debug.LogWarning("[AddSlotItem] �� ���� ����, �߰� ����");
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
