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
    private Player player;
    private HpGauge PlayerHpGauge;
    private void Start()
    {
        player = GameObject.Find("character").GetComponent<Player>();
        PlayerHpGauge = GameObject.Find("PlayerHp").GetComponent<HpGauge>();

    }
    public void Useitem(Item _item)
    {
        if(_item.itemType==Item.ItemType.Used)
        {
            if(_item.ItemName == "�߱� ����")
            {
                player.Hp += 30;
                PlayerHpGauge.curPlayerHp = player.Hp;
                Debug.Log("ü���� ȸ���Ǿ����ϴ�.");
            }
            if(_item.ItemName == "�ϱ� ����")
            {
                player.Hp += 15;
                PlayerHpGauge.curPlayerHp = player.Hp;
                Debug.Log("ü���� ȸ���Ǿ����ϴ�.");
            }
        }
    }
}
