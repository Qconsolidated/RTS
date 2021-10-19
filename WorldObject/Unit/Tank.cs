using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    // Start is called before the first frame update
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
