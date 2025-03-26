using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;
using Unity.Mathematics;
using Unity.Burst.CompilerServices;
using static UnityEngine.Rendering.VolumeComponent;
using UnityEngine.AI;
using static UI_TITLE;


public class Player : Character
{
    private Rigidbody rigidbody;
    private MoveStrategy moveStrategy;
     Camera camera;
    CharacterController chController;
    public float runSpeed = 7f;
    public float jumpFor = 3f;
    private float rotate = 3f;
    private bool moveFast;
    public float Smove = 10f;
    public float detectionDis = 7f;//탐지 거리
    public bool Attack1 = false;
    Enemy Enemy;
    public int Hp = 100;
    public int MaxHp = 100;
    public int AttackDamage = 20;
    BoxCollider BoxCollider;
    [SerializeField] HpGauge hpGauge;
    [SerializeField] DamaeText DamaeText;
    public GameObject damageText;
    public GameObject GameOver;
    private PlayableDirector playableDirector;
    public TimelineAsset[] timelines;
    private int playCount = 0;
    public bool playCut = false;
    private RaycastHit hit;
    public GameObject Autoloading;
    public GameObject AutoPos;
    public bool AutoMode = false;
    NavMeshAgent meshAgent;
    public Transform Warrok;
    public bool DoTelpo = false;
    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;
        if (GameObject.Find("Warrok") != null)
        {
            Enemy = GameObject.Find("Warrok").GetComponent<Enemy>();
            Warrok = GameObject.Find("Warrok").transform;

        }

        Speed = 3f;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GameObject.Find("character").GetComponent<Animator>();
        camera = Camera.main;
        chController = GameObject.Find("character").GetComponent<CharacterController>();
        //MeshCollider = GameObject.Find("Weapon").GetComponent<MeshCollider>();
        //MeshCollider.enabled = false;
        if (GameObject.Find("Weapon") != null)
        {
            BoxCollider = GameObject.Find("Weapon").GetComponent<BoxCollider>();
        }
        playableDirector = GetComponent<PlayableDirector>();
        hpGauge = GameObject.Find("PlayerHp").GetComponent<HpGauge>();
        meshAgent = GetComponent<NavMeshAgent>();

    }

    public void SetMove(MoveStrategy moveStrategy)
    {
        this.moveStrategy = moveStrategy;
    }


    public void DoMove()
    {
        moveStrategy.Move();
    }

    private void FixedUpdate()
    {
        Attack();
    }
    IEnumerator MoveStopCoroutine()//컷신 플레이시 플레이어 움직임 조작
    {
        yield return new WaitForSeconds(11.2f);
        playCut = false;
    }
    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(transform.position, Warrok.position);
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
        if (animator.GetBool("Death") == false)
        {
            if (playCut == false)
            {
                if (Input.GetKey(KeyCode.LeftShift) || AutoMode == true)
                {
                    SetMove(new RunStrategy());
                    moveFast = true;
                    Speed = runSpeed;
                    animator.SetBool("isRun", moveFast);
                    DoMove();
                }
                else
                {
                    moveFast = false;
                    Speed = 3;
                    animator.SetBool("isRun", moveFast);
                }
                Moved();
            }
            else
            {
                Speed = 0;
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", false);
            }
        }
        if (AutoMode && !meshAgent.pathPending && meshAgent.remainingDistance <= 0.1f)
        {
            AutoMode = false;
            meshAgent.isStopped = true;
            chController.enabled = true;
        }
        Autoloading.SetActive(AutoMode);
        if (AutoMode == true)
        {
            if (targetDistance > Range && targetDistance < detectionDis)
            {
                chController.enabled = false;
                meshAgent.isStopped = false;
                meshAgent.speed = 5;
                meshAgent.destination = Warrok.position;
            }
            if (targetDistance <= Range)
            {
                // 네비 멈추고 공격 실행
                meshAgent.isStopped = true;
                Attack();
                Debug.Log("플레이어 적 공격");
            }
        }  
    }
    private void LateUpdate()
    {
        if (camera == null) return;
        Vector3 playerRaotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRaotation), Time.deltaTime * Smove);
        //Vector3 direction = (Warrok.position - transform.position).normalized;
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
    public override void Moved()
    {
        if (DoTelpo == false)
        {
            SetMove(new WalkStrategy());
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Vector3 moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
            if (moveDir != Vector3.zero)
            {
                animator.SetBool("isWalk", true);
                DoMove();
            }
            else
            {
                animator.SetBool("isWalk", false);
            }
            chController.Move(moveDir.normalized * Speed * Time.deltaTime);
        }
    }
    private void StartAttack()
    {
        Attack1 = true;
    }
    void EndAttack1()
    {
        Attack1 = false;
    }
    void EndAttack2()
    {
        animator.SetBool("Atk2", false);
        Debug.Log("어택2끝");
    }

    void StartCol()
    {
        BoxCollider.enabled = true;
    }
    void EndCol()
    {
        BoxCollider.enabled = false;
    }
    public override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Attack1 == false)
            {
                animator.SetTrigger("Atk1");
            }
            else if (animator.GetBool("Atk2") == false)
            {
                animator.SetBool("Atk2", true);
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool("Death") == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("MonsterAttackBox"))
            {
                Damage();
                Debug.Log("데미지");
                Debug.Log($"스텟{Hp}");
            }
            if (other.gameObject.CompareTag("Gate"))
            {
                Debug.Log("게이트");
                SceneManager.LoadScene("BossMapLOAD");
            }
            if (other.gameObject.CompareTag("BossSkill"))
            {
                Hp -= BossSkillDamage;
                hpGauge.SetPlayerHp(Hp, MaxHp);
                Debug.Log($"스텟{Hp}");
            }
            if (other.tag == "CutScene")
            {
                if (playCount == 0)
                {
                    Debug.Log("컷신");
                    playableDirector.Play(timelines[0]);
                    playCut = true;
                    StartCoroutine(MoveStopCoroutine());
                }
                playCount = 1;
            }

        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        //공격범위 확인
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);
    }

    public void Damage()
    {
        Hp -= Enemy.AttackDamage;
        GameObject Text = Instantiate(damageText);
        Text.GetComponent<DamaeText>();
        hpGauge.SetPlayerHp(Hp, MaxHp);
        if (Hp <= 0)
        {
            GameObject gameOver = Instantiate(GameOver);
            gameOver.GetComponent<Ui_Retry>();
            animator.SetBool("Death", true);
            Debug.Log("죽었습니다");
        }
    }
    public void BtnAuto()
    {
        AutoMode = !AutoMode;
        Autoloading.SetActive(AutoMode);
        if (AutoMode)
        {
            chController.enabled = false;
            meshAgent.isStopped = false;
            meshAgent.speed = 5;
            meshAgent.destination = AutoPos.transform.position;
        }

        else
        {
            meshAgent.isStopped = true;
            meshAgent.ResetPath();
            chController.enabled = true;
        }
    }

}
