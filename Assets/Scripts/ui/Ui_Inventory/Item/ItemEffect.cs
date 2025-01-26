using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Effect
{
    public string itemName;
    public string[] effectPos;
    public int[]EffectValue;
}

public class ItemEffect : MonoBehaviour
{
    public ParticleSystem MyparticleSystem;
    private Player player;
    private HpGauge PlayerHpGauge;
    private void Start()
    {
        player = GameObject.Find("character").GetComponent<Player>();
        PlayerHpGauge = GameObject.Find("PlayerHp").GetComponent<HpGauge>();

    }
    IEnumerator EffectOff()
    {
        yield return new WaitForSeconds(3f);
        MyparticleSystem.Stop();
    }
    IEnumerator Heal(int _hp)
    {
        yield return new WaitForSeconds(1f);
        player.Hp += _hp;
        PlayerHpGauge.curPlayerHp = player.Hp;
    }
    public void Useitem(Item _item)
    {
        if(_item.itemType==Item.ItemType.Used)
        {
            if(_item.ItemName == "중급 포션")
            {
                MyparticleSystem.Play();
                StartCoroutine(EffectOff());
                StartCoroutine(Heal(30));
                Debug.Log("체력이 회복되었습니다.");
            }
            if(_item.ItemName == "하급 포션")
            {
                MyparticleSystem.Play();
                StartCoroutine(EffectOff());
                StartCoroutine (Heal(15));
                Debug.Log("체력이 회복되었습니다.");
            }
        }
    }
}
