using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionDis = 3f;//Ž�� �Ÿ�
    public Transform player;//�÷��̾� ��ġ ã�����Կ�
    public float moveSpeed = 2f;
    public float Mindetect = 3;
    [SerializeField] LayerMask Layer;
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

    private void Trace()//Ÿ������
    {
        //�÷��̾���� �Ÿ� ���
        float TargetDistance = Vector3.Distance(transform.position,player.position);
        if(TargetDistance<detectionDis)
        {
            transform.LookAt(player);
            anim.SetBool("isWalk", true);
            //Ÿ�ٰ��� ���� ���
            Vector3 Detection = (player.position - transform.position).normalized;       
            //�÷��̾� ��ġ ���󰡱�
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
            transform.position += Detection*moveSpeed*Time.deltaTime;

            Vector3 dis = transform.forward;
            Ray ray = new Ray(transform.position, dis);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mindetect, Layer);
            if(hit.collider)
            {
                Debug.Log("�÷��̾� Ȯ��");
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

        Vector3 offset = new Vector3(0, 1, 0); 
        Vector3 Pos = transform.position + offset;
        Vector3 dis = Vector3.forward;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Pos, Pos+dis * Mindetect);
    }


}
