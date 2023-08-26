using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenDouble : MonoBehaviour
{
    public float endX, scaleSpeed, moveSpeed, scaleDelay, moveDelay;
    private float currentTime;
    private bool began, complete1, complete2;
    public Ease scaleEase, moveEase;

    public void Animate()
    {
        began = true;
        transform.DOScale(new Vector3(0.6f, 0.6f, 1f), scaleSpeed).SetEase(scaleEase).SetDelay(scaleDelay);
    }

    private void Update()
    {
        if(began)
            EvaluateTime();
    }

    private protected void EvaluateTime()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= scaleDelay+scaleSpeed && !complete1)
        {
            complete1 = true;
            transform.DOLocalMoveX(endX, moveSpeed).SetEase(moveEase).SetDelay(moveDelay);
        }
        if(currentTime >= scaleDelay+scaleSpeed+moveDelay+moveSpeed)
        {
            complete2 = true;
            gameObject.SetActive(false);
        }
    }

    public float GetDuration()
    {
        return (scaleDelay+scaleSpeed+moveDelay+moveSpeed);
    }
}
