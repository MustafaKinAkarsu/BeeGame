using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab, pipe;
    public static ObstacleGenerator instance;

    public IEnumerator _timerCR;

    public float startSpeed;
    void Start()
    {
        instance =  this ;
    }


    public GameObject Generator()
    {
        GameObject go = Instantiate(obstaclePrefab, pipe.transform, false);
        return go;
    }

    public IEnumerator StartTimer(float timeRemaining)
    {
        startSpeed = FlyController.instance.speed;
        for (float i = timeRemaining; i  > 0; i-= 0.1f)
        {
            FlyController.instance.speed += 0.1f;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        float deltaSpeed = FlyController.instance.speed - startSpeed;
        deltaSpeed = Mathf.Round(deltaSpeed);
        Debug.Log("DeltaSpeed = " + deltaSpeed);
        _timerCR = StopTimer(deltaSpeed);
        StartCoroutine(_timerCR);
    }
    
    public IEnumerator StopTimer(float deltaSpeed, float timeRemaining = 1)
    {
        FlyController.instance.speed = Mathf.Round(FlyController.instance.speed);
        Debug.Log("Speed after acceleration =" + FlyController.instance.speed);
        for (float i = timeRemaining; i > 0.1; i -= 0.2f)
        {
            Debug.Log("i = " + i);
            FlyController.instance.speed -= deltaSpeed / 5;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed -= deltaSpeed / 5;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
