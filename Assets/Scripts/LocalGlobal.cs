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
        reference = origin.transform.InverseTransformVector(reference);
        GameObject go = Instantiate(baseCube, new Vector3(reference.x + distance, reference.y, reference.z), Quaternion.identity);
        return go;
    }

    GameObject SpawnUp(GameObject origin, float distance)
    {
        Vector3 reference = origin.transform.position;
        reference = origin.transform.localPosition;

        GameObject go = Instantiate(baseCube, new Vector3(reference.x, reference.y, reference.z + distance), Quaternion.identity);
        return go;
    }

   
    void Start()
    {
        GameObject cube1 = Instantiate(baseCube, new Vector3(0,6,0), Quaternion.identity);
        cube1 = SpawnRight(cube1, 7);
        cube1.transform.Rotate(0f, 60.0f, 0.0f, Space.Self);
        cube1 = SpawnUp(cube1, 7);
        cube1 = SpawnRight(cube1, 10);
    }

}
