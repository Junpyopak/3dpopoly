using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class AIMonster : Enemy_AI 
{
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
}
