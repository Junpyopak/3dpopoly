using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//public class WalkStrategy :Player,MoveStrategy
//{
//    CharacterController chController;
//    Player player;
//    private void Start()
//    {
//        chController = GameObject.Find("character").GetComponent<CharacterController>();
//        player = GameObject.Find("character").GetComponent<Player>();
//    }
//    public void Move()
//    {
//        //Vector3 forward = player.transform.TransformDirection(Vector3.forward);
//        //Vector3 right = player.transform.TransformDirection(Vector3.right);

//        //Vector3 moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
//        //if (moveDir != Vector3.zero)
//        //{
//        //    animator.SetBool("isWalk", true);
//        //    Move();
//        //}
//        //else
//        //{
//        //    animator.SetBool("isWalk", false);
//        //}
//        //chController.Move(moveDir.normalized * Speed * Time.deltaTime);
//        Debug.Log("∞»±‚∆–≈œ");
//    }
//}
public class WalkStrategy : MoveStrategy
{
    CharacterController chController;
    Player player;

    public WalkStrategy(Player player)
    {
        this.player = player;
        chController = player.GetComponent<CharacterController>();
    }

    public void Move()
    {
        Debug.Log("∞»±‚∆–≈œ");
    }
}
