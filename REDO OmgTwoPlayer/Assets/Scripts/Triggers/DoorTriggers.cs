using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggers : MonoBehaviour
{

    //Please Stahr, attach to singular game objects that will have this and ref anothe gameobject
    public bool isOpening = false; //When true, the door will stay activated

    //referencing to the moving objects script:
    public DoorController doorController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            //Tell the other script that it can now move; Set bool to true
            isOpening = true;
            doorController.DoorOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Tell the other script to stop moving now. set bool to false
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            isOpening = false;
            doorController.DoorClose();
        }

    }

}
