using UnityEngine;
using RTS;


public class Worker : Unit
{
    public float capacity, collectionAmount, depositAmount;
    public Building resourceStorage;
    public int buildSpeed;


    private bool gathering = false, unloading = false, building = false;
    private float currentLoad = 0.0f, currentDeposit = 0.0f, amountBuilt = 0.0f;
    private Resource resourceDeposit;
    private Building currentProject;
    private ResourceType resourceType;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!rotating && !moving)
        {
            if (gathering || unloading)
            {
                //Legs[] legs = GetComponentsInChildren<Legs>();
                //foreach (Arms arm in arms) arm.renderer.enabled = true;
                if (gathering)
                {
                    Collect();
                    if (currentLoad >= capacity || resourceDeposit.isEmpty())
                    {
                        //ensure load is a whole number
                        currentLoad = Mathf.Floor(currentLoad);
                        gathering = false;
                        unloading = true;
                        //foreach (Legs leg in legs) leg.renderer.enabled = false;
                        //StartMove(resourceStorage.transform.position, resourceStorage.gameObject);
                    }
                    else
                    {
                        Deposit();
                        if (currentLoad <= 0)
                        {
                            unloading = false;
                            //foreach (Legs leg in legs) leg.renderer.enable = false;
                            if (!resourceDeposit.isEmpty())
                            {
                                gathering = true;
                                //StartMove(resourceDeposit.transform.position, resourceDeposit.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void Init(Building creator)
    {
        base.Init(creator);
        resourceStorage = creator;
    }
    public override void SetHoverState(GameObject hoverObject)
    {
        base.SetHoverState(hoverObject);
        if (player && player.isHuman && currentlySelected)
        {
            if (!hoverObject.CompareTag("Ground"))
            {
                Resource resource = hoverObject.transform.parent.GetComponent<Resource>();
                if (resource && !resource.isEmpty())
                {
                    if (player.SelectedObject) player.SelectedObject.SetSelection(false, playingArea);
                    SetSelection(true, playingArea);
                    player.SelectedObject = this;
                    Gather(resource);
                }
            }
            else StopGather();
        }
    }

    private void Gather(Resource resource)
    {
        resourceDeposit = resource;
        //StartMove(resource.transform.position, resource.gameObject);

        if(resourceType == ResourceType.Unknown || resourceType != resource.GetResourceType())
        {
            resourceType = resource.GetResourceType();
            currentLoad = 0.0f;
        }
        gathering = true;
        unloading = false;
    }

    private void StopGather()
    {

    }

    private void Collect()
    {
        float collection = collectionAmount * Time.deltaTime;
        if (currentLoad + collection > capacity) collection = capacity - currentLoad;
        resourceDeposit.Remove(collection);
        currentLoad += collection;
    }

    private void Deposit()
    {
        int deposit = Mathf.FloorToInt(currentDeposit);
        if (deposit >= 1)
        {
            if (deposit > currentLoad) deposit = Mathf.FloorToInt(currentLoad);
            currentDeposit -= deposit;
            currentLoad -= deposit;
            ResourceType depositType = resourceType;
            if (resourceType == ResourceType.Minerals) depositType = ResourceType.Minerals;
            player.AddResource(depositType, deposit);
        }
    }
}
