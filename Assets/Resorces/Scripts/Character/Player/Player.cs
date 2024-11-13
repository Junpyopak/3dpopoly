using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody rigidbody;
    Animator anim;
    public float speed = 3f;
    public float jumpFor = 3f;
    private Vector3 moveDir = Vector3.zero;
    private float rotate = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        moveDir.Normalize();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jumpPower = Vector3.up * jumpFor;
            rigidbody.AddForce(jumpPower,ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero)
        {
            transform.forward = moveDir;
            anim.SetBool("isWalk", true);
            //Vector3.Lerp(transform.forward,moveDir,rotate*Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalk",false);
        }

        rigidbody.MovePosition (this.transform.position + moveDir * speed * Time.deltaTime);
    }
}
