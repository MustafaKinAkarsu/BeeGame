using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public static FlyController instance;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 1;
    float distanceTravelled;

    public float xOffset, yOffset;
    float maxDistance = 0.5f;

    [SerializeField]
    float controllerSpeed = 115;

    void Start()
    {
        instance = this;
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 desiredPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            /*if (Input.GetKeyDown("a"))
            {
                xOffset += h * controllerSpeed;
            }
            else if (Input.GetKeyDown("d"))
            {
                xOffset += h * controllerSpeed;
            }
            else if (Input.GetKeyDown("w"))
            {
                yOffset += v  * controllerSpeed;
            }
            else if (Input.GetKeyDown("s"))
            {
                yOffset  += v * controllerSpeed;
            }*/

            if (Input.GetKeyDown(KeyCode.UpArrow) && yOffset < 0.4f)
            {
                yOffset += 0.4f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && yOffset > -0.4f)
            {
                yOffset -= 0.4f;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && xOffset < 0.4f)
            {
                xOffset += 0.4f;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && xOffset > -0.4f)
            {
                xOffset -= 0.4f;
            }

            transform.position = desiredPoint;
            xOffset = Mathf.Clamp(xOffset, -maxDistance, maxDistance);
            yOffset = Mathf.Clamp(yOffset, -maxDistance, maxDistance);
            desiredPoint = transform.TransformPoint(new Vector3(xOffset, yOffset, 0));

            transform.position = desiredPoint;
            
        }
    }




    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
