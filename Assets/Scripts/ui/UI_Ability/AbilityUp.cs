using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUp : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("character").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Abillity_Upstate()
    {
        Debug.Log("���ݷ� ����");
        player.AttackDamage += 7;
        gameObject.SetActive(false);
    }
    public void Abillity_HpUpstate()
    {
        Debug.Log("ü�� ����");
        HpGauge.instance.IncreaseMaxHp(20);
        player.IncreaseMaxHp(20);
        gameObject.SetActive(false);
    }
}
