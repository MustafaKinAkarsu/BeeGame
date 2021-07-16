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
    public GameObject pipeParent,planeParent;
    public GameObject CamFollowObj;
    public int Health;
    public Text Distancetxt;
    public GameObject GameoverCanv;


    void Start()
    {
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
        GameObject goPlane = Instantiate(GenerateMyPath.instance.planePrefab, new Vector3(0.3f, 0.7f, 140f), Quaternion.Euler(-90.00f, 0, 0));
        goPlane.transform.SetParent(planeParent.transform);
        
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
           
            Transform transformTemp = GenerateMyPath.instance.InstantiatePipe();
            GenerateMyPath.instance.SpawnPlane();
            transformTemp.SetParent(pipeParent.transform);
            ObstacleGenerator.instance.pipe = transformTemp.gameObject;
            ObstacleGenerator.instance.Generator();
        }
        if (collision.gameObject.tag == "Stray_Voltage_Obs")
        {
            //Debug.Log("ÇARPILDINNNNNNNNNNNNNNNNNNNN");
            ObstacleGenerator.instance._timerCR = ObstacleGenerator.instance.StartTimer(3f);
            StartCoroutine(ObstacleGenerator.instance._timerCR);
            Health -= 4;
        }
        if (collision.gameObject.tag == "Spray_Obs")
        {
            Debug.Log("SPRAYLENDİN");
            ObstacleGenerator.instance._timerCR = ObstacleGenerator.instance.SprayTimer(3f);
            StartCoroutine(ObstacleGenerator.instance._timerCR);
            Health -= 7;
        }
        if (Health <= 0)
        {
            Debug.Log("YOU JUST DIED");
            Time.timeScale = 0;
            DistanceDisplay();
            GameoverCanv.SetActive(true);

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            DeletePipe();
            DeletePlane();
        }
    }

    void DeletePipe()
    {
        Destroy(pipeParent.transform.GetChild(0).gameObject);
    }
    void DeletePlane()
    {
        Destroy(planeParent.transform.GetChild(0).gameObject);
    }
    public void DistanceDisplay()
    {
        Distancetxt.text = "DISTANCE TRAVELLED :" + FlyController.instance.distanceTravelled.ToString() + "m";
    }
  
}
