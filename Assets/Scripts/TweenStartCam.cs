using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenStartCam : TweenUI
{
    public TweenUI ballTween;
    public Rigidbody ballRB;
    public float jumpForce;
    public Transform target;
    public AudioSource audioSource;
    public AudioClip ballEnter;

    public TweenDouble ready;
    public TweenDouble go;

    private bool began;

    private void Start()
    {
        endPos = target.transform.position;
        endRot = target.transform.localEulerAngles;
        endScale = target.transform.localScale;
        ready = GameObject.Find("READY").GetComponent<TweenDouble>();
        go = GameObject.Find("GO").GetComponent<TweenDouble>();
        Animate();
    }

    private void Animate()
    {
        gameObject.transform.DOLocalMove(endPos, moveSpeed).SetEase(moveEase).SetDelay(moveDelay);
        gameObject.transform.DOLocalRotate(endRot, rotSpeed, RotateMode.Fast).SetEase(rotEase).SetDelay(rotDelay);
        gameObject.transform.DOScale(endScale, scaleSpeed).SetEase(scaleEase).SetDelay(scaleDelay);
    }

    private void Update()
    {
        EvaluateTime();
        if(!began && currentTime >= (moveSpeed+moveDelay) - (ballTween.scaleSpeed+ballTween.scaleDelay)*2)
        {
            began = true;
            ballRB.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            ballTween.Animate();
            audioSource.PlayOneShot(ballEnter);
        }
        if(currentTime >= (moveSpeed+moveDelay) - (ready.GetDuration()+go.GetDuration()))
        {
            ready.Animate();
        }
        if(currentTime >= (moveSpeed+moveDelay) - (go.GetDuration()))
        {
            go.Animate();
        }
    }
}
