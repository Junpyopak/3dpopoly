using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public bool Attack1 = false;
    Enemy Enemy;
    public int Hp = 100;
    public int MaxHp = 100;
    public int AttackDamage = 20;
    BoxCollider BoxCollider;
    [SerializeField]HpGauge hpGauge;
    [SerializeField] DamaeText DamaeText;
    public GameObject damageText;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;
        Enemy = GameObject.Find("Warrok").GetComponent<Enemy>();
        Speed = 3f;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GameObject.Find("character").GetComponent<Animator>();
        camera = Camera.main;
        chController = GameObject.Find("character").GetComponent<CharacterController>();
        //MeshCollider = GameObject.Find("Weapon").GetComponent<MeshCollider>();
        //MeshCollider.enabled = false;
        BoxCollider = GameObject.Find("Weapon").GetComponent<BoxCollider>();
        BoxCollider.enabled = false;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jumpPower = Vector3.up * jumpFor;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        Moved();

    }
    private void LateUpdate()
    {
        Vector3 playerRaotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRaotation), Time.deltaTime * Smove);
    }
    public override void Moved()
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
    //void Movement()
    //{

    //    Vector3 forward = transform.TransformDirection(Vector3.forward);
    //    Vector3 right = transform.TransformDirection(Vector3.right);

    //    Vector3 moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
    //    if (moveDir != Vector3.zero)
    //    {
    //        animator.SetBool("isWalk", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("isWalk", false);
    //    }
    //    chController.Move(moveDir.normalized * Speed * Time.deltaTime);
    //}
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
        if (Input.GetMouseButtonDown(0))
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
        if (other.gameObject.layer == LayerMask.NameToLayer("MonsterAttackBox"))
        {          
            Damage();         
            Debug.Log("데미지");
            Debug.Log($"스텟{Hp}");
        }
        if(other.gameObject.CompareTag("Gate"))
        {
            Debug.Log("게이트");
            SceneManager.LoadScene("BossMapLOAD");
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public void Damage()
    { 
        Hp -= Enemy.AttackDamage;
        GameObject Text = Instantiate(damageText);
        Text.GetComponent<DamaeText>();    
        hpGauge.SetPlayerHp(Hp, MaxHp);
        if (Hp <= 0)
        {
            
            Debug.Log("죽었습니다");
        }
    }
}
