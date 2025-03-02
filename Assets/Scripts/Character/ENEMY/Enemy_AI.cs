using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;

public class Enemy_AI : Enemy
{
    protected Character Character;
    //public Vector3 SearchPoint;
    protected eAi AiState = eAi.eAI_CREATE;
    public void Init(Character _Character)
    {
        Character = _Character;
    }
    public void State()
    {
        switch (AiState)
        {
            case eAi.eAI_CREATE:
                Create();
                break;
            case eAi.eAI_SEARCH:
                Search();
                break;
            case eAi.eAI_MOVE:
                Move();
                break;
            case eAi.eAI_RESET:
                Reset();
                break;
                case eAi.eAI_ATTACK:
                Attack(); break;
        }
    }

    protected virtual void Create()
    {
        if (Character = null)
        {
            //객체생성
            if (Character != null)
            {
                AiState = eAi.eAI_SEARCH;
            }
        }
    }
    protected virtual void Search()
    {
        //조건 성사시 무브로
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if (TargetDistance > Range && TargetDistance < detectionDis)//플레이어의 위치가 어택범위보다 클때만 이동하기위함
        {
            AiState = eAi.eAI_MOVE;
        }
    }
    protected virtual void Move()
    {
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if (TargetDistance > Range && TargetDistance < detectionDis)
        {
            transform.LookAt(player);
            animator.SetBool("isWalk", true);
            //타겟과의 방향 계산
            Vector3 Detection = (player.position - transform.position).normalized;
            //플레이어 위치 따라가기
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,player.position,moveSpeed*Time.deltaTime);
            transform.position += Detection * Speed * Time.deltaTime;
            AiState = eAi.eAI_ATTACK;
        }
        else
        {
            animator.SetBool("isWalk", false);
            AiState = eAi.eAI_SEARCH;
        }
    }
    protected virtual void Reset()
    {
        AiState = eAi.eAI_SEARCH;
    }
    protected virtual void Atack()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < Range)//공격범위에 들어왔을때 공격 에님 돌아가기
        {
            //BoxCollider.enabled = true;
            animator.SetBool("isAttack", true);
            Debug.Log("플레이어 공격");
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }
    protected virtual void Damage()
    {
        base.Damage();
    }
}
