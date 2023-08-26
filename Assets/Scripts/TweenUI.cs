using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenUI : MonoBehaviour
{
    public Vector3 endPos, endRot, endScale;
    public Vector3 startPos, startRot, startScale;
    public float moveSpeed, moveDelay, rotSpeed, rotDelay, scaleSpeed, scaleDelay;
    public Ease moveEase, rotEase, scaleEase;
    public float currentTime;
    public bool complete;

    private void OnEnable()
    {
        startPos = transform.localPosition;
        // Debug.Log(startPos.y);
        startRot = transform.localEulerAngles;
        startScale = transform.localScale;
    }

    public void Animate()
    {
        gameObject.transform.DOLocalMove(endPos, moveSpeed).SetEase(moveEase).SetDelay(moveDelay);
        gameObject.transform.DOLocalRotate(endRot, rotSpeed, RotateMode.LocalAxisAdd).SetEase(rotEase).SetDelay(rotDelay);
        gameObject.transform.DOScale(endScale, scaleSpeed).SetEase(scaleEase).SetDelay(scaleDelay);
    }

    public void Reverse()
    {
        gameObject.transform.DOLocalMove(startPos, moveSpeed).SetEase(moveEase).SetDelay(moveDelay);
        gameObject.transform.DOLocalRotate(startRot, rotSpeed, RotateMode.LocalAxisAdd).SetEase(rotEase).SetDelay(rotDelay);
        gameObject.transform.DOScale(startScale, scaleSpeed).SetEase(scaleEase).SetDelay(scaleDelay);
    }

    public void ReInitialize()
    {
        //set element back to start
        transform.localPosition = startPos;
        transform.localEulerAngles = startRot;
        transform.localScale = startScale;
    }

    private void Update()
    {
        EvaluateTime();
    }

    public float GetDuration()
    {
        float duration = 0f;

        //speed
        if(moveSpeed > rotSpeed && moveSpeed > scaleSpeed)
        {
            //move is longest
            duration += moveSpeed;
        }
        else if(rotSpeed > scaleSpeed)
        {
            //rotation is longest
            duration += rotSpeed;
        }
        else
        {
            //scale is longest
            duration += scaleSpeed;
        }

        //delay
        if(moveDelay > rotDelay && moveDelay > scaleDelay)
        {
            //move is longest
            duration += moveDelay;
        }
        else if(rotDelay > scaleDelay)
        {
            //rotation is longest
            duration += rotDelay;
        }
        else
        {
            //scale is longest
            duration += scaleDelay;
        }

        return duration;
    }

    private protected void EvaluateTime()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= moveSpeed+moveDelay && currentTime >= rotSpeed+rotDelay && currentTime >= scaleSpeed+scaleDelay)
        {
            //disable when animation completes
            complete = true;
            this.enabled = false;
        }
    }
}
