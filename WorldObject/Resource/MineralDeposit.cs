using UnityEngine;
using RTS;

public class MineralDeposit : Resource
{
    private int numBlocks;

    protected override void Start()
    {
        base.Start();
        numBlocks = GetComponentsInChildren<Mineral>().Length;
        resourceType = ResourceType.Minerals;
    }

    protected override void Update()
    {
        base.Update();
        float percentLeft = (float)amountLeft / (float)capacity;
        if (percentLeft < 0) percentLeft = 0;
        int numBlocksToShow = (int)(percentLeft * numBlocks);
        Mineral[] blocks = GetComponentsInChildren<Mineral>();
        if (numBlocksToShow >= 0 && numBlocksToShow < blocks.Length)
        {
            Mineral[] sortedBlocks = new Mineral[blocks.Length];

            foreach (Mineral mineral in blocks)
            {
                sortedBlocks[blocks.Length - int.Parse(mineral.name)] = mineral;
            }
            for (int i = numBlocksToShow; i < sortedBlocks.Length; i++)
            {
                sortedBlocks[i].GetComponent<Renderer>().enabled = false;
            }
            CalculateBounds();
        }
    }
}
