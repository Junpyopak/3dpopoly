using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUP_Item : MonoBehaviour
{
    [SerializeField]
    private float Range;
    private bool PickItem = false;
    private RaycastHit hit;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    private Text ActionText;
    [SerializeField]
    private InventoryUi inventoryUi;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        GetAction();
    }
    private void GetAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            DoPickup();
        }
    }

    private void DoPickup()
    {
        if (PickItem)
        {
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.GetComponent<GetItem>().item.ItemName + "»πµÊ");
                inventoryUi.AddSlotItem(hit.transform.GetComponent<GetItem>().item);
                Destroy(hit.transform.gameObject);
                ItemInfoDis();
            }
        }
    }
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Range, layerMask))
        {
            if (hit.transform.tag == "Item")
            {
                ItemInfo();
            }
         
        }
        else
            ItemInfoDis();
    }
    private void ItemInfo()
    {
        PickItem = true;
        ActionText.gameObject.SetActive(true);
        ActionText.text = hit.transform.GetComponent<GetItem>().item.ItemName + "»πµÊ«œ±‚" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDis()
    {
        PickItem = false;
        ActionText.gameObject.SetActive(false);
    }
}
