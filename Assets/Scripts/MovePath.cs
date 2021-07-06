using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour
{
    public Transform path;
    public float amount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            path.position += Vector3.up*amount;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            path.position += Vector3.down * amount;
        }

    }
}
