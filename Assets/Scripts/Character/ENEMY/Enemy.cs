using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using static UI_TITLE;

public class Enemy : Character
{
    Pooling pooling;
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    SphereCollider sphereCollider;
    public int Hp = 70;
    public int MaxHp = 70;
    public int AttackDamage = 7;
    public int PlayerDamage = 20;
    public int expReward = 30;
    Player Player;
    BattleManager battleManager;
    private ParticleSystem hitEffect;
    public EnemyHpBar EnemyHpbar;
    public Hpbar_Shake hpBarShake;
    // Start is called before the first frame update
    private void Awake()
    {
        hitEffect = GetComponentInChildren<ParticleSystem>();
        hpBarShake = GetComponentInChildren<Hpbar_Shake>();
    }
    void Start()
    {
        pooling = Pooling.instance;
        Player = GameObject.Find("character").GetComponent<Player>();
        player = GameObject.Find("character").transform;
        Speed = 2f;
        sphereCollider = GameObject.Find("LeftHand").GetComponent<SphereCollider>();
        if (GameObject.Find("BattleManager") != null)
        {
            battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        }
        animator = GetComponent<Animator>();
        sphereCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Moved();
        // Attack();
    }

    public override void Moved()
    {
        //////�÷��̾���� �Ÿ� ���
        //float TargetDistance = Vector3.Distance(transform.position, player.position);
        //if (TargetDistance > Range && TargetDistance < detectionDis)//�÷��̾��� ��ġ�� ���ù������� Ŭ���� �̵��ϱ�����
        //{
        //    if (TargetDistance < detectionDis)
        //    {
        //        transform.LookAt(player);
        //        animator.SetBool("isWalk", true);
        //        //Ÿ�ٰ��� ���� ���
        //        Vector3 Detection = (player.position - transform.position).normalized;
        //        //�÷��̾� ��ġ ���󰡱�
        //        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //        //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
        //        transform.position += Detection * Speed * Time.deltaTime;

        //    }
        //}
        //else
        //{
        //    animator.SetBool("isWalk", false);
        //}
    }
    //private void Trace()//Ÿ������
    //{

    //    //�÷��̾���� �Ÿ� ���
    //    float TargetDistance = Vector3.Distance(transform.position, player.position);
    //    if(TargetDistance>Range&& TargetDistance < detectionDis)//�÷��̾��� ��ġ�� ���ù������� Ŭ���� �̵��ϱ�����
    //    {
    //        if (TargetDistance < detectionDis)
    //        {
    //            transform.LookAt(player);
    //            animator.SetBool("isWalk", true);
    //            //Ÿ�ٰ��� ���� ���
    //            Vector3 Detection = (player.position - transform.position).normalized;
    //            //�÷��̾� ��ġ ���󰡱�
    //            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    //            //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
    //            transform.position += Detection * Speed * Time.deltaTime;

    //        }
    //    }   
    //    else
    //    {
    //        animator.SetBool("isWalk", false);
    //    }
    //}
    public override void OnDrawGizmos()
    {
        //Ž������Ȯ�ο�
        base.OnDrawGizmos();
        //���ݹ��� Ȯ��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);
    }
    public override void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < Range)//���ݹ����� �������� ���� ���� ���ư���
        {
            //BoxCollider.enabled = true;
            animator.SetBool("isAttack", true);
            Debug.Log("�÷��̾� ����");
        }
        else
        {
            animator.SetBool("isAttack", false);

        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sward"))
        {
            //AttackCam.Instance.AttackShakeCam(0.1f, 0.15f);
            Damage();
            Debug.Log("���͵�����");
            Debug.Log($"����{Hp}");
        }

    }

    public virtual void Damage()
    {
        //if (Hp <= 0) return;

        //Hp -= Player.AttackDamage;
        //EnemyHpbar.SetHp(Hp, MaxHp);
        //ShowDamageText(Player.AttackDamage);
        //if (hpBarShake != null)
        //{
        //    hpBarShake.ShakeHpBar(0.3f, 3f);
        //}

        //if (hitEffect != null)
        //{
        //    hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Ȥ�� ��� ���̸� ����
        //    hitEffect.Play();
        //}
        //animator.SetTrigger("Hurt");

        //if (Hp <= 0)
        //{
        //    Hp = 0;
        //    if (Player != null)
        //    {
        //        Player.GainExperience(expReward);
        //    }
        //    pooling.OnEnemyDeath(this.gameObject);
        //    pooling.enemyCount -= 1;
        //    battleManager.KillCount += 1;
        //    Debug.Log("���Ͱ� �׾����ϴ�");
        //}
        if (Hp <= 0) return;
        if (Player == null) return; // Player null üũ

        int damage = Player.AttackDamage; // �÷��̾� ���ݷ� ��������

        Hp -= damage;
        EnemyHpbar.SetHp(Hp, MaxHp);
        ShowDamageText(damage); // �ؽ�Ʈ�� ���

        if (hpBarShake != null)
        {
            hpBarShake.ShakeHpBar(0.3f, 3f);
        }

        if (hitEffect != null)
        {
            hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            hitEffect.Play();
        }
        animator.SetTrigger("Hurt");

        if (Hp <= 0)
        {
            Hp = 0;

            if (Player != null)
            {
                Player.GainExperience(expReward);
            }

            pooling.OnEnemyDeath(this.gameObject);
            pooling.enemyCount -= 1;
            battleManager.KillCount += 1;

            Debug.Log("���Ͱ� �׾����ϴ�");
        }
    }

    public void StartAttack()
    {
        sphereCollider.enabled = true;
    }

    public void EndAttack()
    {
        sphereCollider.enabled = false;
    }
    public void ResetEnemy()
    {
        Hp = MaxHp;
        Transform hpBar = transform.Find("EnemyHp");
        if (hpBar != null)
        {
            hpBar.gameObject.SetActive(true);
        }

        EnemyHpbar.SetHp(Hp, MaxHp);
    }

    //private void ShowDamageText(int damage)
    //{
    //    GameObject obj = TextPool.Instance.Get();

    //    // ���� ��ġ: ���� �Ӹ� ��
    //    Vector3 spawnPos = transform.position + Vector3.up * 2f;
    //    obj.transform.position = spawnPos;

    //    // �ؽ�Ʈ ����
    //    Damagetext dt = obj.GetComponent<Damagetext>();
    //    if (dt != null)
    //    {

    //        dt.SetText(damage);
    //    }
    //}
    //public void ShowDamageText(int damage)
    //{
    //    GameObject damageTextObj = TextPool.Instance.Get();
    //    if (damageTextObj == null) return; // ���� ó��

    //    damageTextObj.transform.position = transform.position + Vector3.up; // ���� �Ӹ� ��
    //    TextMeshProUGUI text = damageTextObj.GetComponent<TextMeshProUGUI>();
    //    if (text != null)
    //    {
    //        text.text = damage.ToString();
    //    }

    //    // 1�� �� �ڵ� ��ȯ
    //    StartCoroutine(ReturnToPool(damageTextObj, 1f));
    //}
    public void ShowDamageText(int damage)
    {
        GameObject damageTextObj = TextPool.Instance.Get();
        if (damageTextObj == null) return;

        damageTextObj.transform.position = transform.position + Vector3.up;

        Text legacyText = damageTextObj.GetComponent<Text>();
        if (legacyText != null)
        {
            legacyText.text = damage.ToString();
        }

        StartCoroutine(ReturnToPool(damageTextObj, 1f));
    }

    private IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);


    }
}
