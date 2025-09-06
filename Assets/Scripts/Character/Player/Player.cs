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
using UnityEngine.TextCore.Text;
using Photon.Pun;
using Photon.Realtime;


public class Player : Character, IPunObservable
{
    //private Rigidbody rigidbody;
    //private MoveStrategy moveStrategy;
    //Camera camera;
    //CharacterController chController;
    //public float runSpeed = 7f;
    //public float jumpFor = 3f;
    //private float rotate = 3f;
    //private bool moveFast;
    //public float Smove = 10f;
    //public float detectionDis = 7f;//탐지 거리
    //public bool Attack1 = false;
    //public bool IsAttacking => Attack1 || animator.GetBool("Atk2");
    //Enemy Enemy;
    //public int Hp = 100;
    //public int MaxHp = 100;
    //private static int savedAttackDamage = -1;
    //public int AttackDamage = 20;
    //BoxCollider BoxCollider;
    //[SerializeField] HpGauge hpGauge;
    //[SerializeField] DamaeText DamaeText;
    //public GameObject damageText;
    //public GameObject GameOver;
    //private PlayableDirector playableDirector;
    //public TimelineAsset[] timelines;
    //private int playCount = 0;
    //public bool playCut = false;
    //private RaycastHit hit;
    //public GameObject Autoloading;
    //public GameObject AutoPos;
    //public bool AutoMode = false;
    //NavMeshAgent meshAgent;
    //public Transform Warrok;
    //public bool DoTelpo = false;
    //public LayerMask layer;
    //public AttackCam AttackCam;
    //private ParticleSystem SkillEffect;
    //SkillCoolController SkillCool;
    //SlashEffect_OnOff slashEffect;
    //private ParticleSystem TornadoEffect;
    //public Transform skillSpawnPoint;
    //private FrostEffect frostEffect;
    //bool isWalkingSound = false;


    //public int currentExp = 0;
    //public int level = 1;
    //public int expToLevelUp = 100;
    //[SerializeField] private Slider expSlider;
    //private Coroutine expRoutine;
    //public GameObject LevelUPEffect;
    //[SerializeField] private TextMeshProUGUI levelup_text;

    //[SerializeField] private TMP_Text hpText;
    //[SerializeField] private TMP_Text expText;

    //[SerializeField] ButtonFade Levelbtn;
    //public static int savedLevel = 1;
    //public static int savedHp = -1;
    //public static int savedExp = 0;
    //public static int savedMaxHp = 100;
    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    //이전씬에 존재한 공격력 불러오기
    //    if (savedAttackDamage != -1)
    //    {
    //        AttackDamage = savedAttackDamage;
    //    }
    //    if (savedLevel != 1)
    //        level = savedLevel;

    //    if (savedHp != -1)
    //        Hp = savedHp;

    //    if (savedMaxHp != 100)
    //        MaxHp = savedMaxHp;

    //    currentExp = savedExp;
    //}
    //void Start()
    //{
    //    // 씬 전환 후에도 DontDestroyOnLoad ExpBar(UI) 찾아서 연결
    //    ExpSlider existingExpBar = FindObjectOfType<ExpSlider>();
    //    if (existingExpBar != null)
    //    {
    //        if (existingExpBar != null)
    //            expSlider = existingExpBar.slider;
    //        levelup_text = existingExpBar.levelup_text;
    //        LevelUPEffect = existingExpBar.LevelUPEffect;
    //    }
    //    UpdateExpSliderImmediate();
    //    UpdateLevelText();
    //    if (frostEffect == null)
    //        frostEffect = Camera.main.GetComponent<FrostEffect>();
    //    SkillCool = SkillCoolController.instance;
    //    slashEffect = SlashEffect_OnOff.instance;
    //   // Hp = MaxHp;
    //    if (GameObject.Find("Warrok") != null)
    //    {
    //        Enemy = GameObject.Find("Warrok").GetComponent<Enemy>();
    //        Warrok = GameObject.Find("Warrok").transform;
    //    }
    //    Speed = 3f;
    //    rigidbody = this.GetComponent<Rigidbody>();
    //    animator = GameObject.Find("character").GetComponent<Animator>();
    //    camera = Camera.main;
    //    chController = GameObject.Find("character").GetComponent<CharacterController>();
    //    //MeshCollider = GameObject.Find("Weapon").GetComponent<MeshCollider>();
    //    //MeshCollider.enabled = false;
    //    if (GameObject.Find("Weapon") != null)
    //    {
    //        BoxCollider = GameObject.Find("Weapon").GetComponent<BoxCollider>();
    //    }
    //    playableDirector = GetComponent<PlayableDirector>();
    //    hpGauge = GameObject.Find("PlayerHp").GetComponent<HpGauge>();
    //    meshAgent = GetComponent<NavMeshAgent>();
    //    AttackCam = GameObject.Find("Camera").GetComponent<AttackCam>();
    //    SkillEffect = GameObject.Find("Skill1").GetComponent<ParticleSystem>();
    //    TornadoEffect = GameObject.Find("TorrnadoSkill").GetComponent<ParticleSystem>();
    //    UpdateHpUI();
    //    UpdateExpUI();
    //}
    //private void OnDestroy()
    //{
    //    // 파괴될 때 공격력 저장
    //    savedAttackDamage = AttackDamage;
    //    savedLevel = level;
    //    savedHp = Hp;
    //    savedMaxHp = MaxHp;
    //    savedExp = currentExp;
    //}
    //public void SetMove(MoveStrategy moveStrategy)
    //{
    //    this.moveStrategy = moveStrategy;
    //}


    //public void DoMove()
    //{
    //    moveStrategy.Move();
    //}

    //private void FixedUpdate()
    //{
    //    Attack();
    //}
    //IEnumerator MoveStopCoroutine()//컷신 플레이시 플레이어 움직임 조작
    //{
    //    yield return new WaitForSeconds(11.2f);
    //    playCut = false;
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //    if (Hp > MaxHp)
    //    {
    //        Hp = MaxHp;
    //    }
    //    if (animator.GetBool("Death") == false)
    //    {
    //        //if (frostEffect.isFrozen == true)
    //        //{
    //        //    Speed = 0f;
    //        //    moveFast = false;
    //        //    animator.SetBool("isRun", false);
    //        //    return;
    //        //}
    //        if (playCut == false)
    //        {
    //            if (IsAttacking == true) return;
    //            if (SkillCool.isSkillCasting == true) return;
    //            if(frostEffect.isFrozen == true)
    //            {
    //                animator.SetFloat("Speed", 0f);
    //            }

    //            if (Input.GetKey(KeyCode.LeftShift) || AutoMode == true)
    //            {
    //                SetMove(new RunStrategy());
    //                moveFast = true;
    //                Speed = runSpeed;
    //                animator.SetBool("isRun", moveFast);
    //                DoMove();
    //            }
    //            else
    //            {
    //                moveFast = false;
    //                Speed = 3;
    //                animator.SetBool("isRun", moveFast);
    //            }
    //            Moved();
    //        }
    //        else
    //        {
    //            Speed = 0;
    //            animator.SetBool("isRun", false);
    //            animator.SetBool("isWalk", false);
    //        }
    //    }
    //    if (AutoMode && !meshAgent.pathPending && meshAgent.remainingDistance <= 0.1f)
    //    {
    //        AutoMode = false;
    //        meshAgent.isStopped = true;
    //        chController.enabled = true;
    //    }
    //    //if (colliders.Length > 0)
    //    //{
    //    //    float targetDistance = Vector3.Distance(transform.position, colliders[0].transform.position);
    //    //    foreach (Collider collider in colliders)
    //    //    {
    //    //        float Shotdistance = Vector3.Distance(transform.position, collider.transform.position);
    //    //        if (targetDistance > Shotdistance)
    //    //        {
    //    //            targetDistance = Shotdistance;
    //    //            ShotEnemy = collider;
    //    //        }
    //    //    }
    //    //}
    //    Autoloading.SetActive(AutoMode);
    //    //if (AutoMode == true)
    //    //{
    //    //    float targetDistance = Vector3.Distance(transform.position, colliders[0].transform.position);
    //    //    //foreach (Collider collider in colliders)
    //    //    //{
    //    //    //    float Shotdistance = Vector3.Distance(transform.position, collider.transform.position);
    //    //    //    if (targetDistance > Shotdistance)
    //    //    //    {
    //    //    //        targetDistance = Shotdistance;
    //    //    //        ShotEnemy = collider;
    //    //    //    }
    //    //    //}
    //    //    if (targetDistance > Range && targetDistance < detectionDis)
    //    //    {
    //    //        chController.enabled = false;
    //    //        meshAgent.isStopped = false;
    //    //        meshAgent.speed = 5;
    //    //        meshAgent.destination = Warrok.position;
    //    //    }
    //    //    if (targetDistance <= Range)
    //    //    {
    //    //        // 네비 멈추고 공격 실행
    //    //        meshAgent.isStopped = true;
    //    //        //chController.enabled = true;
    //    //        Autoloading.SetActive(true);
    //    //        Attack();
    //    //        Debug.Log("플레이어 적 공격");
    //    //    }
    //    //}
    //}
    //private void LateUpdate()
    //{
    //    //if (camera == null) return;
    //    //Vector3 playerRaotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRaotation), Time.deltaTime * Smove);

    //    //if (camera == null) return;

    //    //Target_Onlock lockOn = GetComponent<Target_Onlock>();
    //    //if (lockOn != null && lockOn.isLockingOn)
    //    //{
    //    //    GameObject target = lockOn.GetLockedTarget();
    //    //    if (target != null)
    //    //    {
    //    //        Vector3 dir = target.transform.position - transform.position;
    //    //        dir.y = 0;
    //    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * Smove);
    //    //        return;
    //    //    }
    //    //}

    //    //// 기본 카메라 방향 회전
    //    //Vector3 playerRotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * Smove);

    //    //if (camera == null) return;

    //    //Target_Onlock lockOn = GetComponent<Target_Onlock>();
    //    //if (lockOn != null && lockOn.IsLockingOn())
    //    //{
    //    //    GameObject target = lockOn.GetLockedTarget();
    //    //    if (target != null)
    //    //    {
    //    //        // 적 방향으로 회전 (y축만 고려)
    //    //        Vector3 dir = target.transform.position - transform.position;
    //    //        dir.y = 0;
    //    //        Quaternion lookRot = Quaternion.LookRotation(dir);

    //    //        // 플레이어 회전
    //    //        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * Smove);

    //    //        // 카메라도 회전
    //    //        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, lookRot, Time.deltaTime * Smove);

    //    //        return;
    //    //    }
    //    //}

    //    //// 기본 카메라 방향을 따라 회전
    //    //Vector3 playerRotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * Smove);
    //    if (camera == null) return;

    //    Target_Onlock lockOn = GetComponent<Target_Onlock>();
    //    if (lockOn != null && lockOn.IsLockingOn())
    //    {
    //        GameObject target = lockOn.GetLockedTarget();
    //        if (target != null)
    //        {
    //            // 적 방향으로 회전 (y축만 고려)
    //            Vector3 dir = target.transform.position - transform.position;
    //            dir.y = 0;
    //            Quaternion lookRot = Quaternion.LookRotation(dir);

    //            // 플레이어 회전
    //            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * Smove);

    //            return;
    //        }
    //    }

    //    // 기본 카메라 방향을 따라 회전
    //    Vector3 playerRotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * Smove);
    //}
    //private void UpdateExpSliderImmediate()
    //{
    //    expSlider.maxValue = expToLevelUp;
    //    expSlider.value = currentExp;
    //}
    //public void UpdateHpUI()
    //{
    //    if (hpGauge != null)
    //        hpGauge.SetPlayerHp(Hp, MaxHp);

    //    if (hpText != null)
    //        hpText.text = $"{Hp} / {MaxHp}";
    //}
    //private void UpdateExpUI()
    //{
    //    if (expSlider != null)
    //    {
    //        expSlider.maxValue = expToLevelUp;
    //        expSlider.value = currentExp;
    //    }

    //    if (expText != null)
    //        expText.text = $"{currentExp} / {expToLevelUp}";
    //}
    //private void UpdateLevelText()
    //{
    //    if (levelup_text != null)
    //        levelup_text.text =  level.ToString();
    //}
    //public void GainExperience(int amount)
    //{
    //    currentExp += amount;

    //    // 코루틴 중단
    //    if (expRoutine != null)
    //        StopCoroutine(expRoutine);

    //    expRoutine = StartCoroutine(HandleExperience());
    //    UpdateExpUI();
    //}
    //private IEnumerator HandleExperience()
    //{
    //    while (true)
    //    {
    //        // 현재 레벨 기준 경험치 목표 저장
    //        int requiredExp = expToLevelUp;

    //        // 부드러운 슬라이더 증가
    //        yield return StartCoroutine(AnimateExpSlider(currentExp, requiredExp));

    //        // 레벨업 조건
    //        if (currentExp >= requiredExp)
    //        {
    //            currentExp -= requiredExp;
    //            LevelUp();
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    // 마지막 슬라이더 값 갱신
    //    UpdateExpSliderImmediate();
    //}
    //private IEnumerator AnimateExpSlider(int currentValue, int maxValue)
    //{
    //    float duration = 0.3f;
    //    float elapsed = 0f;

    //    float startValue = expSlider.value;
    //    float targetValue = Mathf.Min(currentValue, maxValue);

    //    expSlider.maxValue = maxValue;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        float t = elapsed / duration;
    //        expSlider.value = Mathf.Lerp(startValue, targetValue, t);
    //        yield return null;
    //    }

    //    expSlider.value = targetValue;
    //}
    //private void LevelUp()
    //{
    //    level++;
    //    Levelbtn.ShowOnLevelUp();
    //    Debug.Log($"[레벨업 함수 실행됨] 현재 레벨: {level}");
    //    expToLevelUp += 50;
    //    UpdateExpUI();

    //    if (LevelUPEffect != null)
    //    {
    //        Vector3 spawnPos = gameObject.transform.position + new Vector3(0.5f, 0f, 0f);
    //        GameObject particle = Instantiate(LevelUPEffect, spawnPos, quaternion.identity);
    //        SoundManager.Instance.PlaySFX("Levelup");
    //        Destroy(particle, 2f);
    //    }

    //    UpdateLevelText();

    //    Debug.Log("레벨 업! 현재 레벨: " + level);
    //}
    //public void IncreaseMaxHp(int amount)
    //{
    //    MaxHp += amount;
    //    Hp += amount;

    //    if (Hp > MaxHp)
    //        Hp = MaxHp;

    //    if (hpGauge != null)
    //    {
    //        hpGauge.SetPlayerHp(Hp, MaxHp);
    //    }
    //    UpdateHpUI();
    //}
    //public override void Moved()
    //{
    //    if (frostEffect.isFrozen)
    //    {
    //        animator.SetBool("isWalk", false);
    //       // SoundManager.Instance.StopLoop();
    //        chController.Move(Vector3.zero);
    //        return;
    //    }
    //    if (DoTelpo == false)
    //    {
    //        SetMove(new WalkStrategy());
    //        Vector3 forward = transform.TransformDirection(Vector3.forward);
    //        Vector3 right = transform.TransformDirection(Vector3.right);

    //        Vector3 moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
    //        if (moveDir != Vector3.zero)
    //        {
    //            animator.SetBool("isWalk", true);
    //            //if (!isWalkingSound)
    //            //{
    //            //    SoundManager.Instance.PlayLoop("Footstep");
    //            //    isWalkingSound = true;
    //            //}
    //            DoMove();
    //        }
    //        else
    //        {
    //            animator.SetBool("isWalk", false);
    //            //if (isWalkingSound)
    //            //{
    //            //    SoundManager.Instance.StopLoop();
    //            //    isWalkingSound = false;
    //            //}
    //        }
    //        chController.Move(moveDir.normalized * Speed * Time.deltaTime);
    //    }
    //}
    //void FootSetSound()
    //{
    //    SoundManager.Instance.PlaySFX("Footstep",0.5f);
    //}
    //void RunSetSound()
    //{
    //    SoundManager.Instance.PlaySFX("RunStep", 0.5f);
    //}
    //private void StartAttack()
    //{
    //    Attack1 = true;

    //}
    //void EndAttack1()
    //{
    //    Attack1 = false;
    //}
    //void EndAttack2()
    //{
    //    animator.SetBool("Atk2", false);
    //    Debug.Log("어택2끝");
    //}

    //void StartCol()
    //{
    //    BoxCollider.enabled = true;
    //}
    //void EndCol()
    //{
    //    BoxCollider.enabled = false;
    //}
    //public void SkillPositionPlay()
    //{
    //    SkillEffect.Play();
    //}
    //public void SkillEnd()
    //{
    //    SkillCool.isSkillCasting = false;
    //}
    //public override void Attack()
    //{
    //    if(frostEffect.isFrozen==true)return;
    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        if (Attack1 == false)
    //        {
    //            animator.SetTrigger("Atk1");

    //        }
    //        else if (animator.GetBool("Atk2") == false)
    //        {
    //            animator.SetBool("Atk2", true);

    //        }
    //    }
    //}
    //void AtkSound()
    //{
    //    SoundManager.Instance.PlaySFX("AttackSound");
    //}

    //public override void OnTriggerEnter(Collider other)
    //{
    //    if (animator.GetBool("Death") == false)
    //    {
    //        if (other.gameObject.layer == LayerMask.NameToLayer("MonsterAttackBox"))
    //        {
    //            Damage();
    //            if(frostEffect!=null)
    //            {
    //                frostEffect.PlayFrostEffect();
    //            }
    //            Debug.Log("데미지");
    //            Debug.Log($"스텟{Hp}");
    //        }
    //        if (other.tag == "Gate")
    //        {
    //            Debug.Log("게이트");
    //            SceneManager.LoadScene("BossMapLOAD");
    //        }
    //        if (other.gameObject.CompareTag("BossSkill"))
    //        {
    //            Hp -= BossSkillDamage;
    //            hpGauge.SetPlayerHp(Hp, MaxHp);
    //            Debug.Log($"스텟{Hp}");
    //        }
    //        if (other.tag == "CutScene")
    //        {
    //            if (playCount == 0)
    //            {
    //                Debug.Log("컷신");
    //                playableDirector.Play(timelines[0]);
    //                playCut = true;
    //                StartCoroutine(MoveStopCoroutine());
    //            }
    //            playCount = 1;
    //        }

    //    }
    //}

    //public override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();
    //    //공격범위 확인
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(this.transform.position, detectionDis);
    //}

    //public void Damage()
    //{
    //    Hp -= 7;//Enemy.AttackDamage;
    //    UpdateHpUI();
    //    GameObject Text = Instantiate(damageText);
    //    Text.GetComponent<DamaeText>();
    //    hpGauge.SetPlayerHp(Hp, MaxHp);
    //    if (Hp <= 0)
    //    {
    //        GameObject gameOver = Instantiate(GameOver);
    //        gameOver.GetComponent<Ui_Retry>();
    //        animator.SetBool("Death", true);
    //        Debug.Log("죽었습니다");
    //    }
    //}
    //public void BtnAuto()
    //{
    //    AutoMode = !AutoMode;
    //    Autoloading.SetActive(AutoMode);
    //    if (AutoMode)
    //    {
    //        chController.enabled = false;
    //        meshAgent.isStopped = false;
    //        meshAgent.speed = 5;
    //        meshAgent.destination = AutoPos.transform.position;
    //    }

    //    else
    //    {
    //        meshAgent.isStopped = true;
    //        meshAgent.ResetPath();
    //        chController.enabled = true;
    //    }
    //}
    //public void StartTrail()
    //{
    //    if (slashEffect != null)
    //    {

    //        slashEffect.Slash_On();
    //    }

    //}

    //public void StopTrail()
    //{
    //    if (slashEffect != null)
    //    {
    //        slashEffect.Slash_Off();
    //    }

    //}
    //public void OnTornadoCastEnd()
    //{
    //    TornadoEffect.Play();
    //}
    private Rigidbody rigidbody;
    private MoveStrategy moveStrategy;
    private Camera camera;
    private CharacterController chController;

    public float runSpeed = 7f;
    public float jumpFor = 3f;
    private float rotate = 3f;
    private bool moveFast;
    public float Smove = 10f;

    public float detectionDis = 7f;
    public bool Attack1 = false;
    public bool IsAttacking => Attack1 || animator.GetBool("Atk2");

    public int Hp = 100;
    public int MaxHp = 100;
    public int AttackDamage = 20;

    private BoxCollider BoxCollider;
    [SerializeField] HpGauge hpGauge;
    public GameObject damageText;
    public GameObject GameOver;

    private PlayableDirector playableDirector;
    public TimelineAsset[] timelines;
    private int playCount = 0;
    public bool playCut = false;

    public GameObject Autoloading;
    public GameObject AutoPos;
    public bool AutoMode = false;

    private NavMeshAgent meshAgent;
    public Transform Warrok;
    public bool DoTelpo = false;
    public LayerMask layer;

    public AttackCam AttackCam;
    private ParticleSystem SkillEffect;
    private SlashEffect_OnOff slashEffect;
    private ParticleSystem TornadoEffect;
    public Transform skillSpawnPoint;
    private FrostEffect frostEffect;
    bool isWalkingSound = false;

    public int currentExp = 0;
    public int level = 1;
    public int expToLevelUp = 100;
    [SerializeField] private Slider expSlider;
    private Coroutine expRoutine;
    public GameObject LevelUPEffect;
    [SerializeField] private TextMeshProUGUI levelup_text;

    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private ButtonFade Levelbtn;

    // --- Static 저장 변수 ---
    private static int savedAttackDamage = -1;
    public static int savedLevel = 1;
    public static int savedHp = -1;
    public static int savedExp = 0;
    public static int savedMaxHp = 100;

    // --- 초기화 ---
    private void Awake()
    {
        if (savedAttackDamage != -1) AttackDamage = savedAttackDamage;
        if (savedLevel != 1) level = savedLevel;
        if (savedHp != -1) Hp = savedHp;
        if (savedMaxHp != 100) MaxHp = savedMaxHp;
        currentExp = savedExp;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        chController = GetComponent<CharacterController>();
        meshAgent = GetComponent<NavMeshAgent>();
        playableDirector = GetComponent<PlayableDirector>();
        frostEffect = Camera.main.GetComponent<FrostEffect>();
        SkillEffect = GameObject.Find("Skill1").GetComponent<ParticleSystem>();
        TornadoEffect = GameObject.Find("TorrnadoSkill").GetComponent<ParticleSystem>();
        slashEffect = SlashEffect_OnOff.instance;
        camera = Camera.main;
        animator = GameObject.Find("character").GetComponent<Animator>();
        BoxCollider = GameObject.Find("Weapon").GetComponent<BoxCollider>();
        hpGauge = GameObject.Find("PlayerHp").GetComponent<HpGauge>();
        AttackCam = GameObject.Find("Camera").GetComponent<AttackCam>();

        UpdateHpUI();
        UpdateExpUI();
        UpdateLevelText();
    }

    private void OnDestroy()
    {
        savedAttackDamage = AttackDamage;
        savedLevel = level;
        savedHp = Hp;
        savedMaxHp = MaxHp;
        savedExp = currentExp;
    }

    // --- Movement ---
    public void SetMove(MoveStrategy moveStrategy) => this.moveStrategy = moveStrategy;
    public void DoMove() => moveStrategy.Move();

    private void FixedUpdate() => Attack();

    void Update()
    {
        if (Hp > MaxHp) Hp = MaxHp;

        if (!animator.GetBool("Death"))
        {
            if (!playCut && !IsAttacking && !SkillCoolController.instance.isSkillCasting && !frostEffect.isFrozen)
            {
                HandleMovement();
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
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) || AutoMode)
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

    public override void Moved()
    {
        if (frostEffect.isFrozen)
        {
            animator.SetBool("isWalk", false);
            chController.Move(Vector3.zero);
            return;
        }

        if (!DoTelpo)
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

    private void LateUpdate()
    {
        if (camera == null) return;

        Target_Onlock lockOn = GetComponent<Target_Onlock>();
        if (lockOn != null && lockOn.IsLockingOn())
        {
            GameObject target = lockOn.GetLockedTarget();
            if (target != null)
            {
                Vector3 dir = target.transform.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * Smove);
                return;
            }
        }

        Vector3 playerRotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * Smove);
    }

    // --- HP & EXP UI ---
    public void UpdateHpUI()
    {
        hpGauge?.SetPlayerHp(Hp, MaxHp);
        if (hpText != null) hpText.text = $"{Hp} / {MaxHp}";
    }

    private void UpdateExpUI()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToLevelUp;
            expSlider.value = currentExp;
        }
        if (expText != null) expText.text = $"{currentExp} / {expToLevelUp}";
    }

    private void UpdateLevelText()
    {
        if (levelup_text != null) levelup_text.text = level.ToString();
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (expRoutine != null) StopCoroutine(expRoutine);
        expRoutine = StartCoroutine(HandleExperience());
        UpdateExpUI();
    }

    private IEnumerator HandleExperience()
    {
        while (true)
        {
            int requiredExp = expToLevelUp;
            yield return StartCoroutine(AnimateExpSlider(currentExp, requiredExp));

            if (currentExp >= requiredExp)
            {
                currentExp -= requiredExp;
                LevelUp();
            }
            else break;
        }

        UpdateExpSliderImmediate();
    }

    private IEnumerator AnimateExpSlider(int currentValue, int maxValue)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        float startValue = expSlider.value;
        float targetValue = Mathf.Min(currentValue, maxValue);

        expSlider.maxValue = maxValue;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            expSlider.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        expSlider.value = targetValue;
    }

    private void UpdateExpSliderImmediate()
    {
        expSlider.maxValue = expToLevelUp;
        expSlider.value = currentExp;
    }

    private void LevelUp()
    {
        level++;
        Levelbtn.ShowOnLevelUp();
        expToLevelUp += 50;
        UpdateExpUI();

        if (LevelUPEffect != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0.5f, 0f, 0f);
            GameObject particle = Instantiate(LevelUPEffect, spawnPos, Quaternion.identity);
            Destroy(particle, 2f);
        }

        UpdateLevelText();
    }

    public void IncreaseMaxHp(int amount)
    {
        MaxHp += amount;
        Hp += amount;
        if (Hp > MaxHp) Hp = MaxHp;
        UpdateHpUI();
    }

    // --- 공격 ---
    public override void Attack()
    {
        if (frostEffect.isFrozen) return;

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!Attack1)
                animator.SetTrigger("Atk1");
            else if (!animator.GetBool("Atk2"))
                animator.SetBool("Atk2", true);
        }
    }

    void StartAttack() => Attack1 = true;
    void EndAttack1() => Attack1 = false;
    void EndAttack2() => animator.SetBool("Atk2", false);

    void StartCol() => BoxCollider.enabled = true;
    void EndCol() => BoxCollider.enabled = false;

    public void SkillPositionPlay() => SkillEffect.Play();
    public void SkillEnd() => SkillCoolController.instance.isSkillCasting = false;
    public void OnTornadoCastEnd() => TornadoEffect.Play();

    // --- 자동 모드 ---
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

    // --- 트리거 ---
    public override void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool("Death")) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("MonsterAttackBox"))
            Damage();

        if (other.CompareTag("Gate"))
            SceneManager.LoadScene("BossMapLOAD");

        if (other.CompareTag("BossSkill"))
        {
            Hp -= BossSkillDamage;
            UpdateHpUI();
        }

        if (other.CompareTag("CutScene") && playCount == 0)
        {
            playableDirector.Play(timelines[0]);
            playCut = true;
            StartCoroutine(MoveStopCoroutine());
            playCount = 1;
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionDis);
    }

    public void Damage()
    {
        Hp -= 7;
        UpdateHpUI();

        if (Hp <= 0)
        {
            GameObject gameOver = Instantiate(GameOver);
            animator.SetBool("Death", true);
        }
    }

    // --- Photon 동기화 ---
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Hp);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(Attack1);
        }
        else
        {
            Hp = (int)stream.ReceiveNext();
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            Attack1 = (bool)stream.ReceiveNext();

            UpdateHpUI();
        }
    }

    // --- 컷신 ---
    private IEnumerator MoveStopCoroutine()
    {
        yield return new WaitForSeconds(11.2f);
        playCut = false;
    }
}
