using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    private Image Hp;
    private Image Effect;
    [SerializeField] private float curPlayerHp;
    [SerializeField] private float maxPlayerHp;
    private void Awake()
    {
        Hp = transform.Find("HP").GetComponent<Image>();
        Effect = transform.Find("Effect").GetComponent<Image>();
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
        if (Hp.fillAmount < Effect.fillAmount)//�������� �Ծ� ü���� ����������
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //����Ʈ�� ���� �κ��� ���ҵ�

            if (Effect.fillAmount <= Hp.fillAmount)//���� Hp������ �۾��� �����ٸ�
            {
                Effect.fillAmount = Hp.fillAmount;//����Ʈ�� Hp������ ����
            }
        }
        float value = curPlayerHp / maxPlayerHp;//���� hp�� ����
        if (Hp.fillAmount > value) //�������� ����
        {
            Hp.fillAmount -= Time.deltaTime*0.2f;
            if (Hp.fillAmount <= value)
            {
                Hp.fillAmount = value;
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
    public void SetPlayerHp(int _hp, int _maxHp)
    {
        curPlayerHp = _hp;
        maxPlayerHp = _maxHp;
    }
}

