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
    int MaxLevel = 15;
    public bool Success = false;
    public bool Failed = false;
    public GameObject resultText;
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
        if(Level<MaxLevel)
        {
            if(Level<8)
            {
                if (ran < 5)
                {
                    Success = true;
                    Level++;
                    Debug.Log("Succes!!!");
                    level.text = $"Lv.{((int)Level)} ";
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Success = false;
                }
                else if (ran < 8)
                {
                    Failed = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Failed = false;
                }
                else if (ran < 9)
                {
                    Failed = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Failed = false;
                }
                else if (ran < 10)
                {
                    Failed = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Failed = false;
                }
            }
            else if(Level<15)
            {
                if (ran < 5)
                {
                    Failed = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Failed = false;
                }
                else if (ran < 8)
                {
                    Success = true;
                    Level++;
                    Debug.Log("Succes!!!");
                    level.text = $"Lv.{((int)Level)} ";
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Success = false;
                }
                //else if (ran < 9)
                //{
                //    Level++;
                //    Debug.Log("Succes!!!");
                //    level.text = $"Lv.{((int)Level)} ";
                   
                //}
                else if (ran < 10)
                {
                    Debug.Log("강화레벨 하락");
                    Level--;
                    level.text = $"Lv.{((int)Level)} ";
                }
            }
            
        }
        else
        {
            Debug.Log("장비 레벨이 최대치입니다.");
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
