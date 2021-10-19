using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RTS;
/*public class Worker : Unit
{
    public TaskList task;
    public NodeManager.ResourceTypes heldResourceType;
   // public ResourceManager RM;
    public bool isGathering = false;

    public int heldResource;
    public int maxHeldResource;
    
    GameObject[] dropOffPoints;
    public GameObject targetNode;
    private NavMeshAgent agent;
    private ActionList actionList;
 

    
    // Start is called before the first frame update
    protected override void Start()
    {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();
        actionList = FindObjectOfType<ActionList>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (targetNode == null)
        {
            if (heldResource != 0)
            {
                dropOffPoints = GameObject.FindGameObjectsWithTag("Drop");
                agent.destination = GetClosestDropOff(dropOffPoints).transform.position;
                dropOffPoints = null;
                task = TaskList.Delivering;
            }
            else 
            {
                task = TaskList.Idle;
            }
        }
        if (heldResource >= maxHeldResource)
        {
            //Drop off point here
            dropOffPoints = GameObject.FindGameObjectsWithTag("Drop");
            agent.destination = GetClosestDropOff(dropOffPoints).transform.position;
            dropOffPoints = null;
            task = TaskList.Delivering;
        }
        if (Input.GetMouseButtonDown(1) && player && player.isHuman && currentlySelected)
        {
            RightClick();
        }
    }
    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
        GameObject closestDrop = null;

        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject targetDrop in dropOffs)
        {
            Vector3 direction = targetDrop.transform.position - position;
            float distance = direction.sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDrop = targetDrop;
            }
        }

        return closestDrop;
    }

    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                actionList.Move(agent, hit);
                task = TaskList.Moving;
            }
            else if (hit.collider.CompareTag("Resource"))
            {
                actionList.Move(agent, hit);
                targetNode = hit.collider.gameObject;
                task = TaskList.Gathering;
            }
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    GameObject hitObject = other.gameObject;

    //    if (hitObject.CompareTag("Resource") && task == TaskList.Gathering)
    //    {
    //        isGathering = true;
    //        hitObject.GetComponent<NodeManager>().gatherers++;
    //        heldResourceType = hitObject.GetComponent<NodeManager>().resourceType;
    //    }
    //    else if (hitObject.CompareTag("Drop") && task == TaskList.Delivering)
    //    {
    //        if (RM.stone >= RM.maxStone)
    //        {
    //            task = TaskList.Idle;
    //        }
    //        else
    //        {
    //            RM.stone += heldResource;
    //            heldResource = 0;
    //            task = TaskList.Gathering;
    //            agent.destination = targetNode.transform.position;
    //        }
    //    }
    //}
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.CompareTag("Resource"))
        {
            isGathering = false;
            hitObject.GetComponent<NodeManager>().gatherers--;
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering) { heldResource++; }
        }
    }
}
*/