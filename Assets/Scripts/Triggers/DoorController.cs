using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 openSlightly = new Vector3(0, 5, 0);
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;



    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openSlightly;
    }

    
    void Update()
    {
        if (isOpen)
        {
            //If target position is true then openPosition if not closedPosition
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
        }
       
    }

    public void DoorOpen()
    {
        isOpen = true;
    }

    public void DoorClose()
    {
        isOpen = false;
    }
}
