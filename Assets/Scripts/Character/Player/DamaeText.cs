using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamaeText : Enemy
{
    Text TextDamage;
    Color alpha;
    public float UpSpeed = 3;
    public float ColorSpeed = 3;
    public int Damage;
    // Start is called before the first frame update
    Enemy Enemy;
    void Start()
    {
        Enemy = GetComponent<Enemy>();
        Damage = AttackDamage;
        TextDamage = GetComponent<Text>();
        alpha = TextDamage.color;
        TextDamage.text = $"{((int)Damage).ToString("D1")} ";
        Invoke("DeleteText", ColorSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, UpSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * ColorSpeed);
        TextDamage.color = alpha;
        //DamageTextLog();
    }

    //public void DamageTextLog()
    //{
    //    TextDamage.text = $"{((int)Damage).ToString("D1")} ";
    //}
    private void DeleteText()
    {
        Destroy(gameObject);
    }
    //public void GetDamage(int _damage)
    //{
    //    Damage = _damage;
    //}
}
