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

        if (HP.fillAmount < Effect.fillAmount)//�������� �Ծ� ü���� ����������
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //����Ʈ�� ���� �κ��� ���ҵ�

            if (Effect.fillAmount <= HP.fillAmount)//���� Hp������ �۾��� �����ٸ�
            {
                Effect.fillAmount = HP.fillAmount;//����Ʈ�� Hp������ ����
            }
        }
        float value = CurEnemyHp / MaxEnemyHp;//���� hp�� ����
        Monster_import.text = $"���� ���� {((int)CurEnemyHp).ToString("D2")} / {((int)MaxEnemyHp).ToString("D2")} ";
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
            gameObject.SetActive(false);
        }
    }
    public void SetHp(int _hp, int _maxHp)
    {
        CurEnemyHp = _hp;
        MaxEnemyHp = _maxHp;
    }

}
