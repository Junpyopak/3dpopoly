using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TITLE : MonoBehaviour
{
    
    //����� ���� tag �Ƚᵵ��
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
