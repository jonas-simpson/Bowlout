using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneTransition1Element : MonoBehaviour
{
    public Vector3 dir, startPos;
    public float midPos, endPos, duration, delay;
    public Ease introEase, outroEase;

    private void Awake()
    {
        startPos = transform.localPosition;
    }

    public void Intro()
    {
        transform.DOLocalMoveX(midPos, duration).SetDelay(delay).SetEase(introEase);
    }

    public void Outro()
    {
        transform.DOLocalMoveX(endPos, duration).SetDelay(delay).SetEase(outroEase);
    }

    public void ResetPosition()
    {
        transform.localPosition = startPos;
    }

    public float GetDuration()
    {
        return (delay+duration);
    }
}
