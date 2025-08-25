using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS : Enemy
{
    // Start is called before the first frame update
    public static BOSS instance;
    Player Player;
    Animator Animator;
    [SerializeField] BossHpGauge BossHpgauge;
    [SerializeField] List<GameObject> listPattern;//패턴의 종류
    public ParticleSystem MyparticleSystem;
    public ParticleSystem ShiledParticle;
    public StartAttack startAttack;
    [Header("보스 패턴")]

    [SerializeField] int pattern1Count = 1;
    [SerializeField] float pattern1Reload = 5f;

    [SerializeField] int pattern2Count = 1;
    [SerializeField] float pattern2Reload = 7f;

    [SerializeField] int pattern3Count = 1;
    [SerializeField] float pattern3Reload = 7f;

    int curPattern = 1;//현재 어떤패턴을 사용중인지
    int curPattenShootCount = 0;//패턴 몇번 사용했는지
    float patternTimer;//패턴타이머
    bool patternChange = false;//패턴을 바꾸어야하는 상황인지
    [SerializeField] float patternChangeTime = 10.0f;//패턴 바꿀때 딜레이되는시간
                                                     //float CoolTimer = 0.0f;
    private bool hasActivatedFirst = false;
    private bool hasActivatedSecond = false;

    public bool isInvincible = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private ParticleSystem DamageEffect;
    void Start()
    {
        Hp = 300;
        MaxHp = 300;
        Player = GameObject.Find("character").GetComponent<Player>();
        Animator = GetComponent<Animator>();
        startAttack = GameObject.Find("StartAttack").GetComponent<StartAttack>();
        DamageEffect = GameObject.Find("Hitroot").GetComponent<ParticleSystem>();
        ShiledParticle = GameObject.Find("Magic shield").GetComponent<ParticleSystem>();
    }

    IEnumerator RoarCoroutine()
    {
        yield return new WaitForSeconds(0f);
        Animator.SetTrigger("Roar");
    }
    IEnumerator DearthCoroutine()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
        Debug.Log("보스가 죽었습니다");
    }

    // Update is called once per frame
    void Update()
    {

        if (startAttack.onPlayer == true)
        {
            BossSkill();
        }
    }
    private void BossSkill()
    {

        patternTimer += Time.deltaTime;
        if (patternChange == true)//패턴이 바뀔때 잠시 멈춰있는시간,유저가 공격할 타이밍
        {
            if (patternTimer >= patternChangeTime)
            {
                patternTimer = 0;
                patternChange = false;
            }
            return;
        }

        switch (curPattern)
        {
            case 1:
                if (patternTimer >= patternChangeTime)
                {
                    patternTimer = 0.0f;
                    Attack_1();
                    if (curPattenShootCount >= pattern1Count)
                    {
                        curPattern++;
                        patternChange = true;
                        curPattenShootCount = 0;
                    }
                }
                break;
            case 2:
                if (patternTimer >= pattern2Reload)
                {
                    patternTimer = 0.0f;
                    Attack_2();
                    if (curPattenShootCount >= pattern2Count)
                    {
                        curPattern++;
                        patternChange = true;
                        curPattenShootCount = 0;
                    }
                }
                break;
            case 3:
                if (patternTimer >= pattern3Reload)
                {
                    patternTimer = 0.0f;
                    Attack_3();
                    if (curPattenShootCount >= pattern3Count)
                    {
                        curPattern = 1;
                        patternChange = true;
                        curPattenShootCount = 0;
                    }
                }
                break;
        }
    }
    private void Attack_1()
    {
        curPattenShootCount++;
        Shared.MainShake.Shake(0);
        StartCoroutine(RoarCoroutine());
        Vector3 Pos = new Vector3(0, 0.01f, transform.position.z - 9f);
        GameObject pattern = Instantiate(listPattern[0], Pos, Quaternion.identity);
        pattern.name = "Range";
    }
    private void Attack_2()
    {
        curPattenShootCount++;
        Shared.MainShake.Shake(0);
        StartCoroutine(RoarCoroutine());
        Vector3 Pos = new Vector3(0, 0.01f, transform.position.z - 8f);
        GameObject pattern = Instantiate(listPattern[1], Pos, Quaternion.identity);
        pattern.name = "Range";
    }
    private void Attack_3()
    {
        curPattenShootCount++;
        Shared.MainShake.Shake(0);
        Vector3 Pos = transform.position;
        GameObject pattern = Instantiate(listPattern[2], Pos, Quaternion.identity);
        pattern.name = "Range";
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sward"))
        {
            Damage();
            Debug.Log("데미지");
            Debug.Log($"스탯{Hp}");
        }
    }

    public override void Damage()
    {
        if (isInvincible)
        {
            Debug.Log("보스 무적상태");
            return;
        }
        if (DamageEffect != null)
        {
            DamageEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // 혹시 재생 중이면 멈춤
            DamageEffect.Play();
        }
        if (Player == null) return; // Player null 체크

        int damage = Player.AttackDamage; // 플레이어 공격력 가져오기

        Hp -= damage;
        BossHpgauge.SetHp(Hp, MaxHp);
        if (Hp <= 200 && !hasActivatedFirst && Hp > 120)
        {
            Debug.Log("보스 무적상태 (HP 200 이하 ~ 121)");
            StartCoroutine(unbeatable(10f));
            hasActivatedFirst = true; // 1회 발동 후 true로 변경
        }
        else if (Hp <= 120 && !hasActivatedSecond)
        {
            Debug.Log("보스 무적상태 (HP 120 이하)");
            StartCoroutine(unbeatable(10f));
            hasActivatedSecond = true; // 1회 발동 후 true로 변경
        }
        if (Hp <= 0)
        {
            Hp = 0;
            Animator.SetBool("Death", true);
            StartCoroutine(DearthCoroutine());
        }
    }

    public void StartBless()
    {
        MyparticleSystem.Play();
    }
    public void EndBless()
    {
        MyparticleSystem.Stop();
    }
    IEnumerator unbeatable(float time)
    {
        isInvincible = true;
        if (Boss_unbeatable.instance != null)
        {
            Boss_unbeatable.instance.StartSpawn();
        }
        ShiledParticle.Play();
        yield return new WaitForSeconds(time);

        isInvincible = false;
        if (Boss_unbeatable.instance != null)
        {
            Boss_unbeatable.instance.StopSpawn();
        }        
        Debug.Log("무적종료");
    }
}
