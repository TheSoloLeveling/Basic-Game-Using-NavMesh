using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer; //

    public Texture2D pointer; // normal mouse
    public Texture2D target; // target mouse
    public Texture2D doorway; // dourway mouse

    public EventVector3 OnClickEnvironment; // event to which we can connect the NavMeshAgent

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }

            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnClickEnvironment.Invoke(hit.point);  // the hitpoint from the raycast 
            }
        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }

    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }  // creating a custom class to send Vector3 informations through an event
