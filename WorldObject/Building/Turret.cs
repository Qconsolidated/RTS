using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        actions = new string[] { "Attack, Upgrade, Sell" };
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override bool CanAttack()
    {
        return base.CanAttack();
    }
}
