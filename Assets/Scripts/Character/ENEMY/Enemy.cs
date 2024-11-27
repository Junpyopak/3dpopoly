using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : Character
{
    public float detectionDis = 3f;//탐지 거리
    public Transform player;//플레이어 위치 찾기위함용
    public float moveSpeed = 2f;
    public float AttakRange = 2;
    [SerializeField] LayerMask Layer;
    private IEnumerator Cortine;
    Animator anim;
    BoxCollider BoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider = GameObject.Find("AttackBox").GetComponent<BoxCollider>();
        anim = GameObject.Find("Warrok").GetComponent<Animator>();
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

    private void Trace()//타겟추적
    {

        //플레이어와의 거리 계산
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if(TargetDistance>AttakRange&& TargetDistance < detectionDis)//플레이어의 위치가 어택범위보다 클때만 이동하기위함
        {
            if (TargetDistance < detectionDis)
            {
                transform.LookAt(player);
                anim.SetBool("isWalk", true);
                //타겟과의 방향 계산
                Vector3 Detection = (player.position - transform.position).normalized;
                //플레이어 위치 따라가기
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
        //탐지지점확인용
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectionDis);

        ;
        //공격범위 확인
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, AttakRange);
    }

    protected override void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < AttakRange)//공격범위에 들어왔을때 공격 에님 돌아가기
        {
            anim.SetBool("isAttack", true);
            Debug.Log("플레이어 공격");
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }

    //private void Attack()
    //{
    //    float distance = Vector3.Distance(transform.position, player.position);
    //    if (distance < AttakRange)//공격범위에 들어왔을때 공격 에님 돌아가기
    //    {
    //        anim.SetBool("isAttack", true);
    //        Debug.Log("플레이어 공격");
    //    }
    //    else
    //    {
    //        anim.SetBool("isAttack", false);
    //    }
    //}

}
