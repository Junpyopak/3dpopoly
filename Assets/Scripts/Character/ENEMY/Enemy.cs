using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
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
    Player Player;
    BattleManager battleManager;
    private ParticleSystem hitEffect;
    public EnemyHpBar EnemyHpbar;
    // Start is called before the first frame update
    private void Awake()
    {
        hitEffect = GetComponentInChildren<ParticleSystem>();
    }
    void Start()
    {
        pooling = Pooling.instance;
        Player = GameObject.Find("character").GetComponent<Player>();
        player = GameObject.Find("character").transform;
        Speed = 2f;
        sphereCollider = GameObject.Find("LeftHand").GetComponent<SphereCollider>();
        if(GameObject.Find("BattleManager")!=null)
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
            AttackCam.Instance.AttackShakeCam(0.1f, 0.15f);
            Damage();
            Debug.Log("���͵�����");
            Debug.Log($"����{Hp}");
        }

    }

    public virtual void Damage()
    {
        if (Hp <= 0) return;

        Hp -= Player.AttackDamage;
        EnemyHpbar.SetHp(Hp, MaxHp);
        ShowDamageText(Player.AttackDamage);

        if (hitEffect != null)
        {
            hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Ȥ�� ��� ���̸� ����
            hitEffect.Play();
        }
        animator.SetTrigger("Hurt");

        if (Hp <= 0)
        {
            Hp = 0;
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
        EnemyHpbar.SetHp(Hp, MaxHp);
    }

    private void ShowDamageText(int damage)
    {
        GameObject obj = TextPool.Instance.Get();

        // ���� ��ġ: ���� �Ӹ� ��
        Vector3 spawnPos = transform.position + Vector3.up * 2f;
        obj.transform.position = spawnPos;

        // �ؽ�Ʈ ����
        Damagetext dt = obj.GetComponent<Damagetext>();
        if (dt != null)
        {
            
            dt.SetText(damage);
        }
    }

}
