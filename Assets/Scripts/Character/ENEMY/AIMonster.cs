using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class AIMonster : Enemy_AI 
{
    SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GameObject.Find("LeftHand").GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
    }
    private void Update()
    {
        Search();
        Move();
        Attack();
    }
    protected override void Search()
    {
        base.Search();
    }
    protected override void Move()
    {
        base.Move();
    }
    protected override void Atack()
    {
        base.Atack();
    }
    public void StartAttack()
    {
        sphereCollider.enabled = true;
    }

    public void EndAttack()
    {
        sphereCollider.enabled = false;
    }
}
