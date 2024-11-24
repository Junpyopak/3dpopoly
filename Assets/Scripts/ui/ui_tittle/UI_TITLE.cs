using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TITLE : MonoBehaviour
{
    
    //상속을 쓰면 tag 안써도됌
    public class actor
    { }

    public class Character : actor
    { }

    public class Player : Character { }

    public void onBtnTitle()
    {
      Shared.Scenemgr.ChangeScene(eSCENE.LOGIN);
    }
}
