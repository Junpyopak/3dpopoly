using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public partial class Scenemgr : MonoBehaviour
{
    eSCENE Scene = eSCENE.TITLE;

    Dictionary<int,Character> CharacterMap = new Dictionary<int,Character>();
   
    public void ChangeScene(eSCENE _e, bool _lodding = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        switch (_e)
        {
            case eSCENE.TITLE:
                SceneManager.LoadScene("TITLE");
                break;
            case eSCENE.LOGIN:
                SceneManager.LoadScene("LOGIN");
                break;
            case eSCENE.LOBBY:
                SceneManager.LoadScene("LOBBY");
                break;
            case eSCENE.BATTLE:
                SceneManager.LoadScene("BATTLE");
                break;

        }


    }
    public Character GetCharacter(int Index)//����������� add�����

    {
        if(CharacterMap.ContainsKey(Index))
            return CharacterMap[Index];

        CharacterMap.Remove(Index);//�ش�Ű�� ����
        CharacterMap.Clear();//���λ���


        var pair = CharacterMap.GetEnumerator();

        while (pair.MoveNext())//��ü �� ���鼭 �����Ϳ� ����
        {
           Character character = pair.Current.Value;
        }

        return null;
    }
}
