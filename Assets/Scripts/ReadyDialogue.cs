using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyDialogue : MonoBehaviour
{
    public Set currentSet; //set that the player has selected to play (SetPlay)
    public Frame currentFrame; //current frame that the player has selected to play (QuickPlay)
    public GameObject music;
    public bool began;

    public Text shortName; //short name of set or frame that the player has selected
    public Text longName; //long name of set or frame
    public Text highScore; //high score of set or frame

    public SceneSwitch sceneSwitch;
    public SceneTransition1 sceneTransition;

    public TransitionText transitionText;
    public FadeOutMusic musicFade;

    private void Start()
    {
        sceneSwitch = GameObject.FindObjectOfType<SceneSwitch>();
        // transitionText = GameObject.FindObjectOfType<TransitionText>();
        musicFade = GameObject.FindObjectOfType<FadeOutMusic>();
    }

    public void Select(Set _set)
    {
        currentSet = _set;
        currentFrame = null;
        UpdateUI();
        PlayerPrefs.SetString("Load Next", "true"); //load frame's next frame
        Debug.Log("Set " + currentSet.shortName + " selected!");
    }
    public void Select(Frame _frame)
    {
        currentFrame = _frame;
        currentSet = null;
        UpdateUI();
        PlayerPrefs.SetString("Load Next", "false"); //don't load next frame
    }

    public void Deselect()
    {
        currentFrame = null;
        currentSet = null;
    }

    public void BeginGame()
    {
        if(currentFrame != null)
        {
            GameObject.Instantiate(music, Vector3.zero, Quaternion.identity);
            sceneSwitch.LoadSingleFrame(currentFrame.shortName);
        }
        if(currentSet != null)
        {
            GameObject.Instantiate(music, Vector3.zero, Quaternion.identity);
            sceneSwitch.LoadSingleFrame(currentSet.frames[0].shortName);
        }
    }

    private void UpdateUI()
    {
        if(currentSet != null)
        {
            shortName.text = currentSet.shortName;
            longName.text = currentSet.longName;
            // if(PlayerPrefs.GetInt(currentSet.shortName+"_score") != null)
            // int high = PlayerPrefs.GetInt(currentSet.shortName+"_score");
            highScore.text = "High Score " + PlayerPrefs.GetInt(currentSet.shortName+"_score").ToString();
        }
        if(currentFrame != null)
        {
            shortName.text = currentFrame.shortName;
            longName.text = currentFrame.longName;
            // highScore.text = "High Score " + PlayerPrefs.GetInt(currentFrame.shortName+"_score").ToString();
        }
        if(currentSet == null && currentFrame == null)
        {
            shortName.text = "SHORTNAME";
            longName.text = "LONGNAME";
            highScore.text = "High Score 000000";
        }
    }

    public void CallWaitToLoad()
    {
        if(!began)
        {
            began = true;
            PlayerPrefs.SetInt("Current Score", 0);
            PlayerPrefs.SetInt("Local Lives", 3);
            StartCoroutine(WaitToLoad());
        }
    }

    public IEnumerator WaitToLoad()
    {
        transitionText.EnterSet(currentSet.shortName, currentSet.highScore);
        musicFade.enabled = true;
        sceneTransition.StartAnimate("intro");
        yield return new WaitForSeconds(sceneTransition.delay + sceneTransition.totalTime + sceneTransition.wait);
        //perform following actions after wait
        BeginGame();
    }
}
