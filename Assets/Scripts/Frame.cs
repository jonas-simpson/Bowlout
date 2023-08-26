using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frame : MonoBehaviour
{
    public string shortName, longName; //number name and long name
    public int currentScore, highScore;
    public bool unlocked;

    public Frame nextFrame;
    public CountScore score;
    public SceneSwitch sceneSwitch;
    // public int numLives;
    
    private void Start()
    {
        score = GameObject.FindObjectOfType<CountScore>();
        sceneSwitch = GameObject.FindObjectOfType<SceneSwitch>();
        LoadFrameData();
    }

    public void SaveFrameData()
    {
        //call this when the player finishes a level
        currentScore = score.currentScore;
        // SaveSystem.SaveFrame(this);
        PlayerPrefs.SetInt(shortName+"_score", currentScore);
        PlayerPrefs.SetInt(shortName+"_unlocked", 1);
    }

    public void LoadFrameData()
    {
        //call this on scene enter
        // FrameData data = SaveSystem.LoadFrame(this);
        // if(data != null)
        // {
        //     Debug.Log("Score exists");
        //     highScore = data.highScore;
        //     unlocked = true;
        // }
        // else
        // {
        //     Debug.Log("Score does not exist");
        //     unlocked = false;
        //     highScore = 0;
        // }
        highScore = PlayerPrefs.GetInt(shortName+"_score");
        // unlocked = PlayerPrefs.GetInt(shortName+"_unlocked");
    }

    public void NextFrame()
    {
        if(nextFrame != null)
            sceneSwitch.LoadSingleFrame(nextFrame.shortName);
        else
        {
            //last frame in set, go to victory screen
            sceneSwitch.LoadVictory();
        }
    }
    public void RestartFrame()
    {
        //restart the same frame from the beginning
        sceneSwitch.LoadSingleFrame(shortName);
    }

    public void MenuScore()
    {
        //load numbers to be visible on menu
    }
}