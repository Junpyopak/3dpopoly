using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    protected float Range =2f;
    protected float Speed ;//��ӹ޴� Ŭ�����鿡�� ���� ���� ������
    //protected int Hp;
    //protected int Attack_Damage;
    //protected abstract void SetSpeed();
    public abstract void Attack();
    public abstract void Moved();


    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, Range);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
    //public void Damage(int  damage)
    //{
    //    Hp -= damage;
    //    if(Hp <= 0)
    //    {
    //        Debug.Log("�׾����ϴ�");
    //    }
    //}
    //public int GetAttack_Power()
    //{
    //    return Attack_Damage;
    //}
}