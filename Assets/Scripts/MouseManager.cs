using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
   // public Animator doorAnim;

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
                if ( door == true)
                {
                    Transform doorway = hit.collider.gameObject.transform;  // access to the transform of the doorway if its hit
                    OnClickEnvironment.Invoke(doorway.position + doorway.forward * 5); // invoke a forward vector through the doorway position
                   // doorAnim.SetBool("isDoorClickedOn", true);

                    //StartCoroutine("CloseDoor");
                }

                else
                {
                    OnClickEnvironment.Invoke(hit.point);  // invoke  the hitpoint from the raycast 
                }
               
            }
        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }

    }

   /* private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(3);
        doorAnim.SetBool("isDoorClickedOn", false);

    }*/
}


[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }  // creating a custom class to send Vector3 informations through an event
