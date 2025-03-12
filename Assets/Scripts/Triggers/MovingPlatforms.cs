
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    //lets code the movement, I do not want to play with the animator right now.
    public Vector3 moveDirection = Vector3.up; //The platform will move up
    //how far it will move up
    public float moveDistance = 4f;
    //how fast it will move
    public float platSpeed = 2.5f;

    //now need to ensure it GOES BACK down. Im so tayad omg
    private Vector3 startPosition;
    private bool isMoving = false;



    void Start()
    {
        //The position the platform is in, 
        startPosition = transform.position;
    }
    //ACTUALLY NEED CODE HERE THAT WILL SAY WHEN THE PLAYERS ARE IN THE TRIGGERZONE,
    //tHEN THE PLATFORM SHOULD START MOVING. WILL MODIFY LATER...TOO TAYAD RN

    
    void Update()
    {
        if (isMoving)
        {
            float movement = Mathf.PingPong(Time.time  *platSpeed, moveDistance);
            transform.position = startPosition + (moveDirection * movement);
        }
    }

    public void ActivatePlatform()
    {
        isMoving = true;
    }

    public void DeactivatePlatform()
    {
        isMoving = false;
    }
}
