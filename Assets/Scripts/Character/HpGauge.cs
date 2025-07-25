using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    private Image Hp;
    private Image Effect;
    [SerializeField] public float curPlayerHp;
    [SerializeField] private float maxPlayerHp;
    public static HpGauge instance;
    private void Awake()
    {
        Hp = transform.Find("HP").GetComponent<Image>();
        Effect = transform.Find("Effect").GetComponent<Image>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
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
        if(curPlayerHp>maxPlayerHp)
        {
            curPlayerHp = maxPlayerHp;
        }
        if (Hp.fillAmount < Effect.fillAmount)//�������� �Ծ� ü���� ����������
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //����Ʈ�� ���� �κ��� ���ҵ�

            if (Effect.fillAmount <= Hp.fillAmount)//���� Hp������ �۾��� �����ٸ�
            {
                Effect.fillAmount = Hp.fillAmount;//����Ʈ�� Hp������ ����
            }
        }
        else if (Hp.fillAmount > Effect.fillAmount)//ü���� ȸ��������
        {
            Effect.fillAmount = Hp.fillAmount;//����Ʈ�� hp ������ ����
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
        else if (Hp.fillAmount < value)//ȸ���Ǿ�����
        {
            Hp.fillAmount += Time.deltaTime;
            if (Hp.fillAmount >= value)
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
    public void BtnOn_Hp()
    {
        gameObject.SetActive(true);
    }
    public void BtnOff_Hp()
    {
        gameObject.SetActive(false);
    }
    public void IncreaseMaxHp(int amount)
    {
        maxPlayerHp += amount;
        curPlayerHp += amount;

        if (curPlayerHp > maxPlayerHp)
            curPlayerHp = maxPlayerHp;
    }
}

