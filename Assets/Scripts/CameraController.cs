using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    private Vector3 offset;
    public GameObject virtualCamera;
    public GameObject emptyFollower, beeFollower;


    void Start()
    {
        instance = this;
        offset = transform.position - target.position;    
    }

    void SwitchCameraAngles()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = emptyFollower.transform;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = beeFollower.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = newPosition;
        //transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
        SwitchCameraAngles();
    }
}
