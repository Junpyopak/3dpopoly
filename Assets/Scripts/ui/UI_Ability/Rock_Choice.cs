using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Choice : MonoBehaviour
{
    // Start is called before the first frame update
    private CamFollowUi CamfollowUi;
    void Start()
    {
        CamfollowUi = FindObjectOfType<CamFollowUi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenCardUI()
    {
        gameObject.SetActive(true);
        CamfollowUi.isSelectingCard = true;
    }

    public void CloseCardUI()
    {
        gameObject.SetActive(false);
        CamfollowUi.isSelectingCard = false;
    }
}
