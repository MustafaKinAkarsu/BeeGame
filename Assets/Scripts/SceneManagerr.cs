using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public void GotoGeneratePath()
    {
         SceneManager.LoadScene("Generate Path");
    }

    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
