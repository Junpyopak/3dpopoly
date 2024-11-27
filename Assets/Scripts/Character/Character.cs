using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected float Speed = 3;

    //protected abstract void SetSpeed();
    protected abstract void Attack();
    public  Character() { }
}