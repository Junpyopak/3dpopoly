using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : Character
{
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    SphereCollider sphereCollider;
    public int Hp = 70;
    public int AttackDamage = 7;
    Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("character").GetComponent<Player>();
        Speed = 2f;
        sphereCollider = GameObject.Find("LeftHand").GetComponent<SphereCollider>();
        // BoxCollider = GameObject.Find("AttackBox").GetComponent<BoxCollider>();
        animator = GameObject.Find("Warrok").GetComponent<Animator>();
        //BoxCollider.enabled = false;
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
        if(other.gameObject.layer==LayerMask.NameToLayer("Sward")) 
        {
            Damage();
            Debug.Log("���͵�����");
            Debug.Log($"����{Hp}");
        }
    }

    public void Damage()
    {
        Hp -= Player.AttackDamage;
        if (Hp <= 0)
        {
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
}
