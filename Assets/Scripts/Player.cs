using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public bool collisionFlag = false;
    public GameObject pathObject;
    public int k;
    public int counter;
    public GameObject pipeParent, planeParent;
    public GameObject CamFollowObj;
    public int Health;
    public Text Distancetxt;
    public GameObject GameoverCanv;
    //public GameObject follower;
    private int i;

    void Start()
    {

        i = 0;
        counter = 0;
        instance = this;
        k = 20;
        /*
        for(int i = 0; i < 8; i++)//başlangıcta oluşturulan i kadar pipe
        {
            Transform transformTemp = GenerateMyPath.instance.InstantiatePipe();//başlangıçtaki oluşturulan 2 . pipe
            transformTemp.SetParent(pipeParent.transform);
        }
        */
        //GameObject goPlane = Instantiate(GenerateMyPath.instance.planePrefab, new Vector3(0.3f, 0.7f, 140f), Quaternion.Euler(-90.00f, 0, 0));
        //goPlane.transform.SetParent(planeParent.transform);

    }

    /* Belirli aralıklarla borularda engel oluşmaması durumunu engellemek için Update içerisinde bulunan
     * yapı eklenmiştir. Bu yapı, arının arkasında bıraktığı borulardaki engelleri temizlemektedir.
     * Ek olarak ObsDeactivator() methodunda for döngüsü üst sınırı 13 olarak güncellenmiştir. 
     * Sebebi 12 indexli bir engel olmasıdır. 
     */

    void Update()
    {
        for (int i = 0; i < 11; i++)
        {
            if (pipeParent.transform.GetChild(i).position.z < this.transform.position.z - 17.531f)
                ObstacleGenerator.instance.ObsDeactivator(pipeParent.transform.GetChild(i).gameObject);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Çarpışma gerçekleşti");
        if (collision.gameObject.tag == "Prefab")
        {
            ++counter;
            Vector3 temp = new Vector3(-0.28f, -0.28f, k);
            k += 5;
            GenerateMyPath.instance.waypoints[0].position = GenerateMyPath.instance.waypoints[1].position;
            GenerateMyPath.instance.waypoints[1].position = GenerateMyPath.instance.waypoints[2].position;
            GenerateMyPath.instance.waypoints[2].position = temp;
            GenerateMyPath.instance.bezierPath.AddSegmentToEnd(temp);
        }

        if (collision.gameObject.tag == "SpawnTrigger")//borudaki triger alanından geçtikten sonra boru oluşturma tetiklemesi
        {
            if (i >= 11)
            {
                i = 0;
            }
            Debug.Log("Spawna girildi");
            //GenerateMyPath.instance.SpawnPlane();
            //ObstacleGenerator.instance.Generator();
            TransportPipe(i);
            TransportPlane();
            Debug.Log("Index SPAWN: " + i);
            i++;

        }
        if (collision.gameObject.tag == "Stray_Voltage_Obs")
        {
            //Debug.Log("ÇARPILDINNNNNNNNNNNNNNNNNNNN");
            ObstacleGenerator.instance._timerCR = ObstacleGenerator.instance.StartTimer(3f);
            StartCoroutine(ObstacleGenerator.instance._timerCR);
            //Health -= 14;
        }
        if (collision.gameObject.tag == "Spray_Obs")
        {
            Debug.Log("SPRAYLENDİN");
            ObstacleGenerator.instance._timerCR = ObstacleGenerator.instance.SprayTimer(3f);
            StartCoroutine(ObstacleGenerator.instance._timerCR);
           // Health -= 7;
        }
        if (Health <= 0)
        {
            Debug.Log("YOU JUST DIED");
            Time.timeScale = 0;
            DistanceDisplay();
            GameoverCanv.SetActive(true);


        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {

        }
    }
    */
    void TransportPipe(int index)
    {
        Debug.Log("Index TRANSPORT PIPE: " + index);
        ObstacleGenerator.instance.Generator(pipeParent.transform.GetChild(index).gameObject, index);
        pipeParent.transform.GetChild(index).transform.position = new Vector3(pipeParent.transform.GetChild(index).transform.position.x, pipeParent.transform.GetChild(index).transform.position.y, pipeParent.transform.GetChild(index).transform.position.z + 192.84148f);
        /*
        if(index != 0)
        {
            ObstacleGenerator.instance.ObsDeactivator(pipeParent.transform.GetChild(index - 1).gameObject);
        }
        else
        {
            ObstacleGenerator.instance.ObsDeactivator(pipeParent.transform.GetChild(10).gameObject);
        }
        */


    }
    void TransportPlane()
    {
        planeParent.transform.GetChild(0).transform.position = new Vector3(planeParent.transform.GetChild(0).transform.position.x, planeParent.transform.GetChild(0).transform.position.y, planeParent.transform.GetChild(0).transform.position.z + 17.531f);
        //Destroy(planeParent.transform.GetChild(0).gameObject);
    }
    public void DistanceDisplay()
    {
        Debug.Log(FlyController.instance.distanceTravelled.ToString());
        Distancetxt.text = "DISTANCE TRAVELLED :" + FlyController.instance.distanceTravelled.ToString() + "m";
    }

}
