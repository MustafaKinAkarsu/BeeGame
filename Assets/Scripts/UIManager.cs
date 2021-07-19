using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void GotoGeneratePath()
    {
         SceneManager.LoadScene("Generate Path");
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
}
