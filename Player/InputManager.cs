using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class InputManager : MonoBehaviour
{
    public GameObject selectedObject;
    public Texture boxTexture;
    

    private Vector2 boxStart;
    private Vector2 boxEnd;
    //private ObjectInfo selectedInfo;

    private Rect selectionBox;

    private Player player;
    private GameObject[] units;
    // Start is called before the first frame update
    
    
    void Start()
    {
        player = transform.root.GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (player && player.isHuman) 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Camera.main.transform.rotation = rotation;
        //}
        //if (Input.GetMouseButton(0) && boxStart == Vector2.zero)
        //{
        //    boxStart = Input.mousePosition;
        //}
        //else if(Input.GetMouseButton(0) && boxStart != Vector2.zero)
        //{
        //    boxEnd = Input.mousePosition;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    units = GameObject.FindGameObjectsWithTag("Selectable");
        //    MultiSelect();;
        //}

        //RotateCamera();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    LeftClick();
        //}
        //selectionBox = new Rect(boxStart.x, Screen.height - boxStart.y, boxEnd.x - boxStart.x,
        //        -1 * ((Screen.height - boxStart.y) - (Screen.height - boxEnd.y)));
    }

   



    public static float PanSpeed {  get { return 25; } }
    public static float RotateSpeed { get { return 100; } }
    public static int PanDetect { get { return 15; } }

    public static float RotateAmount { get { return 10; } }

    public static float MinCameraHeight { get { return 10; } }

    public static float MaxCameraHeight { get { return 40; } }
    /*
    public void MultiSelect()
    {
        foreach (GameObject unit in units)
        {
            if (unit.GetComponent<ObjectInfo>().isUnit)
            {
                Vector2 unitPos = Camera.main.WorldToScreenPoint(unit.transform.position);

                if (selectionBox.Contains(unitPos, true))
                {
                    unit.GetComponent<ObjectInfo>().isSelected = true;
                }
            }
        }

        boxStart = Vector2.zero;
        boxEnd = Vector2.zero;
    }*/
    private void LeftClick()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (player.hud.MouseInBounds())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();
           
            if (hitObject && hitPoint != ResourceManager.InvalidPosition)
            {
                selectedObject = hitObject;
                if (player.SelectedObject)
                {                 
                    player.SelectedObject.MouseClick(hitObject, hitPoint, player);
                } else if (!hitObject.CompareTag("Ground"))
                {
                    WorldObject worldObject = hitObject.transform.parent.GetComponentInChildren<WorldObject>();
                    if (worldObject)
                    {
                        Debug.Log(worldObject.objectName);
                        player.SelectedObject = worldObject;
                        worldObject.SetSelection(true, player.hud.GetPlayingArea());
                    }
                }
            }

            /*if (hitObject.GetComponent<Collider>().CompareTag("Selectable"))
            {
                //    selectedObject = hit.collider.gameObject;
                //    selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                //selectedObject = hitObject.GetComponent<GameObject>();
                
               Debug.Log("Selected "+selectedObject.name);
            }*/

            /*if (player.SelectedObject.GetComponent<Unit>())
            {
                player.GetComponent<GUIManager>().UnitPanel.gameObject.SetActive(false);
            }
            else if (player.SelectedObject.GetComponent<Building>())
            {
                player.GetComponent<GUIManager>().BuildingPanel.gameObject.SetActive(false);
            }*/
            //Deselect object
            if (!Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject && hitObject.CompareTag("Ground"))
            {
                player.GetComponent<GUIManager>().BuildingPanel.gameObject.SetActive(false);
                player.GetComponent<GUIManager>().UnitPanel.gameObject.SetActive(false);
                player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
                player.SelectedObject = null;
            }    
        }
    }

    //TO DO: CHANGE DESELECT TO LEFT CLICK ON GROUND AND SET RIGHT CLICK TO MOVE
    private void RightClick()
    {
        if (player.hud.MouseInBounds())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();

            if (hitObject && hitPoint != ResourceManager.InvalidPosition)
            {
                if (hitObject.CompareTag("Ground")) player.SelectedObject.RightMouseClick(hitObject, hitPoint, player);
                /*else if (hitObject.GetComponent<Player>().isHuman == false)
                {
                //Logic for if the target is an enemy object
                }*/
            }
            /*
            if (player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject)
            {
                player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
                player.SelectedObject = null;



                GameObject hitObject = FindHitObject();
                Vector3 hitPoint = FindHitPoint();

                if (hitObject && hitPoint != ResourceManager.InvalidPosition)
                {
                    selectedObject = hitObject;
                    if (player.SelectedObject)
                    {
                        player.SelectedObject.MouseClick(hitObject, hitPoint, player);
                    }
                    else if (!hitObject.CompareTag("Ground"))
                    {
                        WorldObject worldObject = hitObject.transform.parent.GetComponentInChildren<WorldObject>();
                        if (worldObject)
                        {
                            Debug.Log(worldObject.objectName);
                            worldObject.SetSelection(true, player.hud.GetPlayingArea());
                            player.SelectedObject = worldObject;
                        }
                    }
                }
            }*/
        }
    }

    private void MoveCamera()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        bool mouseScroll = false;
        //horizontal camera  movement
        if (xpos >= 0 && xpos < PanDetect)
        {
            movement.x -= PanSpeed;
            player.hud.SetCursorState(CursorState.PanLeft);
            mouseScroll = true;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - PanDetect)
        {
            movement.x += PanSpeed;
            player.hud.SetCursorState(CursorState.PanRight);
            mouseScroll = true;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < PanDetect)
        {
            movement.z -= PanSpeed;
            player.hud.SetCursorState(CursorState.PanDown);
            mouseScroll = true;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - PanDetect)
        {
            movement.z += PanSpeed;
            player.hud.SetCursorState(CursorState.PanUp);
            mouseScroll = true;
        }

        //Ensure camera movement is in the direction the camera is facing

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0; //ignore vertical tilt to get sensible scrolling


        //movement away from ground
        movement.y -= PanSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;

        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > MaxCameraHeight)
        {
            destination.y = MaxCameraHeight;
        }
        if (destination.y < MinCameraHeight)
        {
            destination.y = MinCameraHeight;
        }

        //if the position is changed, perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * PanSpeed);
        }

        if (!mouseScroll)
        {
            player.hud.SetCursorState(CursorState.Select);
        }
    }
    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * RotateAmount;
        }

        //if a change in this the rotation is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * RotateSpeed);
        }
    }

    private void MouseActivity()
    {
        if (Input.GetMouseButtonDown(0)) { LeftClick(); }
        else if (Input.GetMouseButtonDown(1)) { RightClick(); }
        MouseHover();
    }

    private void MouseHover()
    {
        if (player.hud.MouseInBounds())
        {
            GameObject hoverObject = FindHitObject();
            if (hoverObject)
            {
                if (player.SelectedObject) player.SelectedObject.SetHoverState(hoverObject);
                else if (!hoverObject.CompareTag("Ground"))
                {
                    Player owner = hoverObject.transform.root.GetComponent<Player>();
                    if (owner)
                    {
                        Unit unit = hoverObject.transform.parent.GetComponent<Unit>();
                        Building building = hoverObject.transform.parent.GetComponent<Building>();
                        if (owner.username == player.username && (unit || building)) player.hud.SetCursorState(CursorState.Select);
                    }
                }
            }
        }
    }
    private GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) return hit.collider.gameObject;
        return null;
    }
    private Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) return hit.point;
        return ResourceManager.InvalidPosition;
    }
    private void OnGUI()
    {

        if (boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectionBox, boxTexture);
        }
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0.0f;
        GetComponentInChildren<PauseMenu>().enabled = true;
        GetComponent<InputManager>().enabled = false;
        Cursor.visible = true;
        ResourceManager.MenuOpen = true;
    }
}
