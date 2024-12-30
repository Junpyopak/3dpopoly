using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpGauge : MonoBehaviour
{
    private Image HP;
    private Image Effect;
    [SerializeField] private float CurPlayerHp;
    [SerializeField] private float MaxPlayerHp;
    private void Awake()
    {
        HP = GameObject.Find("HpBar").GetComponent<Image>();
        Effect = GameObject.Find("HpEffect").GetComponent<Image>();
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
        float value = CurPlayerHp / MaxPlayerHp;//남은 hp의 비율
        if (HP.fillAmount > value) //데미지를 입음
        {
            HP.fillAmount -= Time.deltaTime * 0.2f;
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
            Destroy(gameObject);
        }
    }
    public void SetHp(int _hp, int _maxHp)
    {
        CurPlayerHp = _hp;
        MaxPlayerHp = _maxHp;
    }
}
