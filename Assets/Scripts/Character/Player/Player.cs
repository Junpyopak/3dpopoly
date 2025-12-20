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
    [SerializeField] private Text levelUpNoticeText;

    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private ButtonFade Levelbtn;

    [SerializeField] private GameObject levelUpNoticePanel;
    [SerializeField] private float levelUpNoticeDuration = 2f;

    // --- Static 저장 변수 ---
    private static int savedAttackDamage = -1;
    public static int savedLevel = 1;
    public static int savedHp = -1;
    public static int savedExp = 0;
    public static int savedMaxHp = 100;

    PhotonView pv;

    // --- 초기화 ---
    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        if (savedAttackDamage != -1) AttackDamage = savedAttackDamage;
        if (savedLevel != 1) level = savedLevel;
        if (savedHp != -1) Hp = savedHp;
        if (savedMaxHp != 100) MaxHp = savedMaxHp;
        currentExp = savedExp;
        moveStrategy = new WalkStrategy(this);
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
        if (!pv.IsMine) return;//내 캐릭터만 움직이도록
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
            SetMove(new WalkStrategy(this));
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
    void FootSetSound()
    {
        SoundManager.Instance.PlaySFX("Footstep", 0.5f);
    }
    void RunSetSound()
    {
        SoundManager.Instance.PlaySFX("RunStep", 0.5f);
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

    private IEnumerator ShowLevelUpNotice()
    {
        if (levelUpNoticePanel != null)
        {
            levelUpNoticePanel.SetActive(true);
            yield return new WaitForSeconds(levelUpNoticeDuration);
            levelUpNoticePanel.SetActive(false);
        }
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
        if (levelUpNoticeText != null)
            levelUpNoticeText.text = $"Level {level}";
        StartCoroutine(ShowLevelUpNotice());
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

    //  Photon 동기화 
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

    //  컷신 
    private IEnumerator MoveStopCoroutine()
    {
        yield return new WaitForSeconds(11.2f);
        playCut = false;
    }
    public void StartTrail()
    {
        if (slashEffect != null)
        {

            slashEffect.Slash_On();
        }

    }

    public void StopTrail()
    {
        if (slashEffect != null)
        {
            slashEffect.Slash_Off();
        }
    }
    void AtkSound()
    {
        SoundManager.Instance.PlaySFX("AttackSound");
    }
}
