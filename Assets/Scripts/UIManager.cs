using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject[] healthBarList = new GameObject[4];
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void GotoGeneratePath()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void HealthBarController()
    {

        if (Player.instance.healthIconNumber != 0)
        {
            Player.instance.healthIconNumber--;
            Debug.Log("healtbar methodr");
            healthBarList[Player.instance.healthIconNumber].GetComponent<Image>().gameObject.SetActive(false);

        }
    }


}
