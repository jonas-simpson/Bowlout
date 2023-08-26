using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text bigText, smallText;
    public int bigTime, maxTime;
    public float smallTime, currentTime;
    public AudioSource audioSource;
    public AudioClip beep;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        bigText = GameObject.Find("Big Time").GetComponent<Text>();
        smallText = GameObject.Find("Small Time").GetComponent<Text>();
        manager = GameObject.FindObjectOfType<GameManager>();

        bigTime = maxTime;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // smallTime -= Time.deltaTime;
        // if(smallTime < 0)
        // {
        //     smallTime = 0.99f;
        //     bigTime --;
        // }

        // UpdateUI();
    }

    public void CalculateTime()
    {
        smallTime -= Time.deltaTime;
        currentTime += Time.deltaTime;
        if(smallTime < 0 && currentTime < maxTime)
        {
            PLaySound();
            bigTime --;
            smallTime = 0.99f;
            Debug.Log("Big Time Decrease");
        }

        if(currentTime >= maxTime)
        {
            //time up!
            PLaySound();
            bigTime = 0;
            smallTime = 0.00f;
            Debug.Log("Time Out!");
            StartCoroutine(manager.WaitToRestart());
        }
    }

    public void UpdateUI()
    {
        if(bigTime >= 100)
            bigText.text = bigTime.ToString();
        else if(bigTime >= 10)
        {
            string newBig = bigTime.ToString();
            newBig = newBig.Insert(0, "0");
            bigText.text = newBig;
        }
        else
        {
            string newBig = bigTime.ToString();
            newBig = newBig.Insert(0, "00");
            bigText.text = newBig;
        }
        string newSmall = smallTime.ToString("F2");
        newSmall = newSmall.TrimStart('0');
        smallText.text = newSmall;
    }

    public void PLaySound()
    {
        if(bigTime < 10)
            audioSource.pitch += .05f;
        audioSource.PlayOneShot(beep);
    }

    public float GetTime()
    {
        return bigTime + smallTime;
    }
}
