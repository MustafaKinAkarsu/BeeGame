using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGlobal : MonoBehaviour
{

    public GameObject baseCube;
    // Start is called before the first frame update

    GameObject SpawnRight(GameObject origin ,float distance)
    {
        Vector3 reference = origin.transform.position;      
        GameObject go = Instantiate(baseCube, new Vector3(reference.x + distance, reference.y, reference.z), Quaternion.identity);
        go.transform.position = getRelativePosition(origin.transform, reference);
        return go;
    }

    GameObject SpawnUp(GameObject origin, float distance)
    {
        Vector3 reference = origin.transform.position;
        GameObject go = Instantiate(baseCube, new Vector3(reference.x, reference.y, reference.z + distance), Quaternion.identity);
        go.transform.position = getRelativePosition(origin.transform, reference);
        return go;
    }
    public static Vector3 getRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
        Debug.Log(relativePosition);
        return relativePosition;
    }

    void Start()
    {
        GameObject cube1 = Instantiate(baseCube, new Vector3(0,6,0), Quaternion.identity);
        GameObject cube2 = Instantiate(baseCube, new Vector3(10, 6, 0), Quaternion.Euler(0,60,0));
        Vector3 relativepos = transform.InverseTransformVector(cube2.transform.position);
        GameObject cube3 = Instantiate(baseCube, relativepos, Quaternion.identity);
    }

}
