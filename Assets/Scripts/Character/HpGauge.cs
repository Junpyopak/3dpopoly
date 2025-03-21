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
        if (Hp.fillAmount < Effect.fillAmount)//데미지를 입어 체력이 감소했을때
        {
            Effect.fillAmount -= Time.deltaTime * 0.1f; //이펙트의 붉은 부분이 감소됨

            if (Effect.fillAmount <= Hp.fillAmount)//만약 Hp값보다 작아져 버린다면
            {
                Effect.fillAmount = Hp.fillAmount;//이펙트를 Hp값으로 변경
            }
        }
        else if (Hp.fillAmount > Effect.fillAmount)//체력을 회복했을때
        {
            Effect.fillAmount = Hp.fillAmount;//이팩트를 hp 값으로 변경
        }
        float value = curPlayerHp / maxPlayerHp;//남은 hp의 비율
        if (Hp.fillAmount > value) //데미지를 입음
        {
            Hp.fillAmount -= Time.deltaTime*0.2f;
            if (Hp.fillAmount <= value)
            {
                Hp.fillAmount = value;
            }
        }
        else if (Hp.fillAmount < value)//회복되었을때
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
}

