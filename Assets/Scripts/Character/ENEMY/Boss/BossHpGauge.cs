using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpGauge : MonoBehaviour
{
    private Image HP;
    private Image Effect;
    [SerializeField] private float CurBossHp;
    [SerializeField] private float MaxBossHp;
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
        if (HP.fillAmount < Effect.fillAmount)//�������� �Ծ� ü���� ����������
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //����Ʈ�� ���� �κ��� ���ҵ�

            if (Effect.fillAmount <= HP.fillAmount)//���� Hp������ �۾��� �����ٸ�
            {
                Effect.fillAmount = HP.fillAmount;//����Ʈ�� Hp������ ����
            }
        }
        float value = CurBossHp / MaxBossHp;//���� hp�� ����
        if (HP.fillAmount > value) //�������� ����
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
            Destroy(gameObject);
        }
    }
    public void SetHp(int _hp, int _maxHp)
    {
        CurBossHp = _hp;
        MaxBossHp = _maxHp;
    }
}
