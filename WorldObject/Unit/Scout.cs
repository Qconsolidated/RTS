using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Unit
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanAttack()
    {
        return false;
    }
}
