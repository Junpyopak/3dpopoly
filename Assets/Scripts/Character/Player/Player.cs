using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody rigidbody;
    Animator anim;
    Camera camera;
    CharacterController chController;
    public float speed = 3f;
    public float runSpeed = 7f;
    public float jumpFor = 3f;
    private float rotate = 3f;
    private bool moveFast;
    public float Smove = 10f;
    public float Range = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        anim = GameObject.Find("character").GetComponent<Animator>();
        camera = Camera.main;
        chController = GameObject.Find("character").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveDir.x = Input.GetAxis("Horizontal");
        //moveDir.z = Input.GetAxis("Vertical");
        //moveDir = new Vector3(moveDir.x, 0, moveDir.z).normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveFast = true;
            speed = runSpeed;
            anim.SetBool("isRun", moveFast);
        }
        else
        {
            moveFast = false;
            speed = 3;
            anim.SetBool("isRun", moveFast);
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
        if(moveDir != Vector3.zero)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
        chController.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }
}
