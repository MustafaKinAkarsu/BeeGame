using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject pipe, sprayobj;
    public GameObject[] obstaclePrefab = new GameObject[4];

    public static ObstacleGenerator instance;

    public IEnumerator _timerCR;

    public float startSpeed;
    public int accelerationCount = 0;
    void Start()
    {
        instance =  this ;
    }
    

    public GameObject Generator()
    {
        GameObject go = Instantiate(obstaclePrefab[Random.Range(0,6)], pipe.transform, false);
        //GameObject go = Instantiate(sprayobj, pipe.transform, false);
        return go;
        
    }

    public IEnumerator StartTimer(float timeRemaining)
    {
        ++accelerationCount;
        if (accelerationCount == 1)
        {
            startSpeed = FlyController.instance.speed;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed = startSpeed;
        }

        for (float i = timeRemaining; i  > 0; i-= 0.1f)
        {
            FlyController.instance.speed += 0.1f;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        FlyController.instance.speed = startSpeed;
        CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed = startSpeed;
        /*float deltaSpeed = FlyController.instance.speed - startSpeed;
        deltaSpeed = Mathf.Round(deltaSpeed);
        Debug.Log("DeltaSpeed = " + deltaSpeed);
        _timerCR = StopTimer(deltaSpeed);
        StartCoroutine(_timerCR);*/
    }
    public IEnumerator SprayTimer(float timeRemaining)
    {
        ++accelerationCount;
        if (accelerationCount == 1)
        {
            startSpeed = FlyController.instance.speed;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed = startSpeed;
        }
        for (float i = timeRemaining; i > 0; i -= 0.1f)
        {
            FlyController.instance.speed -= 0.1f;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        FlyController.instance.speed = startSpeed;
        CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed = startSpeed;
    }
    /*public IEnumerator StopTimer(float deltaSpeed, float timeRemaining = 1)
    {
        FlyController.instance.speed = Mathf.Round(FlyController.instance.speed);
        CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed = Mathf.Round(CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed);
        Debug.Log("Speed after acceleration =" + FlyController.instance.speed);
        for (float i = timeRemaining; i > 0.1; i -= 0.2f)
        {
            Debug.Log("i = " + i);
            FlyController.instance.speed -= deltaSpeed / 5;
            CameraController.instance.emptyFollower.GetComponent<PathFollower>().speed -= deltaSpeed / 5;
            yield return new WaitForSeconds(0.2f);
        }
    }*/

}
