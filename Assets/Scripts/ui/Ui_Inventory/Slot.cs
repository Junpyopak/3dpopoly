using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Button slotButton;
    // Start is called before the first frame update
    void Start()
    {
        slotButton = GetComponent<Button>();
        slotButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
