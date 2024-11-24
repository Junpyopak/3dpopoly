using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Scenemgr : MonoBehaviour
{
    //레지스트리,플레이어 아이디 존재유무에 따른 로그인
    public void SetPlayerPrefsIntKey(string _key, int _value)//PlayerPrefs 는 int,float,string 사용
    {
        PlayerPrefs.SetInt(_key, _value);
        PlayerPrefs.Save();
    }

    public int GetPlayerPrefsIntKey(string _key)
    {
        return PlayerPrefs.GetInt(_key);
    }

    public void SetPlayerPrefsFloatKey(string _key, float _value)
    {
        PlayerPrefs.SetFloat(_key, _value);
        PlayerPrefs.Save();
    }

    public float GetPlayerPrefsFloatKey(string _key)
    {
        return PlayerPrefs.GetFloat(_key);
    }

    public void SetPlayerPrefsStringKey(string _key, string _value)
    {
        PlayerPrefs.SetString(_key, _value);
        PlayerPrefs.Save();
    }
    public string GetPlayerPrefsStringKey(string _key)
    {
        return PlayerPrefs.GetString(_key);
    }

}
