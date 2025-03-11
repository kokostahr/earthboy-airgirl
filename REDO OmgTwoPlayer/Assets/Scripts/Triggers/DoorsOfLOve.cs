using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOfLOve : MonoBehaviour
{
    public Vector3 openWide = new Vector3(0, 5, 0);
    public float openingSpeed = 5f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;



    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + openWide;
    }


    void Update()
    {
        if (isOpen)
        {
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openingSpeed);
        }
    }

    public void LoveDoorOpen()
    {
        isOpen = true;
    }

    public void LoveDoorClose()
    {
        isOpen = false;
    }
}
