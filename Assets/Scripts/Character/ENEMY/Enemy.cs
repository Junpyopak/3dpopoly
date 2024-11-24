using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    public float moveSpeed = 2f;
    public float AttakRange = 2;
    [SerializeField] LayerMask Layer;
    private IEnumerator Cortine;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Warrok").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Trace();
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
        if(TargetDistance>AttakRange&& TargetDistance < detectionDis)//�÷��̾��� ��ġ�� ���ù������� Ŭ���� �̵��ϱ�����
        {
            Attack();
            if (TargetDistance < detectionDis)
            {
                transform.LookAt(player);
                anim.SetBool("isWalk", true);
                //Ÿ�ٰ��� ���� ���
                Vector3 Detection = (player.position - transform.position).normalized;
                //�÷��̾� ��ġ ���󰡱�
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
                transform.position += Detection * moveSpeed * Time.deltaTime;
            }
        }   
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void OnDrawGizmos()
    {
        //Ž������Ȯ�ο�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);

        ;
        //���ݹ��� Ȯ��
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, AttakRange);
    }

    private void Attack()
    {
        Debug.Log("�÷��̾� ����");
    }
}
