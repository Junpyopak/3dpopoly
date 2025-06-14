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
    public float detectionDis = 3f;//탐지 거리
    public Transform player;//플레이어 위치 찾기위함용
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
        //////플레이어와의 거리 계산
        //float TargetDistance = Vector3.Distance(transform.position, player.position);
        //if (TargetDistance > Range && TargetDistance < detectionDis)//플레이어의 위치가 어택범위보다 클때만 이동하기위함
        //{
        //    if (TargetDistance < detectionDis)
        //    {
        //        transform.LookAt(player);
        //        animator.SetBool("isWalk", true);
        //        //타겟과의 방향 계산
        //        Vector3 Detection = (player.position - transform.position).normalized;
        //        //플레이어 위치 따라가기
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
    //private void Trace()//타겟추적
    //{

    //    //플레이어와의 거리 계산
    //    float TargetDistance = Vector3.Distance(transform.position, player.position);
    //    if(TargetDistance>Range&& TargetDistance < detectionDis)//플레이어의 위치가 어택범위보다 클때만 이동하기위함
    //    {
    //        if (TargetDistance < detectionDis)
    //        {
    //            transform.LookAt(player);
    //            animator.SetBool("isWalk", true);
    //            //타겟과의 방향 계산
    //            Vector3 Detection = (player.position - transform.position).normalized;
    //            //플레이어 위치 따라가기
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
        //탐지지점확인용
        base.OnDrawGizmos();
        //공격범위 확인
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);
    }
    public override void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < Range)//공격범위에 들어왔을때 공격 에님 돌아가기
        {
            //BoxCollider.enabled = true;
            animator.SetBool("isAttack", true);
            Debug.Log("플레이어 공격");
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
            Debug.Log("몬스터데미지");
            Debug.Log($"스탯{Hp}");
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
            hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // 혹시 재생 중이면 멈춤
            hitEffect.Play();
        }
        animator.SetTrigger("Hurt");

        if (Hp <= 0)
        {
            Hp = 0;
            pooling.OnEnemyDeath(this.gameObject);
            pooling.enemyCount -= 1;
            battleManager.KillCount += 1;
            Debug.Log("몬스터가 죽었습니다");
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

        // 생성 위치: 몬스터 머리 위
        Vector3 spawnPos = transform.position + Vector3.up * 2f;
        obj.transform.position = spawnPos;

        // 텍스트 설정
        Damagetext dt = obj.GetComponent<Damagetext>();
        if (dt != null)
        {
            
            dt.SetText(damage);
        }
    }

}
