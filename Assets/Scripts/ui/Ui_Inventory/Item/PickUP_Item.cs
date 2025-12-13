using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PickUP_Item : MonoBehaviour
{
    //[SerializeField]
    //private float Range;
    //private bool PickItem = false;
    //private RaycastHit hit;

    //[SerializeField]
    //LayerMask layerMask;
    //Vector3 itemPick = new Vector3(0f, 0.1f, 1f);
    //[SerializeField]
    //private Text ActionText;
    //[SerializeField]
    //private InventoryUi inventoryUi;
    //Vector3 fising = new Vector3(0f, -7f, 10f);
    //[SerializeField]
    //GameObject FishSlider;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckItem();
    //    GetAction();
    //    Draw();
    //    WaterRay();
    //}
    //private void GetAction()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        CheckItem();
    //        DoPickup();
    //    }
    //}

    //private void DoPickup()
    //{
    //    if (PickItem)
    //    {
    //        if (hit.transform != null)
    //        {
    //            Debug.Log(hit.transform.GetComponent<GetItem>().item.ItemName + "»πµÊ");
    //            inventoryUi.AddSlotItem(hit.transform.GetComponent<GetItem>().item);
    //            Destroy(hit.transform.gameObject);
    //            ItemInfoDis();
    //        }
    //    }
    //}
    //private void CheckItem()
    //{
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Range, layerMask))
    //    {
    //        if (hit.transform.tag == "Item")
    //        {
    //            ItemInfo();
    //        }

    //    }
    //    else
    //        ItemInfoDis();
    //}
    //private void ItemInfo()
    //{
    //    PickItem = true;
    //    ActionText.gameObject.SetActive(true);
    //    ActionText.text = hit.transform.GetComponent<GetItem>().item.ItemName + "»πµÊ«œ±‚" + "<color=yellow>" + "(E)" + "</color>";
    //}
    //private void ItemInfoDis()
    //{
    //    PickItem = false;
    //    ActionText.gameObject.SetActive(false);
    //}
    //private void  Draw()
    //{
    //    Debug.DrawRay(transform.position, transform.TransformDirection(itemPick),Color.red);
    //}
    //private void WaterRay()
    //{
    //    Debug.DrawRay(transform.position, transform.TransformDirection(fising), Color.blue);
    //    if(Physics.Raycast(transform.position, transform.TransformDirection(fising), out hit))
    //    {
    //        if (hit.transform.tag == "Water")
    //        {
    //            StartFishing();
    //            Debug.Log("≥¨Ω√ ∞°¥…");
    //        }

    //        else
    //        {
    //            DisFising();
    //        }
    //    }
    //}
    //private void StartFishing()
    //{
    //    ActionText.gameObject.SetActive(true);
    //    ActionText.text = "≥¨Ω√«œ±‚" + "<color=yellow>" + "(F)" + "</color>";
    //    if(Input.GetKeyDown(KeyCode.F))
    //    {
    //        GameObject go = Instantiate(FishSlider);
    //        go.name = "fishing";

    //    }
    //}
    //void DisFising()
    //{
    //    ActionText.gameObject.SetActive(false);
    //}
    [SerializeField]
    private float Range;
    private bool PickItem = false;
    private RaycastHit itemHit;  // æ∆¿Ã≈€ ¿¸øÎ
    private RaycastHit waterHit; // ≥¨Ω√ ¿¸øÎ

    [SerializeField] LayerMask layerMask;
    Vector3 itemPick = new Vector3(0f, 0.1f, 1f);
    [SerializeField] private Text ActionText;
    [SerializeField] private InventoryUi inventoryUi;
    Vector3 fising = new Vector3(0f, -7f, 10f);
    [SerializeField] GameObject FishSlider;

    void Update()
    {
        CheckItem();
        WaterRay();
        Draw();
        GetAction();
    }

    private void GetAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && PickItem)
        {
            DoPickup();
        }
    }

    private void DoPickup()
    {
        if (PickItem && itemHit.transform != null)
        {
            Debug.Log(itemHit.transform.GetComponent<GetItem>().item.ItemName + " »πµÊ");
            inventoryUi.AddSlotItem(itemHit.transform.GetComponent<GetItem>().item);
            Destroy(itemHit.transform.gameObject);
            ItemInfoDis();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out itemHit, Range, layerMask))
        {
            if (itemHit.transform.CompareTag("Item"))
            {
                ItemInfo();
                return; // æ∆¿Ã≈€¿Ã ¿÷¿∏∏È ≥¨Ω√¥¬ π´Ω√
            }
        }

        ItemInfoDis(); // æ∆¿Ã≈€¿Ã æ¯¿∏∏È ≈ÿΩ∫∆Æ ≤Ù±‚
    }

  

    private void ItemInfo()
    {
        PickItem = true;
        if (ActionText != null)
        {
            ActionText.gameObject.SetActive(true);
           // ActionText.text = itemHit.transform.GetComponent<GetItem>().item.ItemName + " »πµÊ«œ±‚ <color=yellow>(E)</color>";
            ActionText.text = itemHit.transform.GetComponent<GetItem>().item.ItemName + "»πµÊ«œ±‚" + "<color=yellow>" + "(E)" + "</color>";
        }
    }

    private void ItemInfoDis()
    {
        PickItem = false;
        if (ActionText != null)
            ActionText.gameObject.SetActive(false);
    }

    private void WaterRay()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(fising), Color.blue);

        // æ∆¿Ã≈€¿Ã æ¯¿ª ∂ß∏∏ ≥¨Ω√ Ray √º≈©
        if (!PickItem && Physics.Raycast(transform.position, transform.TransformDirection(fising), out waterHit))
        {
            if (waterHit.transform.CompareTag("Water"))
            {
                StartFishing();
                Debug.Log("≥¨Ω√ ∞°¥…");
            }
            else
            {
                DisFising();
            }
        }
        else if (!PickItem)
        {
            DisFising();
        }
    }

    private void StartFishing()
    {
        if (ActionText != null)
        {
            ActionText.gameObject.SetActive(true);
            ActionText.text = "≥¨Ω√«œ±‚ <color=yellow>(F)</color>";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject go = Instantiate(FishSlider);
            go.name = "fishing";
        }
    }

    void DisFising()
    {
        if (!PickItem && ActionText != null)
            ActionText.gameObject.SetActive(false);
    }

    private void Draw()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(itemPick), Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(fising), Color.blue);
    }

}
