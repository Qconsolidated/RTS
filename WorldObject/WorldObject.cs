using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class WorldObject : MonoBehaviour
{
    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    protected Bounds selectionBounds;
    protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    
    public string objectName;
    public Texture2D buildIcon;
    public int cost, sellValue, health, maxHealth, attack, defense;

    private void ChangeSelection(WorldObject worldObject, Player controller)
    {
        //this should be called by the following line, but there is an outside chance it will not
        SetSelection(false, playingArea);
        if (controller.SelectedObject) controller.SelectedObject.SetSelection(false, playingArea);
        controller.SelectedObject = worldObject;
        worldObject.SetSelection(true, GetPlayingArea());
    }
    private void DrawSelection()
    {
        GUI.skin = ResourceManager.SelectBoxSkin;
        Rect selectBox = WorkManager.CalculateSelectionBox(selectionBounds, playingArea);
        //Draw the selection box around the currently selected object, within the bounds of the playing area
        GUI.BeginGroup(playingArea);
        DrawSelectionBox(selectBox);
        GUI.EndGroup();
    }
    protected virtual void Awake()
    {
        selectionBounds = ResourceManager.InvalidBounds;
        CalculateBounds();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = transform.root.GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnGUI()
    {
        //if (currentlySelected) DrawSelection();
    }

    protected void SetTeamColor()
    {
        TeamColor[] teamColors = GetComponentsInChildren<TeamColor>();
        foreach (TeamColor color in teamColors) color.GetComponent<Renderer>().material.color = player.teamColor;
    }
    protected virtual void DrawSelectionBox(Rect selectionBox)
    {
        GUI.Box(selectionBox, "");
    }
    public virtual void SetSelection(bool selected, Rect playingArea)
    {
        currentlySelected = selected;
        if (selected) this.playingArea = playingArea;
    }

    public string[] GetActions()
    {
        return actions;
    }

    public virtual void PerformAction(string actionToPerform)
    {

    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        //only handle input if currently selected
        if (currentlySelected && hitObject && !hitObject.CompareTag("Ground"))
        {
            WorldObject worldObject = hitObject.GetComponent<WorldObject>();
            //clicked on another selectable object
            if (worldObject)
            {
                Resource resource = hitObject.transform.parent.GetComponent<Resource>();
                if (resource && resource.isEmpty()) return;
                ChangeSelection(worldObject, controller);
            }
        }
    }

    public virtual void RightMouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {

    }


    public virtual void SetHoverState(GameObject hoverObject)
    {
        if(player && player.isHuman && currentlySelected)
        {
            if (!hoverObject.CompareTag("Ground"))
            {
                Player owner = hoverObject.transform.root.GetComponent<Player>();
                Unit unit = hoverObject.transform.parent.GetComponent<Unit>();
                Building building = hoverObject.transform.parent.GetComponent<Building>();
                if (owner)
                {
                    if (owner.username == player.username) player.hud.SetCursorState(CursorState.Select);
                    else if (CanAttack()) player.hud.SetCursorState(CursorState.Attack);
                    else player.hud.SetCursorState(CursorState.Select);
                }
                else if (unit || building && CanAttack()) player.hud.SetCursorState(CursorState.Attack);
                else player.hud.SetCursorState(CursorState.Select);
            }
        }
    }

    public virtual bool CanAttack()
    {
        return false;
    }
    public void CalculateBounds()
    {
        selectionBounds = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            selectionBounds.Encapsulate(r.bounds);
        }
    }

    public Rect GetPlayingArea()
    {
        return new Rect(0, 0, 0, 0);
        //return new Rect(0, PanDetect, Screen.width - PanDetect, Screen.height - RESOURCE_BAR_HEIGHT);
    }

    public bool IsOwnedBy(Player owner)
    {
        if (player && player.Equals(owner))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
