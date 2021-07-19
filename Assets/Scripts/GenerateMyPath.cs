using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateMyPath : MonoBehaviour
{
    public GameObject prefab, pipePrefab;
    public GameObject planePrefab;
    public bool closedLoop = false;
    public Transform[] waypoints = new Transform[8];

    public GameObject parent;
    public static GenerateMyPath instance;
    public BezierPath bezierPath;

    public int k = 0;
    public int factor = 1;
    private void Awake()
    {
        instance = this;
        for (int i = 1; i < 4; i++)
        {
            GameObject go = Instantiate(prefab, new Vector3(-0.28f, -0.28f, k), Quaternion.identity);
            k += 5;
            go.transform.SetParent(parent.transform);
            waypoints[i-1] = go.transform;
        }
        
    }
    float z = 17.531f;
    public Transform InstantiatePipe()
    {
        // Instantiate methodunun son girdisi Quaternion.identity'den pipePrefab.transform.rotation olarak düzeltildi. 
        GameObject pipe = Instantiate(pipePrefab, new Vector3(pipePrefab.transform.position.x, pipePrefab.transform.position.y, pipePrefab.transform.position.z + z), pipePrefab.transform.rotation);

        
        z += 17.531f;
        return pipe.transform;
    }
    void Start()
    {

        bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
        bezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
        bezierPath.GlobalNormalsAngle = 90;
        GetComponent<PathCreator>().bezierPath = bezierPath;
    }

    public Transform SpawnPlane()
    {
        GameObject plane = Instantiate(planePrefab, new Vector3(planePrefab.transform.position.x, planePrefab.transform.position.y, planePrefab.transform.position.z + z), Quaternion.Euler(-90.00f,0,0));
        plane.transform.SetParent(Player.instance.planeParent.transform);
        return plane.transform;
    }

   
}
