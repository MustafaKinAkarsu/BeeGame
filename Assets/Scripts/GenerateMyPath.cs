using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateMyPath : MonoBehaviour
{
    public GameObject prefab;
    public bool closedLoop = false;
    public Transform[] waypoints = new Transform[8];

    public GameObject parent;
    public static GenerateMyPath instance;
    public BezierPath bezierPath;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(prefab, new Vector3(5 * i, 0, 3 * i), Quaternion.identity);
            go.transform.SetParent(parent.transform);
            waypoints[i] = go.transform;
        }

            
        
    }
    void Start()
    {

        bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
        bezierPath.ControlPointMode = BezierPath.ControlMode.Free;
        GetComponent<PathCreator>().bezierPath = bezierPath;
    }

    private void Update()
    {
        
    }
}
