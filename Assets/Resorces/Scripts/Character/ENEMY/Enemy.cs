using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    public float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Trace();
    }

    private void Trace()//Ÿ������
    {
        //�÷��̾���� �Ÿ� ���
        float TargetDistance = Vector3.Distance(transform.position,player.position);
        if(TargetDistance<detectionDis)
        {
            transform.LookAt(player);
            //Ÿ�ٰ��� ���� ���
            Vector3 Detection = (player.position - transform.position).normalized;       
            //�÷��̾� ��ġ ���󰡱�
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
            transform.position += Detection*moveSpeed*Time.deltaTime;
            
        }
    }
    
    private void OnDrawGizmos()
    {
        //Ž������Ȯ�ο�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);
    }
}
