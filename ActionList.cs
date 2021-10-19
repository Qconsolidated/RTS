using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ActionList : MonoBehaviour
{
    // Start is called before the first frame update
    public void Move(NavMeshAgent agent, RaycastHit hit)
    {
        agent.destination = hit.point;
        Debug.Log("Moving");
    }

    public void Gather(NavMeshAgent agent, GameObject targetNode, RaycastHit hit, TaskList task)
    {
        agent.destination = hit.collider.gameObject.transform.position;
        task = TaskList.Gathering;
        targetNode = hit.collider.gameObject;
    }
}
