using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeScene : MonoBehaviour
{
    public string startScene;
    public string levelToLoad;
    public float Timelife;
    public bool Start;


void Awake()
{        
    if(!Start)
    {
    StartCoroutine(FinishVideo());
    }
}


// Metodo per cambiare scena
public void changeScene()
{
    SceneManager.LoadScene(startScene, LoadSceneMode.Single);
    SceneManager.sceneLoaded += OnSceneLoaded;
}

// Metodo eseguito quando la scena è stata caricata
private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    //GameplayManager.instance.FadeIn();

    SceneManager.sceneLoaded -= OnSceneLoaded;
 
}

    IEnumerator FinishVideo()
    {
        yield return new WaitForSeconds(Timelife);
        SceneManager.LoadScene(startScene, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.LoadScene(startScene);
    }
}
