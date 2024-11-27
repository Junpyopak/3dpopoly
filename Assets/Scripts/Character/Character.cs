using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    protected float Range;
    protected float Speed ;//��ӹ޴� Ŭ�����鿡�� ���� ���� ������
    protected float Damage;
    protected float Hp;
    //protected abstract void SetSpeed();
    protected abstract void Attack();
    public  Character() { }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }
}