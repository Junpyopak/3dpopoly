using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMap_Set : MonoBehaviour
{
    public static WorldMap_Set instance;
    Vector3 MousePos;
    Camera WorldCam;
    RaycastHit hit;
    public GameObject Target;
    public GameObject TelPos;
    public bool TelPort = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        WorldCam = GameObject.Find("World_miniCam").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = WorldCam.ScreenPointToRay(Input.mousePosition);
            MousePos = Input.mousePosition;
            MousePos = WorldCam.ScreenToWorldPoint(MousePos);
            Debug.Log(MousePos);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Telport")
                {
                    Debug.Log("ÀÌµ¿");
                    //TelPort = true;
                    Target.transform.position = TelPos.transform.position;
                }
            }
        }
    }
    public void Btn_MapOpen()
    {
        Debug.Log("¿ùµå¸Ê ¿ÀÇÂ");
        gameObject.SetActive(true);
    }
    public void Btn_MapClose()
    {
        gameObject.SetActive(false);
    }
}
