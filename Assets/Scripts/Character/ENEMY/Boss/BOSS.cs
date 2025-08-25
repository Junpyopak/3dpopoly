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
    [SerializeField] List<GameObject> listPattern;//������ ����
    public ParticleSystem MyparticleSystem;
    public ParticleSystem ShiledParticle;
    public StartAttack startAttack;
    [Header("���� ����")]

    [SerializeField] int pattern1Count = 1;
    [SerializeField] float pattern1Reload = 5f;

    [SerializeField] int pattern2Count = 1;
    [SerializeField] float pattern2Reload = 7f;

    [SerializeField] int pattern3Count = 1;
    [SerializeField] float pattern3Reload = 7f;

    int curPattern = 1;//���� ������� ���������
    int curPattenShootCount = 0;//���� ��� ����ߴ���
    float patternTimer;//����Ÿ�̸�
    bool patternChange = false;//������ �ٲپ���ϴ� ��Ȳ����
    [SerializeField] float patternChangeTime = 10.0f;//���� �ٲܶ� �����̵Ǵ½ð�
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
        Debug.Log("������ �׾����ϴ�");
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
        if (patternChange == true)//������ �ٲ� ��� �����ִ½ð�,������ ������ Ÿ�̹�
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
            Debug.Log("������");
            Debug.Log($"����{Hp}");
        }
    }

    public override void Damage()
    {
        if (isInvincible)
        {
            Debug.Log("���� ��������");
            return;
        }
        if (DamageEffect != null)
        {
            DamageEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Ȥ�� ��� ���̸� ����
            DamageEffect.Play();
        }
        if (Player == null) return; // Player null üũ

        int damage = Player.AttackDamage; // �÷��̾� ���ݷ� ��������

        Hp -= damage;
        BossHpgauge.SetHp(Hp, MaxHp);
        if (Hp <= 200 && !hasActivatedFirst && Hp > 120)
        {
            Debug.Log("���� �������� (HP 200 ���� ~ 121)");
            StartCoroutine(unbeatable(10f));
            hasActivatedFirst = true; // 1ȸ �ߵ� �� true�� ����
        }
        else if (Hp <= 120 && !hasActivatedSecond)
        {
            Debug.Log("���� �������� (HP 120 ����)");
            StartCoroutine(unbeatable(10f));
            hasActivatedSecond = true; // 1ȸ �ߵ� �� true�� ����
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
        Debug.Log("��������");
    }
}
