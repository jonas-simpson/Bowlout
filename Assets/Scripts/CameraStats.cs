using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraStats : MonoBehaviour
{
    public Transform ball;
    public Rigidbody rb;
    public Transform pointer;
    public float rotSpeed;

    public Ease easePos;
    public Ease easeRot;

    // Start is called before the first frame update
    void Start()
    {
        // ball = GameObject.FindObjectOfType<BallRoll>().gameObject.transform;
        // rb = ball.GetComponent<Rigidbody>();
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     transform.position = ball.transform.position;
    //     Vector3 forward = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
    //     //Vector3 up = new Vector3(0,-rb.velocity.y,0);
    //     if(forward != Vector3.zero)
    //     {
    //         //transform.rotation = Quaternion.LookRotation(forward, up);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime*rotSpeed);
    //     }
    // }

    public void UpdateCamera()
    {
        transform.position = ball.transform.position;
        Vector3 forward = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        //Vector3 up = new Vector3(0,-rb.velocity.y,0);
        if(forward != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(forward, up);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), Time.deltaTime*rotSpeed);
        }
    }

    public void MoveToPins(Transform target, float _time)
    {
        // float elapsedTime = 0.0f;
        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;

        transform.DOMove(target.position, _time).SetEase(easePos);
        transform.DORotate(target.rotation.eulerAngles, _time).SetEase(easeRot);
        // while(elapsedTime < _time)
        // {
        //     transform.position = Vector3.Lerp(startingPos, target.position, (elapsedTime / _time));
        //     transform.rotation = Quaternion.Slerp(startingRot, target.rotation,  (elapsedTime / _time));
        //     elapsedTime += Time.deltaTime;
        //     yield return new WaitForEndOfFrame();
        // }
    }

    // public void Tween(GameObject target, Vector3 goal, Ease ease, float speed, float delay)
    // {
    //     //Debug.Log(goal);
    //     target.transform.DOMove(goal, speed).SetEase(ease).SetDelay(delay);
    // }
}
