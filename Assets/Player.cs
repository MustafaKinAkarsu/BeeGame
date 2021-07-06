using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public bool collisionFlag = true;
    public GameObject pathObject;
    public int k;
    public int counter;
    void Start()
    {
        counter = 0;
       instance = this;
        k = 1;
    }
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Çarpışma gerçekleşti");
        if (collision.gameObject.tag == "Prefab")
        {
            ++counter;
            Vector3 temp = new Vector3(8.819591f, 9.361911f+ k, 5.182884f);
            k += 5;
            GenerateMyPath.instance.waypoints[0].position = GenerateMyPath.instance.waypoints[1].position;
            GenerateMyPath.instance.waypoints[1].position = GenerateMyPath.instance.waypoints[2].position;
            GenerateMyPath.instance.waypoints[2].position = temp;
            if (counter==1)
                ModifyAnchorsForUpwards(GenerateMyPath.instance.bezierPath.GetPoint(3), 6);
            if (counter==2)
                ModifyAnchorsForUpwards(GenerateMyPath.instance.bezierPath.GetPoint(6), 9);
            GenerateMyPath.instance.bezierPath.AddSegmentToEnd(temp);
        }
    }

    void ModifyAnchorsForUpwards(Vector3 pointpos, int pointindex)
    {
        pointpos = transform.TransformPoint(pointpos);
        GenerateMyPath.instance.bezierPath.SetPoint(pointindex, new Vector3(pointpos.x+3.819591f, pointpos.y+ 5.361911f, pointpos.z+ 2.182884f));
        GenerateMyPath.instance.bezierPath.SetPoint(pointindex-1, new Vector3(11.24933f, -0.627973f, 6.791041f));
        
    }
    IEnumerator Wait()
    {
        //To wait, type this:

        //Stuff before waiting
        yield return new WaitForSeconds(3);
        //Stuff after waiting.
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
