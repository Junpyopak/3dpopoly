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
            //��ü����
            if (Character != null)
            {
                AiState = eAi.eAI_SEARCH;
            }
        }
    }
    protected virtual void Search()
    {
        //���� ����� �����
        float TargetDistance = Vector3.Distance(transform.position, player.position);
        if (TargetDistance > Range && TargetDistance < detectionDis)//�÷��̾��� ��ġ�� ���ù������� Ŭ���� �̵��ϱ�����
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
            //Ÿ�ٰ��� ���� ���
            Vector3 Detection = (player.position - transform.position).normalized;
            //�÷��̾� ��ġ ���󰡱�
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
    protected virtual void Damage()
    {
        base.Damage();
    }
}
