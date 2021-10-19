using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    // Start is called before the first frame update
    protected override  void Start()
    {
        base.Start();
        actions = new string[] { "Infantry", "Tank", "Worker" };
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

}

