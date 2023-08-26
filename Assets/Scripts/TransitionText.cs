using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionText : MonoBehaviour
{
    //move to set from menu
    public Text setText;
    public Text highScoreText;

    //die text
    public GameObject fallOutText;
    public GameObject timeOutText;
    public GameObject ballCountObject;
    public Text ballCountText;

    //success text
    public GameObject successText;
    public Text timeCalculationText;
    public Text pinCalculationText;
    public Text scoreCalculationText;

    public void EnterSet(string setName, int highScore)
    {
        SetEnterState(true);
        setText.text = setName;
        highScoreText.text = "High Score " + highScore.ToString();

        SetDeathState(false, "");

        SetSuccessState(false);

        Debug.Log("Transition Text - Enter Set");
    }

    public void EnterDeath(string _state, int _ballCount)
    {
        setText.enabled = false;
        highScoreText.enabled = false;

        SetDeathState(true, _state);
        ballCountText.text = "0" + _ballCount.ToString();

        SetSuccessState(false);

        Debug.Log("Transition Text - Enter Death");
    }

    public void EnterSuccess(int _bigTime, string _smallTime, int _timeValue, int _pinCount, int _pinValue)
    {
        SetEnterState(false);
        SetDeathState(false, "");
        SetSuccessState(true);
        Debug.Log(_pinValue);
        Debug.Log(_timeValue);
        timeCalculationText.text = "Time | " + _bigTime.ToString() + _smallTime + " x 100 = " + _timeValue.ToString();
        pinCalculationText.text = "Pins | " + _pinCount.ToString() + " x 1000 = " + _pinValue.ToString();

        Debug.Log("Transition Text - Enter Success");
    }


    private void SetEnterState(bool _state)
    {
        setText.enabled = _state;
        highScoreText.enabled = _state;
    }

    private void SetDeathState(bool _state, string _cause)
    {
        if(_cause == "fallout" && _state == true)
        {
            fallOutText.SetActive(true);
            timeOutText.SetActive(false);
        }
        else if(_cause == "timeout" && _state == true)
        {
            timeOutText.SetActive(true);
            fallOutText.SetActive(false);
        }
        else
        {
            timeOutText.SetActive(false);
            fallOutText.SetActive(false);
        }
        ballCountObject.SetActive(_state);
        // ballCountText.text = "0" + _ballCount.ToString();
    }

    private void SetSuccessState(bool _state)
    {
        successText.SetActive(_state);
        timeCalculationText.enabled = _state;
        pinCalculationText.enabled = _state;
        scoreCalculationText.enabled = _state;
    }

}
