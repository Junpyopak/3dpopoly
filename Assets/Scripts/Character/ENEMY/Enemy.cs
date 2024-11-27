using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : Character
{
    public float detectionDis = 3f;//탐지 거리
    public Transform player;//플레이어 위치 찾기위함용
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

    private void Trace()//타겟추적
    {

        //플레이어와의 거리 계산
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if(TargetDistance>Range&& TargetDistance < detectionDis)//플레이어의 위치가 어택범위보다 클때만 이동하기위함
        {
            if (TargetDistance < detectionDis)
            {
                transform.LookAt(player);
                animator.SetBool("isWalk", true);
                //타겟과의 방향 계산
                Vector3 Detection = (player.position - transform.position).normalized;
                //플레이어 위치 따라가기
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
    //    //탐지지점확인용
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(this.transform.position, detectionDis);

    //    ;
    //    //공격범위 확인
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(this.transform.position, Range);
    //}

    public override void OnDrawGizmos()
    {
        //탐지지점확인용
        base.OnDrawGizmos();
        //공격범위 확인
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }

    protected override void Attack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < Range)//공격범위에 들어왔을때 공격 에님 돌아가기
        {
            animator.SetBool("isAttack", true);
            Debug.Log("플레이어 공격");
        }
        else
        {
            animator.SetBool("isAttack", false);
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Sward")) 
        {
            Debug.Log("몬스터데미지");
        }
    }

}
