using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;
public class Player : MonoBehaviour
{
    private Dictionary<ResourceType, int> resources, resourceCaps;
    
    public string username;
    public bool isHuman;
    public HUD hud;
    public int minerals, maxMinerals, gas, maxGas;
    public Color teamColor;
    
    
    public WorldObject SelectedObject { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponentInChildren<HUD>();
        AddStartingResources();
        SetStartingResourceCapacity();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHuman)
        {
            hud.SetResourceValues(resources, resourceCaps);
        }
    }

    void Awake()
    {
        resources = InitResourceList();
        resourceCaps = InitResourceList();
    }

    Dictionary<ResourceType, int> InitResourceList()
    {
        Dictionary<ResourceType, int> list = new Dictionary<ResourceType, int>
        {
            { ResourceType.Minerals, 0 },
            { ResourceType.Gas, 0 }
        };
        return list;
    }

    void AddStartingResources()
    {
        AddResource(ResourceType.Minerals, minerals);
        AddResource(ResourceType.Gas, gas);
    }

    void SetStartingResourceCapacity()
    {
        IncrementResourceCapacity(ResourceType.Minerals, maxMinerals);

        IncrementResourceCapacity(ResourceType.Gas, maxGas);
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }

    public void IncrementResourceCapacity(ResourceType type, int amount)
    {
        resourceCaps[type] += amount;
    }

    public void AddUnit(string unitName, Vector3 spawnPoint,
        Vector3 rallyPoint, Quaternion rotation, Building creator)
    {
        Units units = GetComponentInChildren<Units>();
        GameObject newUnit = (GameObject)Instantiate(ResourceManager.GetUnit(unitName), spawnPoint, rotation);
        newUnit.transform.parent = units.transform;
        Unit unitObject = newUnit.GetComponent<Unit>();
        if (unitObject && spawnPoint != rallyPoint) unitObject.StartMove(rallyPoint);
        Debug.Log("add " + unitName + "to player");

        if (unitObject)
        {
            unitObject.Init(creator);
            if (spawnPoint != rallyPoint) unitObject.StartMove(rallyPoint);
        }
    }
}
