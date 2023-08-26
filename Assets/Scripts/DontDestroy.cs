using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu - GameOver" || scene.name == "Menu - Victory" || scene.name == "Menu - Credits")
        {
            Destroy(this.gameObject);
        }
    }
    // {
        
    //     GameObject[] objs = GameObject.FindGameObjectsWithTag("Scene Transition");
    //     if(objs.Length > 1)
    //     {
    //         Destroy(this);
    //     }

    //     DontDestroyOnLoad(this.gameObject);
    // }
}
