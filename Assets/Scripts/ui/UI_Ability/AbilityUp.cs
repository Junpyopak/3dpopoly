using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUp : MonoBehaviour
{
    private Player player;
    private GameObject character;
    public GameObject Attackuppart;
    public GameObject HP_upPart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("character").GetComponent<Player>();
        character = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Abillity_Upstate()
    {
        Debug.Log("공격력 증가");
        Debug.Log(character.transform.position);
        player.AttackDamage += 7;

        if (Attackuppart != null)
        {
            Vector3 spawnPos = character.transform.position + new Vector3(0.5f, 0f, 0f);
            GameObject particle = Instantiate(Attackuppart, spawnPos, character.transform.rotation);
            Destroy(particle, 2f);
        }

        gameObject.SetActive(false);
    }

    public void Abillity_HpUpstate()
    {
        Debug.Log("체력 증가");
        HpGauge.instance.IncreaseMaxHp(20);
        player.IncreaseMaxHp(20);

        if (HP_upPart != null)
        {
            Vector3 spawnPos = character.transform.position + new Vector3(0.5f, 0f, 0f);
            GameObject particle = Instantiate(HP_upPart, spawnPos, character.transform.rotation);
            Destroy(particle, 2f);
        }

        gameObject.SetActive(false);
    }
}
