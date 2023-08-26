using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    public Transform cam;
    public float minTime, maxTime, myTime;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindObjectOfType<Camera>().transform;
        rb = gameObject.GetComponent<Rigidbody>();
        myTime = Random.Range(minTime, maxTime);
        StartCoroutine(DestroyAfterSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(myTime);
        gameObject.SetActive(false);
    }

    public void ApplyForce(Vector3 _direction, float _force)
    {
        rb.AddForce(_direction*_force, ForceMode.Impulse);
    }
}
