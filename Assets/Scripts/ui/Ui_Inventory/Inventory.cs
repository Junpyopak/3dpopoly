using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public delegate void ChangeSlotCount(int val);
    public ChangeSlotCount changeSlotCount;

  
    private int slotCount ;
    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
            changeSlotCount(slotCount);
        }
    }
    private void Start()
    {
        slotCount = 0;
        //SlotCount = slotCount;
    }
    
}
