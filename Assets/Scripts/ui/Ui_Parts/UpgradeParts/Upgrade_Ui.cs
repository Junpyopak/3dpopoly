using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UI_TITLE;

public class Upgrade_Ui : MonoBehaviour
{
    public UpgradeSlot[] slots;
    [SerializeField] List<GameObject> listWeapon;
    [SerializeField]
    private Text level;
    [SerializeField]
    private Text atkText;
    int Level=0;
    int MaxLevel = 15;
    int Atkup = 0;
    public GameObject resultText;
    [SerializeField]
    Upgrade_Text Upgrade_Text;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<UpgradeSlot>();
        player = GameObject.Find("character").GetComponent<Player>();
        DoPickup();
        int _count = 8;
        Recursion(_count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Recursion(int _Count)
    {
        if (_Count <= 0)
        {
            return;
        }

        else
        {
            UpgaradeParts();
            Debug.Log("재귀호풀");
            // Recursion(_Count);
            Recursion(_Count - 1);
        }
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
                    Upgrade_Text.isSucces = true;
                    Level++;
                    Atkup += 2;
                    Debug.Log("Succes!!!");
                    Debug.Log($" + ({(Atkup)})");
                    player.AttackDamage += 2;
                    atkText.text = $"공격력 : 20 (+{(Atkup)})";
                    level.text = $"Lv.{((int)Level)} ";
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Upgrade_Text.isSucces = false;
                }
                else if (ran < 8)
                {
                    Upgrade_Text.isFail = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Upgrade_Text.isFail = false;
                }
                else if (ran < 9)
                {
                    Upgrade_Text.isFail = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Upgrade_Text.isFail = false;
                }
                else if (ran < 10)
                {
                    Upgrade_Text.isFail = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Upgrade_Text.isFail = false;
                }
            }
            else if(Level<15)
            {
                if (ran < 5)
                {
                    Upgrade_Text.isFail = true;
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Debug.Log("Fail!!!");
                    Upgrade_Text.isFail = false;
                }
                else if (ran < 8)
                {
                    Upgrade_Text.isSucces = true;
                    Level++;
                    Atkup += 2;
                    Debug.Log("Succes!!!");
                    atkText.text = $"공격력 : 20 (+{(Atkup)})";
                    player.AttackDamage += 2;
                    level.text = $"Lv.{((int)Level)} ";
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Upgrade_Text.isSucces = false;
                }
                //else if (ran < 9)
                //{
                //    Level++;
                //    Debug.Log("Succes!!!");
                //    level.text = $"Lv.{((int)Level)} ";
                   
                //}
                else if (ran < 10)
                {
                    Upgrade_Text.Downgrade = true;
                    Debug.Log("강화레벨 하락");
                    Level--;
                    Atkup -= 2;
                    player.AttackDamage -= 2;
                    atkText.text = $"공격력 : 20 (+{(Atkup)})";
                    level.text = $"Lv.{((int)Level)} ";
                    GameObject text = Instantiate(resultText);
                    resultText.GetComponent<Upgrade_Text>();
                    Upgrade_Text.Downgrade = false;
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
