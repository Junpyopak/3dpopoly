using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : Character
{
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    [SerializeField] LayerMask Layer;
    BoxCollider BoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        Range = 2f;
        Speed = 2f;
        BoxCollider = GameObject.Find("AttackBox").GetComponent<BoxCollider>();
        animator = GameObject.Find("Warrok").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Trace();
        Attack();
    }

    public IEnumerator StopTrace(float wait)
    {
        while (true)
        {
            yield return new WaitForSeconds(wait);
            Trace();
        }

    }

    private void Trace()//Ÿ������
    {

        //�÷��̾���� �Ÿ� ���
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if(TargetDistance>Range&& TargetDistance < detectionDis)//�÷��̾��� ��ġ�� ���ù������� Ŭ���� �̵��ϱ�����
        {
            if (TargetDistance < detectionDis)
            {
                transform.LookAt(player);
                animator.SetBool("isWalk", true);
                //Ÿ�ٰ��� ���� ���
                Vector3 Detection = (player.position - transform.position).normalized;
                //�÷��̾� ��ġ ���󰡱�
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
                transform.position += Detection * Speed * Time.deltaTime;
               
            }
        }   
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    //Ž������Ȯ�ο�
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(this.transform.position, detectionDis);

    //    ;
    //    //���ݹ��� Ȯ��
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(this.transform.position, Range);
    //}

    public override void OnDrawGizmos()
    {
        //Ž������Ȯ�ο�
        base.OnDrawGizmos();
        //���ݹ��� Ȯ��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }

    protected override void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < Range)//���ݹ����� �������� ���� ���� ���ư���
        {
            animator.SetBool("isAttack", true);
            Debug.Log("�÷��̾� ����");
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    //private void Attack()
    //{
    //    float distance = Vector3.Distance(transform.position, player.position);
    //    if (distance < AttakRange)//���ݹ����� �������� ���� ���� ���ư���
    //    {
    //        anim.SetBool("isAttack", true);
    //        Debug.Log("�÷��̾� ����");
    //    }
    //    else
    //    {
    //        anim.SetBool("isAttack", false);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Sward")) 
        {
            Debug.Log("���͵�����");
        }
    }

}
