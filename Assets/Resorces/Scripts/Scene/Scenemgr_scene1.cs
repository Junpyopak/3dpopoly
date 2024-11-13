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
    public Character GetCharacter(int Index)//집어넣을때는 add를사용

    {
        if(CharacterMap.ContainsKey(Index))
            return CharacterMap[Index];

        CharacterMap.Remove(Index);//해당키만 삭제
        CharacterMap.Clear();//전부삭제


        var pair = CharacterMap.GetEnumerator();

        while (pair.MoveNext())//전체 다 돌면서 데이터에 접근
        {
           Character character = pair.Current.Value;
        }

        return null;
    }
}
