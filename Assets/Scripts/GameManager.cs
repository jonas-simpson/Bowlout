using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        INTRO,
        PLAY,
        PAUSE,
        BOWL,
        SCORE,
        FINISH,
        FALLOUT,
        TIMEOUT
    }

    public Frame frame;

    //local elements
    public GameState myState;
    public bool allowInput;
    public float pinCheckWait, cameraSyncSpeed, waitAfterScore, waitAfterDeath, waitToTransition;
    public AudioSource audioSource;
    public AudioClip symbols;
    private bool switchedToScore;

    //external elements
    public Transform cameraObject;
    public Timer timer;
    public PinManager pins;
    public BallRoll ball;
    public GameObject ballTracker;
    public CameraStats camStats;
    public TweenUI scoreText;
    public CountScore scoreCount;
    public TweenUI startCamTween;
    public GameObject introBall;
    public SceneTransition1 sceneTransition;
    public GameObject pinGroup;
    public SceneSwitch sceneSwitch;
    public AudioClip deathSound;

    public Text stateText;
    public Text lifeText;
    public Text frameText;

    public int localLives;

    public TransitionText transitionText;

    private void Start()
    {
        scoreCount.totalScore = PlayerPrefs.GetInt("Current Score");
        scoreCount.SetScoreText(scoreCount.totalScore);

        Debug.Log(PlayerPrefs.GetInt("Current Score").ToString());

        sceneTransition = GameObject.FindObjectOfType<SceneTransition1>();
        sceneTransition.StartAnimate("outro");

        localLives = PlayerPrefs.GetInt("Local Lives");
        lifeText.text = "0"+localLives.ToString();

        // myState = GameState.PLAY;
        myState = GameState.INTRO;
        // stateText = GameObject.Find("Game State").GetComponent<Text>();
        UpdateUI();
        allowInput = false;
        frame = GameObject.FindObjectOfType<Frame>();
        frame.LoadFrameData();

        transitionText = GameObject.FindObjectOfType<TransitionText>();

        sceneSwitch = GameObject.FindObjectOfType<SceneSwitch>();

        frameText.text = frame.longName;
        // timer = FindObjectOfType<Timer>();
        // ball = FindObjectOfType<BallRoll>();
        // camStats = FindObjectOfType<CameraStats>();
        // pins = FindObjectOfType<PinManager>();
    }

    private void Update()
    {
        switch(myState)
        {
            case GameState.INTRO:
                if(startCamTween.complete)
                {
                    SwitchToPlay();
                }
                break;

            case GameState.PLAY:
                //run timer
                timer.CalculateTime();
                timer.UpdateUI();
                //move camera
                camStats.UpdateCamera();
                //apply gravity to ball
                // ball.ApplyGravity();
                break;

            case GameState.BOWL:
                //enable pin counter
                //apply gravity to ball
                // ball.ApplyGravity();
                //check for pin movement
                if(!pins.CheckMovement())
                {
                    // Debug.Log("Pins not moving - first check");
                    //if no movement, wait and check again
                    StartCoroutine(Wait(pinCheckWait, 0));
                    // if(!pins.CheckMovement() && pins.pinsDown > 0)
                    // {
                    //     //if still not moving, change to win state
                    //     Debug.Log("Pins still not moving!");
                    //     SwitchToFinish();
                    // }
                }
                else
                {
                    // Debug.Log("Pins still moving");
                }
                break;
            
            case GameState.SCORE:
                //wait for score to finish count
                if(scoreCount.finished)
                {
                    StartCoroutine(Wait(waitAfterScore));
                }
                break;
        }
    }

    public void SwitchToPlay()
    {
        cameraObject.transform.parent = ballTracker.transform; //parent camera to ball parent
        introBall.SetActive(false);    //disable fake ball
        ball.gameObject.SetActive(true);    //enable real ball
        allowInput = true; //allow input on real ball
        // Debug.Log(cameraObject.transform.eulerAngles.x);
        myState = GameState.PLAY;
        UpdateUI();

        // sceneTransition.ResetAnimation();
    }
    public void SwitchToBowl()
    {
        myState = GameState.BOWL;
        pinGroup.SetActive(true);
        camStats.MoveToPins(pins.camTarget, cameraSyncSpeed);
        allowInput = false;
        audioSource.PlayOneShot(symbols, 0.75f);
        ball.audioSource.enabled = false;
        ball.rollSource.enabled = false;
        UpdateUI();
    }
    public void SwitchToScore()
    {
        // allowInput = false;
        myState = GameState.SCORE;
        // scoreText.Animate();
        // scoreCount.enabled = true;
        StartCoroutine(WaitToScore());
        // Debug.Log(timer.bigTime.ToString());
        // Debug.Log(scoreCount.timePoints);
        // Debug.Log(scoreCount.pinPoints);
        PlaySceneTransition("intro");
        UpdateUI();
    }
    public void SwitchToFinish()
    {
        // allowInput = false;
        myState = GameState.FINISH;
        UpdateUI();
        frame.SaveFrameData();
        if(PlayerPrefs.GetString("Load Next") == "true")
            frame.NextFrame(); //load next frame if in set play
        else //else, go to fraame score scene
        {
            
        }
    }

    public IEnumerator WaitToScore()
    {
        int timePoints = (int)((timer.maxTime-timer.currentTime)*100);
        int pinPoints = pins.pinsDown * 1000;
        transitionText.EnterSuccess(timer.bigTime, timer.smallTime.ToString("F3").TrimStart('0'), timePoints, pins.pinsDown, pinPoints);
        yield return new WaitForSeconds(sceneTransition.totalTime+sceneTransition.delay+1f);
        scoreCount.enabled = true;
    }

    public void SwitchToStart()
    {
        //for use when the player dies
        frame.RestartFrame();
    }

    public IEnumerator Wait(float _time, int _Pin)
    {
        // WaitForSeconds(_time);
        
        yield return new WaitForSeconds(_time);
        // Debug.Log("after return");
        if(!pins.CheckMovement() && !switchedToScore)
        {
            switchedToScore = true;
            //if still not moving, change to win state
            // Debug.Log("Pins still not moving!");
            SwitchToScore();
            // PlaySceneTransition("intro");
            yield break;
        }
    }

    public IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        SwitchToFinish();
    }

    public IEnumerator WaitToRestart()
    {
        // ball.audioSource.enabled = false;
        // ball.rollSource.enabled = false;
        allowInput = false;
        if(myState == GameState.PLAY) //don't die after bowling
        {
            Debug.Log("Wait to Restart");
            myState = GameState.FALLOUT;
            Debug.Log("Switch to Fallout");
            //subtract life
            localLives --;
            PlayerPrefs.SetInt("Local Lives", localLives);
            //if lives > 0, reload level
            //else, game over
            StartCoroutine(WaitToSceneTransition());

            if(timer.currentTime >= timer.maxTime)
            {
                //enter time out
                transitionText.EnterDeath("timeout", localLives);
            }
            else
            {
                //fall out
                transitionText.EnterDeath("fallout", localLives);
            }

            if(localLives > 0)
            {
                
                yield return new WaitForSeconds(waitAfterDeath);
                Debug.Log("Restarting");
                SwitchToStart();
            }
            else
            {
                //go to main menu
                yield return new WaitForSeconds(waitAfterDeath);
                PlaySceneTransition("outro");
                sceneSwitch.LoadGameOver();
            }
        }
    }

    public IEnumerator WaitToSceneTransition()
    {
        yield return new WaitForSeconds(waitToTransition);
        audioSource.PlayOneShot(deathSound);
        //play scene transition
        PlaySceneTransition("intro");
    }


    public void UpdateUI()
    {
        // stateText.text = myState.ToString();
    }

    public void PlaySceneTransition(string mode)
    {
        sceneTransition.StartAnimate(mode);
    }
    
}
