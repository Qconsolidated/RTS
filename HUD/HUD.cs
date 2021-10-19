using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class HUD : MonoBehaviour
{
    private const int RESOURCE_BAR_HEIGHT = 40, INFO_PANEL_WIDTH = 150,
        SELECTION_NAME_HEIGHT = 15, BUILD_IMAGE_WIDTH = 64, BUILD_IMAGE_HEIGHT = 64, BUTTON_SPACING = 7,
        SCROLL_BAR_WIDTH = 22;
    private Player player;
    private string selectionName = "";
    private CursorState activeCursorState, previousCursorState;
    private int currentFrame = 0, buildAreaHeight = 0;
    private Dictionary<ResourceType, int> resourceValues, resourceCaps;
    private WorldObject lastSelection;
    private float sliderValue;
    public GUISkin selectBoxSkin, mouseCursorSkin, resourceSkin, ordersSkin;
    public Texture2D activeCursor;
    public Texture2D selectCursor, leftCursor, rightCursor, upCursor, downCursor, buttonHover, buttonClick;
    public Texture2D[] moveCursors, attackCursors, gatherCursors, resources;

    public float minerals;
    public float maxMinerals;
    public float gas;
    public float maxGas;

    public float population;
    public float maxPopulation;

    public Text mineralDisp;
    public Text gasDisp;

    public Text populationDisp;

    // Start is called before the first frame update
    void Start()
    {
        resourceValues = new Dictionary<ResourceType, int>();
        resourceCaps = new Dictionary<ResourceType, int>();
        player = transform.root.GetComponent<Player>();
        ResourceManager.StoreSelectBoxItems(selectBoxSkin);
        SetCursorState(CursorState.Select);
        buildAreaHeight = Screen.height - RESOURCE_BAR_HEIGHT - SELECTION_NAME_HEIGHT - 2 * BUTTON_SPACING;
        for (int i = 0; i < resources.Length; i++)
        {
            switch (resources[i].name)
            {
                case "Minerals":
                    //resourceImages.Add(ResourceType.Minerals, resources[i]);
                    resourceValues.Add(ResourceType.Minerals, 0);
                    resourceCaps.Add(ResourceType.Minerals, 0);
                    break;
                case "Gas":
                    //resourceImages.Add(ResourceType.Gas, resources[i]);
                    resourceValues.Add(ResourceType.Gas, 0);
                    resourceCaps.Add(ResourceType.Gas, 0);
                    break;
                default: break;
            }
        }
    }

    private void OnGUI()
    {
        if (player && player.isHuman)
        {
           // DrawResourceBar();
          //  DrawOrdersBar();
            if (player.SelectedObject)
            {
                selectionName = player.SelectedObject.objectName;
            }
            if (!selectionName.Equals(""))
            {
                GUI.Label(new Rect(0, 10, INFO_PANEL_WIDTH, SELECTION_NAME_HEIGHT), selectionName);
            }
            DrawMouseCursor();
        }
    }
    private void DrawMouseCursor()
    {
        bool mouseOverHud = !MouseInBounds() && activeCursorState != CursorState.PanRight && activeCursorState != CursorState.PanUp;

        if (mouseOverHud)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            GUI.skin = mouseCursorSkin;
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            UpdateCursorAnimation();
            Rect cursorPosition = GetCursorDrawPosition();
            GUI.Label(cursorPosition, activeCursor);
            GUI.EndGroup();
        }
    }
    /*private void DrawResourceIcon(ResourceType type, int iconLeft, int textLeft, int topPos)
    {
        Texture2D icon = resourceImages[type];
        string text = resourceValues[type].ToString() + "/" + resourceLimits[type].ToString();
        GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), icon);
        GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), text);
    }*/

    private void DrawOrdersBar()
    {
        GUI.skin = ordersSkin;
        GUI.BeginGroup(new Rect(Screen.width - INFO_PANEL_WIDTH, RESOURCE_BAR_HEIGHT, INFO_PANEL_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0,0,INFO_PANEL_WIDTH, Screen.height-RESOURCE_BAR_HEIGHT), "");
        string selectionName = "";
        if (player.SelectedObject)
        {
            selectionName = player.SelectedObject.objectName;
        }
        if (player.SelectedObject.IsOwnedBy(player))
        {
            if (lastSelection && lastSelection != player.SelectedObject) sliderValue = 0.0f;
            //DrawActions(player.SelectedObject.GetActions());
            //store current selection
            lastSelection = player.SelectedObject;
            Building selectedBuilding = lastSelection.GetComponent<Building>();
           //if (selectedBuilding) DrawBuildQUeue(selectedBuilding.getBuildQueueValues(), selectedBuilding.getBuildPercentage());
        }
        if (!selectionName.Equals(""))
        {
            int topPos = buildAreaHeight + BUTTON_SPACING;
           // GUI.Label(new Rect(0, topPos, ORDERS_BAR_WIDTH, SELECTION_NAME_HEIGHT), selectionName);
        }
        GUI.EndGroup();
    }

    private void DrawResourceBar()
    {
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, Screen.height, Screen.width, Screen.height-RESOURCE_BAR_HEIGHT), "");
        GUI.EndGroup();
    } 
    /*private DrawActions()
    {
        GUIStyle buttons = new GUIStyle();
        buttons.hover.background = buttonHover;
        buttons.active.background = buttonClick;
        GUI.skin.button = buttons;
        int numActions = actions.Length;

        //define draw area
        GUI.BeginGroup(new Rect(0, 0, INFO_PANEL_WIDTH, buildAreaHeight));

        //draw scrollbar if needed
        if (numActions >= MaxNumRows(buildAreaHeight)) DrawSlider(buildAreaHeight, numActions / 2.0f);

        //display actions as buttons and handle button click

        for (int i = 0; i < numActions; i++)
        {
            int col = i % 2;
            int row = i / 2;
            Rect pos = GetButtonPos(row, col);
            //Texture2D action = ResourceManager.GetBuildImage(actions[i]);
        }
    }
    private int MaxNumRows(int areaHeight)
    {
        return areaHeight / BUILD_IMAGE_HEIGHT;
    }

    private Rect GetButtonPos(int row, int column)
    {
        int left = SCROLL_BAR_WIDTH + column * BUILD_IMAGE_WIDTH;
        float top = row * BUILD_IMAGE_HEIGHT - sliderValue * BUILD_IMAGE_HEIGHT;
        return new Rect(left, top, BUILD_IMAGE_WIDTH, BUILD_IMAGE_HEIGHT);
    }

    private void DrawSlider(int groupHeight, float numRows)
    {
        //slider goes from 0 to the number of rows that do not fit on screen
      //  sliderValue = GUI.VerticalSlider(GetScrollPos(groupHeight), sliderValue, 0.0f, numRows - MaxNumRows(groupHeight));
    }*/
    private Rect GetCursorDrawPosition()
    {
        //set base position for custom cursor image
        float leftPos = Input.mousePosition.x;
        float topPos = Screen.height - Input.mousePosition.y; //screen draw coordinates are inverted
                                                              //adjust position base on the type of cursor being shown
        if (activeCursorState == CursorState.PanRight) { leftPos = Screen.width - activeCursor.width; }
        else if (activeCursorState == CursorState.PanDown) { topPos = Screen.height - activeCursor.height; }
        else if (activeCursorState == CursorState.Move ||
            activeCursorState == CursorState.Select || activeCursorState == CursorState.Gather)
        {
            topPos -= activeCursor.height / 2;
            leftPos -= activeCursor.width / 2;
        }
        return new Rect(leftPos, topPos, activeCursor.width, activeCursor.height);
    }

    private void UpdateCursorAnimation()
    {
        //sequence animation for cursor (based on more than one image for the cursor)
        //change once per second, loops through array of images
        if (activeCursorState == CursorState.Move)
        {
            currentFrame = (int)Time.time % moveCursors.Length;
            activeCursor = moveCursors[currentFrame];
        }
        else if (activeCursorState == CursorState.Attack)
        {
            currentFrame = (int)Time.time % attackCursors.Length;
            activeCursor = attackCursors[currentFrame];
        }
        else if (activeCursorState == CursorState.Gather)
        {
            currentFrame = (int)Time.time % gatherCursors.Length;
            activeCursor = gatherCursors[currentFrame];
        }
    }
    public bool MouseInBounds()
    {
        //Screen coordinates start in the lower-left corner of the screen
        //not the top-left of the screen like the drawing coordinates do
        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - INFO_PANEL_WIDTH;
        bool insideHeight = mousePos.y >= RESOURCE_BAR_HEIGHT && mousePos.y <= Screen.height;
        return insideWidth && insideHeight;
    }

    public Rect GetPlayingArea()
    {
        return new Rect(0, 0, Screen.width - INFO_PANEL_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT);
    }

    public void SetCursorState(CursorState newState)
    {
        if (activeCursorState != newState) previousCursorState = activeCursorState;
        activeCursorState = newState;
        switch (newState)
        {
            case CursorState.Select:
                activeCursor = selectCursor;
                break;
            case CursorState.Attack:
                currentFrame = (int)Time.time % attackCursors.Length;
                activeCursor = attackCursors[currentFrame];
                break;
            case CursorState.Gather:
                currentFrame = (int)Time.time % gatherCursors.Length;
                activeCursor = gatherCursors[currentFrame];
                break;
            case CursorState.Move:
                currentFrame = (int)Time.time % moveCursors.Length;
                activeCursor = moveCursors[currentFrame];
                break;
            case CursorState.PanLeft:
                activeCursor = leftCursor;
                break;
            case CursorState.PanRight:
                activeCursor = rightCursor;
                break;
            case CursorState.PanUp:
                activeCursor = upCursor;
                break;
            case CursorState.PanDown:
                activeCursor = downCursor;
                break;
            default: break;
        }
    }
    public CursorState GetPreviousCursorState()
    {
        return previousCursorState;
    }
    public CursorState GetCursorState()
    {
        return activeCursorState;
    }
    public void SetResourceValues(Dictionary<ResourceType, int> resourceValues, Dictionary<ResourceType, int> resourceCapacities)
    {
        this.resourceValues = new Dictionary<ResourceType, int>();
        this.resourceCaps = new Dictionary<ResourceType, int>();
    }
}
