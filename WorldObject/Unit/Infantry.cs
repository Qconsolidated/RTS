using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit
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
        return true;
    }
}
