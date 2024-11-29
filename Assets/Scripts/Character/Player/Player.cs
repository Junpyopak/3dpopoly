using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody rigidbody;
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
    public int AttackDamage = 20;
    MeshCollider MeshCollider;
    // Start is called before the first frame update

    void Start()
    {
        Enemy = GameObject.Find("Warrok").GetComponent<Enemy>();
        Speed = 3f;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GameObject.Find("character").GetComponent<Animator>();
        camera = Camera.main;
        chController = GameObject.Find("character").GetComponent<CharacterController>();
        MeshCollider = GameObject.Find("Weapon").GetComponent<MeshCollider>();
        MeshCollider.enabled = false;
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
            moveFast = true;
            Speed = runSpeed;
            animator.SetBool("isRun", moveFast);
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

        Movement();

    }
    private void LateUpdate()
    {
        Vector3 playerRaotation = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRaotation), Time.deltaTime * Smove);
    }
    void Movement()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        if (moveDir != Vector3.zero)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
        chController.Move(moveDir.normalized * Speed * Time.deltaTime);
    }
    private void StartAttack()
    {
        Attack1 = true;
    }
    void EndAttack1()
    {
        Attack1 = false;
        MeshCollider.enabled = false;
    }
    void EndAttack2()
    {
        animator.SetBool("Atk2", false);
        MeshCollider.enabled = false;
        Debug.Log("어택2끝");
    }

    protected override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Attack1 == false)
            {
                MeshCollider.enabled = true;
                animator.SetTrigger("Atk1");
            }
            else if (animator.GetBool("Atk2") == false)
            {
                MeshCollider.enabled = true;
                animator.SetBool("Atk2", true);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MonsterAttackBox"))
        {
            Damage();
            Debug.Log("데미지");
            Debug.Log($"스텟{Hp}");
        }
    }



    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public void Damage()
    {
        Hp -= Enemy.AttackDamage;
        if (Hp <= 0)
        {
            Debug.Log("죽었습니다");
        }
    }
}
