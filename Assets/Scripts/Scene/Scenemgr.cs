using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scenemgr : MonoBehaviour
{
    private void Awake()
    {
        Shared.Scenemgr = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    Shared.InitTableMgr();

    //    Table_Character.Info info = Shared.TableMgr.Character.Get(1);
    //    Table_Item.Info info1 = Shared.TableMgr.Item.Get(1);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
