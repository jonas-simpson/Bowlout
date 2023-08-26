using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinManager : MonoBehaviour
{
    public List<Pin> pins;
    public int totalPins, pinsDown;
    public bool bowling;

    public Text pinCounter;
    public GameManager manager;
    public Transform camTarget;
    public PinSounds sounds;

    public AudioClip pinDownSound;
    public AudioSource pinDownAudio;
    public float audioPitch, pitchScale;

    

    // Start is called before the first frame update
    void Start()
    {
        Pin[] pinArray = GameObject.FindObjectsOfType<Pin>();
        foreach(Pin pin in pinArray)
        {
            pins.Add(pin);
        }
        totalPins = pins.Count;
        pinsDown = 0;

        manager = GameObject.FindObjectOfType<GameManager>();
        // pinCounter = GameObject.Find("Pin Counter").GetComponent<Text>();
        camTarget = GameObject.Find("Camera Target").transform;
        sounds = gameObject.GetComponent<PinSounds>();
        UpdateUI();
        pinCounter.gameObject.SetActive(false);
    }

    public void PinDown()
    {
        // if(pinsDown == 0)
        // {
        //     Debug.Log("Pin Down!");
        //     manager.SwitchToBowl();
        //     SetKinematic();
        //     EnableUI();
        // }
        PlayAudio();
        pinsDown++;
        UpdateUI();
    }

    public void EnterBowl()
    {
        if(!bowling)
        {
            bowling = true;
            Debug.Log("Pin Down!");
            manager.SwitchToBowl();
            SetKinematic();
            EnableUI();
        }
    }

    public void UpdateUI()
    {
        string pinText = pinsDown.ToString() + "/" + totalPins.ToString();
        pinCounter.text = pinText;
    }

    public void EnableUI()
    {
        pinCounter.gameObject.SetActive(true);
    }

    public void SetKinematic()
    {
        foreach(Pin pin in pins)
        {
            pin.SetKinematic(true);
        }
    }

    public bool CheckMovement()
    {
        bool stillMoving = false;
        foreach(Pin pin in pins)
        {
            if(pin.CheckIfMoving())
            {
                stillMoving = true;
                break;
            }
        }
        return stillMoving;
    }

    private void PlayAudio()
    {
        pinDownAudio.pitch = audioPitch;
        audioPitch += pitchScale;
        pinDownAudio.PlayOneShot(pinDownSound);
    }
}
