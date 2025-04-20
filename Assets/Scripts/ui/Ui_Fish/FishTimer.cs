using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FishTimer : MonoBehaviour
{
    public static FishTimer instance;
    private Slider Slider;
    private float sliderSpeed = 200f;
    public float MinPos;
    public float MaxPos;
    int fishCnt = 0;
    int MaxCnt = 3;
    private bool Failed = false;
    public bool Succes = false;
    public List<GameObject> FishingItems;
    [SerializeField]
    public Text fishText;
    private InventoryUi inventoryUi;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        Slider = GetComponent<Slider>();
        fishText.text = $"{((int)fishCnt)}/{MaxCnt}";
        Slider.value = 0;
        inventoryUi = GameObject.Find("InvenCanvas").GetComponent<InventoryUi>();
    }
    // Update is called once per frame
    void Update()
    {
        Fishing();
        if (fishCnt >= MaxCnt)
        {
            fishText.text = "¼º°ø!!";
            Succes = true;
            Slider.value = Slider.value;
            StartCoroutine(Failfish());
            DropFish();
            fishCnt = 0;
        }
    }
    private void Fishing()
    {
        MinPos = 115f;
        MaxPos = 180f;
        if (Slider.value <= Slider.maxValue && Failed == false && Succes == false)
        {
            Slider.value += Time.deltaTime * sliderSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Slider.value >= MinPos && Slider.value <= MaxPos)
                {
                    fishCnt++;
                    fishText.text = $"{((int)fishCnt)}/{MaxCnt}";
                    Debug.Log($"{fishCnt}");


                }
                else if (Slider.value != MinPos && Slider.value != MaxPos)
                {
                    Failed = true;
                    fishText.text = "<color=red>" + "½ÇÆÐ..." + "</color>";
                    Slider.value = Slider.value;
                    StartCoroutine(Failfish());
                    // gameObject.SetActive(false);
                }
            }
            if (Slider.value == Slider.maxValue)
            {
                Slider.value = 0;
                return;
            }
        }
    }
    IEnumerator Failfish()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.Find("fishing"));
    }
    private void DropFish()
    {
        //if (inventoryUi == null)
        //{
        //    inventoryUi = GameObject.Find("InvenCanvas").GetComponent<InventoryUi>();
        //    if (inventoryUi == null)
        //    {
        //        Debug.LogError("[DropFish] inventoryUi°¡ ÇÒ´çµÇÁö ¾Ê¾Ò½À´Ï´Ù!");
        //        return;
        //    }
        //}
        int ran = Random.Range(0, 10);
        if (ran < 9)
        {
            Debug.Log(FishingItems[0].transform.GetComponent<GetItem>().item.ItemName + "È¹µæ");
            inventoryUi.AddSlotItem(FishingItems[0].transform.GetComponent<GetItem>().item);
        }
        else if (ran < 10)
        {
            Debug.Log(FishingItems[1].transform.GetComponent<GetItem>().item.ItemName + "È¹µæ");
            inventoryUi.AddSlotItem(FishingItems[1].transform.GetComponent<GetItem>().item);
        }
    }
}
