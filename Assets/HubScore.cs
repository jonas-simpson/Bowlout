using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubScore : MonoBehaviour
{
    public UnlockStage[] stages;
    public int totalScore;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<Text>(); // get my text
        
        stages = GameObject.FindObjectsOfType<UnlockStage>(); //get all frames in the scene
        foreach(UnlockStage stage in stages)
        {
            totalScore += PlayerPrefs.GetInt(stage.myFrame.shortName+"_score", 0); //add all scores to the total score
        }

        myText.text = totalScore.ToString(); //assign to text
    }

    public void ResetProgress()
    {
        foreach(UnlockStage stage in stages)
        {
            PlayerPrefs.SetInt(stage.myFrame.shortName+"_complete", 0);
            PlayerPrefs.SetInt(stage.myFrame.shortName+"_score", 0);
        }
        SceneSwitch sceneSwitch = GameObject.FindObjectOfType<SceneSwitch>();
        sceneSwitch.LoadHub();
    }
}
