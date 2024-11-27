using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected float Speed ;//상속받는 클래스들에서 많이 쓰는 변수들
    protected float Damage;
    protected float Hp;
    //protected abstract void SetSpeed();
    protected abstract void Attack();
    public  Character() { }
}