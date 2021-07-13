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
    public float distanceTravelled;

    public float xOffset, yOffset;
    float maxDistance = 0.5f;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    [SerializeField]
    float controllerSpeed = 115;

    void Start()
    {
        instance = this;
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    public void Awake()
    {

    }


    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 desiredPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            SwipeMouse(); 
            //SwipeTouch();
            

            transform.position = desiredPoint;
            xOffset = Mathf.Clamp(xOffset, -maxDistance, maxDistance);
            yOffset = Mathf.Clamp(yOffset, -maxDistance, maxDistance);
            desiredPoint = transform.TransformPoint(new Vector3(xOffset, yOffset, 0));

            transform.position = desiredPoint;
        }

    }

    public void SwipeMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0  && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && yOffset < 0.4f)
        {
                Debug.Log("up swipe");
                StartCoroutine(Fly("Up"));
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && yOffset > -0.4f)
        {
                Debug.Log("down swipe");
                StartCoroutine(Fly("Down"));
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.4f && currentSwipe.y < 0.4f && xOffset > -0.4f)
        {
                Debug.Log("left swipe");
                xOffset -= 0.4f;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.4f && currentSwipe.y < 0.4f && xOffset < 0.4f)
        {
                Debug.Log("right swipe");
                xOffset += 0.4f;
            }
        }

    }

    public float flytime = 0;
    private IEnumerator Fly(string wheretoFly)
    {
        float tempY = transform.position.y;
        switch (wheretoFly)
        {
            case "Up":
                flytime = 0;
                Debug.Log("tempY_up: " + tempY);
                yOffset = 0.5f;
                while (flytime < 2f)
                {
                    flytime += Time.deltaTime;
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(0.75f, tempY + yOffset, flytime / 2f), transform.position.z);
                    
                    yield return null;
                }
                break;

            case "Down":
                flytime = 0f;
                Debug.Log("tempY_down: " + tempY);
                yOffset = 0.5f;
                while (flytime < 2f)
                {
                    flytime += Time.deltaTime;
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(0.75f, tempY - yOffset, flytime / 2f), transform.position.z);
                    yield return null;
                }
                break;
            default:
                break;
        }
    }
    public void SwipeTouch()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && yOffset < 0.4f)
             {
                    Debug.Log("up swipe");
                    yOffset += 0.4f;
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && yOffset > -0.4f)
             {
                    Debug.Log("down swipe");
                    yOffset -= 0.4f;
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.4f && currentSwipe.y < 0.4f && xOffset > -0.4f)
             {
                    Debug.Log("left swipe");
                    xOffset -= 0.4f;
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.4f && currentSwipe.y < 0.4f && xOffset < 0.4f)
             {
                    Debug.Log("right swipe");
                    xOffset += 0.4f;
                }
            }
        }
    }



    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
