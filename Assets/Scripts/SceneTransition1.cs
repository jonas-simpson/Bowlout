using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneTransition1 : MonoBehaviour
{
    public float totalTime, delay, frequency, wait;
    public float currentTime;
    public bool outro;
    private int i;
    public Ease ease;

    public List<SceneTransition1Element> elements;


    // private void Start()
    // {
    //     GameManager manager = GameObject.FindObjectOfType<GameManager>();
    //     if(manager != null)
    //     {
    //         //in game mode, play outro
    //         AddAnimate();
    //     }
    //     else
    //     {
    //         //dont
    //     }
    // }

    public void StartAnimate(string mode)
    {
        //set frequency
        i = 0;
        frequency = totalTime*elements[0].GetDuration();
        // foreach(SceneTransition1Element element in elements)
        // {
        //     element.ReInitialize();
        //     element.endScale = element.startScale;
        //     element.endPos.y = element.startPos.y;
        // }
        Debug.Log("Beginning");
        if(mode == "intro")
        {
            ResetAnimation();
            InvokeRepeating("AnimateNextIntro", delay, frequency);
        }
        else if (mode == "outro")
            InvokeRepeating("AnimateNextOutro", delay, frequency);
    }

    // private void Update()
    // {
    //     currentTime += Time.deltaTime;
    //     if(currentTime >= delay+totalTime+wait && !outro)
    //     {
    //         outro = true;
    //         AddAnimate();
    //         InvokeRepeating("AnimateNext", 0.0f, frequency);
    //     }
    // }

    // public void AddAnimate()
    // {
    //     foreach(SceneTransition1Element element in elements)
    //     {
    //         element.enabled = true;
    //         element.moveEase = Ease.InCubic;
    //         // element.ReInitialize();
    //         element.endPos = new Vector3(850f, element.startPos.y, 0f);
    //     }
    // }

    public void AnimateNextIntro()
    {
        // Debug.Log("Animate Intro " + i);

        elements[i].Intro();

        i++;
        if(i >= elements.Count)
        {
            i=0;
            CancelInvoke();
            // foreach(TweenUI element in elements)
            // {
            //     element.enabled = false;
            // }
        }
    }

    public void AnimateNextOutro()
    {
        // Debug.Log("Animate Outro " + i);

        elements[i].Outro();

        i++;
        if(i >= elements.Count)
        {
            i=0;
            CancelInvoke();
            // foreach(TweenUI element in elements)
            // {
            //     element.enabled = false;
            // }
        }
    }

    public void ResetAnimation()
    {
        foreach(SceneTransition1Element element in elements)
        {
            element.ResetPosition();
        }
    }
}
