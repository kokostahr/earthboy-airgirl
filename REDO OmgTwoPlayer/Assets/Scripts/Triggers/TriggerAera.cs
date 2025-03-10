using UnityEngine;

public class TriggerAera : MonoBehaviour
{
    //Need code that will detect when both players are in the relevant trigger areas
    //Using bools to perform checks
    private bool playe1Inside = false;
    private bool playe2Inside = false;

    //Going to assign the object that will activate, whether its a platform or door...will see
    public GameObject objectToActivate;

    //LET'S START USING TRIGGERS! 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) //why is it highlighted red bruh
        {
            //if player 1 enters, then set the bool to true.
            playe1Inside = true;    
        }

        //do the same for player two
        if (other.CompareTag("Player2"))
        {
            playe2Inside = true;
        }
        ActivateObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) //why is it highlighted red bruh
        {
            //if player 1 enters, then set the bool to true.
            playe1Inside = false;
        }

        //do the same for player two
        if (other.CompareTag("Player2"))
        {
            playe2Inside = false;
        }
        ActivateObject();
    }

    void ActivateObject()
    {
        //method that will determine if both players are inside the trigger, and react accordingly
        if (playe1Inside && playe2Inside) //if both are inside, then
        {
            //activate the platform or the door. Need better code? will i move these things in code or...with the animator. we shall see lol
            objectToActivate.SetActive(true);
        }
        else //IF BOTH aren't inside, then the object will NOOT appear for now
        {
            objectToActivate.SetActive(false);
        }
    }
}
