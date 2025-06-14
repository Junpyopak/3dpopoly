using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    public Image HP;
    public Image Effect;
    public Text Monster_import;
    [SerializeField] public float CurEnemyHp;
    [SerializeField] public float MaxEnemyHp;
    private void Awake()
    {
        HP = transform.Find("HpBar").GetComponent<Image>();
        Effect = transform.Find("HpEffect").GetComponent<Image>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HpCheck();
        isDestorying();
    }
    public void HpCheck()
    {

        if (HP.fillAmount < Effect.fillAmount)//데미지를 입어 체력이 감소했을때
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //이펙트의 붉은 부분이 감소됨

            if (Effect.fillAmount <= HP.fillAmount)//만약 Hp값보다 작아져 버린다면
            {
                Effect.fillAmount = HP.fillAmount;//이펙트를 Hp값으로 변경
            }
        }
        float value = CurEnemyHp / MaxEnemyHp;//남은 hp의 비율
        Monster_import.text = $"숲의 망령 {((int)CurEnemyHp).ToString("D2")} / {((int)MaxEnemyHp).ToString("D2")} ";
        if (HP.fillAmount > value) //데미지를 입음
        {
            HP.fillAmount -= Time.deltaTime * 0.35f;
            if (HP.fillAmount <= value)
            {
                HP.fillAmount = value;
            }
        }
    }
    private void isDestorying()
    {
        if (Effect.fillAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetHp(int _hp, int _maxHp)
    {
        CurEnemyHp = _hp;
        MaxEnemyHp = _maxHp;
    }

}
