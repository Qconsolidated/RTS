using UnityEngine;
using UnityEngine.AI;
using RTS;
public class Unit : WorldObject
{
    /*** Game Engine methods, all can be overridden by subclass ***/
    public float moveSpeed, rotateSpeed;
    public int energy;
    public GameObject selectionIndicator;

    protected bool moving, rotating;

    private Vector3 destination;
    private Quaternion targetRotation;
    private NavMeshAgent agent;
    private RaycastHit hit;
    

    
    

    //private void MakeMove()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
    //    if (transform.position == destination) moving = false;
    //    CalculateBounds();
    //}

    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();
        selectionIndicator.SetActive(currentlySelected);
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }
    public virtual void Init(Building creator) {  }
    public override void SetHoverState(GameObject hoverObject)
    {
        base.SetHoverState(hoverObject);
        //only handle input if owner is a human player and unit is currently selected
        if (player && player.isHuman && currentlySelected)
        {
            bool moveHover = false;
            if (hoverObject.CompareTag("Ground"))
            {
                moveHover = true;
            }
            else
            {
                Resource resource = hoverObject.transform.parent.GetComponent<Resource>();
                if (resource && resource.isEmpty()) moveHover = true;
            }

            if (moveHover) player.hud.SetCursorState(CursorState.Move);
        }
    }
    public override void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        base.MouseClick(hitObject, hitPoint, controller);
        //only handle input if object is selected and owned by a human player
        if (player && player.isHuman && currentlySelected)
        {
            bool hitEmptyResource = false;
            if (hitObject.transform.parent)
            {
                Resource resource = hitObject.transform.parent.GetComponent<Resource>();
                if (resource && resource.isEmpty()) hitEmptyResource = true;
            }
            /*if (player.SelectedObject.CompareTag("Unit") && (hitObject.CompareTag("Ground") || hitEmptyResource) && hitPoint != ResourceManager.InvalidPosition) 
            {
               MakeMove(agent, hit);
            }*/
        }
    }

    public override void RightMouseClick(GameObject hitObject, Vector3 destination, Player controller)
    {
        base.RightMouseClick(hitObject, destination, controller);
        if (player && player.isHuman && currentlySelected)
        {
            if (player.SelectedObject.CompareTag("Unit") && hitObject.CompareTag("Ground") && destination != ResourceManager.InvalidPosition)
            {
                MakeMove(agent, hit);
            }
        }
    }

    public void StartMove(Vector3 destination)
    {
        if (player.SelectedObject.CompareTag("Unit")) 
        { 
            this.destination = destination;
            targetRotation = Quaternion.LookRotation(destination - transform.position);
            rotating = true;
            moving = false;
        }
    }


    private void MakeMove(NavMeshAgent agent, RaycastHit hit)
    {
        if (player.SelectedObject.CompareTag("Unit")) 
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Ground"))
                    agent.destination = hit.point;
            }
        }
    }

    private void TurnToTarget()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed); // smooth transition from the current rotation towards the desired
        Quaternion inverseTargetRotation = new Quaternion(-targetRotation.x, -targetRotation.y,
            -targetRotation.z, -targetRotation.w);  //This fixes rotation occasionally being stuck at exactly 180 degrees
        if (transform.rotation == targetRotation || transform.rotation == inverseTargetRotation)
        {
            rotating = false;
            moving = true;
        }
        CalculateBounds();
    }

}
