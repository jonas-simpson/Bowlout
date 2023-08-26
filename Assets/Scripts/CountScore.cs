using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountScore : MonoBehaviour
{
    public int currentScore, targetScore, totalScore;
    public int timeValue, pinValue, pinBonus, numDigits;
    public float speed, ease;
    public bool finished;

    public int pinPoints, timePoints;

    public Timer timer;     //to access current & start times
    public PinManager pins; //for pins down count

    public Text scoreText;

    public AudioSource scoreAudio;
    public AudioClip scoreSound;
    public float pitch, pitchScale, soundDelay, soundFrequency;

    public Frame frame;

    private void OnEnable()
    {
        frame = GameObject.FindObjectOfType<Frame>();
        scoreText = GameObject.Find("Total Score").GetComponent<Text>();
        timer = GameObject.FindObjectOfType<Timer>();
        pins = GameObject.FindObjectOfType<PinManager>();
        pinPoints = pins.pinsDown * pinValue;
        timePoints = (int)timer.GetTime()*timeValue;
        // totalScore = PlayerPrefs.GetInt("Current Score");
        CalculateScore();

        //save score of frame
        int currentHigh = PlayerPrefs.GetInt(frame.shortName + "_score", 0);
        if(targetScore > currentHigh)
        {
            PlayerPrefs.SetInt(frame.shortName + "_score", targetScore);
        }
        if(PlayerPrefs.GetInt(frame.shortName+"_complete") == 0)
        {
            PlayerPrefs.SetInt(frame.shortName+"_complete", 1); //mark frame as complete to unlock next frame on map
        }

        InvokeRepeating("PlaySound", soundDelay, soundFrequency);
    }

    //start disabled, count score on enable
    void Update()
    {
        if(currentScore < targetScore)
        {
            int scoreIncrement = (int)(Time.deltaTime * speed);
            // Debug.Log(scoreIncrement);
            currentScore += scoreIncrement;
        }
        if(currentScore >= targetScore && !finished)
        {
            //score finished counting!
            currentScore = targetScore;
            finished = true;
            CancelInvoke();
            // int prevScore = PlayerPrefs.GetInt("Current Score");
            PlayerPrefs.SetInt("Current Score", currentScore+totalScore);
            Debug.Log(currentScore + " " + PlayerPrefs.GetInt("Current Score"));
        }
        // int numZeros = numDigits - currentScore.ToString().Length;
        // string newScore = "";
        // for(int i = 0; i < numZeros; i++)
        // {
        //     newScore += "0";
        // }
        pitch += (pitchScale * Time.deltaTime);
        // scoreText.text = "SCORE " + newScore + currentScore.ToString();
        SetScoreText(currentScore+totalScore);
    }

    void CalculateScore()
    {
        targetScore = (int)(pins.pinsDown*pinValue + timer.GetTime()*timeValue);
    }

    void PlaySound()
    {
        scoreAudio.pitch = pitch;
        scoreAudio.PlayOneShot(scoreSound);
    }

    public void SetScoreText(int _score)
    {
        int numZeros = numDigits - _score.ToString().Length;
        string newScore = "";
        for(int i = 0; i < numZeros; i++)
        {
            newScore += "0";
        }

        scoreText.text = "SCORE " + newScore + _score.ToString();
    }
}
