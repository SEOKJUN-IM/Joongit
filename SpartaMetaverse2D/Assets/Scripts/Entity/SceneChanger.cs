using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{   
    static SceneChanger sceneChanger;

    public static SceneChanger Instance { get { return sceneChanger; } }

    private void Awake()
    {
        sceneChanger = this;
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoGameLoading()
    {
        SceneManager.LoadScene(2);
    }

    public void GoMiniGame()
    {
        SceneManager.LoadScene(3);
    }
}
