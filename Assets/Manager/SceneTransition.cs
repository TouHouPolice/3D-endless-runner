using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {


    public GameObject objectToHide;
    public GameObject objectToHide2;

    public void LoadGameLevel()
    {
        
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
    public void UnloadGameLevel()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
    }

    public void UnloadMenuLevel()
    {
        SceneManager.UnloadSceneAsync(0);
    }

    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Hide()
    {
        objectToHide.SetActive(false);
    }

    public void Hide2()
    {
        objectToHide2.SetActive(false);
    }
    
}
