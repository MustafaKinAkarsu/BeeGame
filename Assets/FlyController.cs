using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    float xOffset, yOffset;
    float maxDistance = 3;

    [SerializeField]
    float controllerSpeed = 5;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 desiredPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            /*if (h == 0 && v == 0)
            {
                xOffset = Mathf.MoveTowards(xOffset, 0, Time.deltaTime * controllerSpeed);
                yOffset = Mathf.MoveTowards(yOffset, 0, Time.deltaTime * controllerSpeed);
            }
            else
            {*/
                xOffset += h * Time.deltaTime * controllerSpeed;
                yOffset += v * Time.deltaTime * controllerSpeed;
            //}

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
