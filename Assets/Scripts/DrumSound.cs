using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float volumeMultiplier;
    public Transform ball;
    public Collider col;

    private float distTrigger;

    private void Awake()
    {
        // ball = GameObject.FindObjectOfType<BallRoll>().transform;
        audioSource = gameObject.GetComponent<AudioSource>();
        distTrigger = col.bounds.extents.x;
    }

    private void OnEnable()
    {
        audioSource.Play();
        Debug.Log("Drum Sound -- Playing!");
    }
    private void OnDisable()
    {
        audioSource.Stop();
    }

    private void Update()
    {
        // Debug.Log(Vector3.Distance(this.transform.position, ball.position));
        // float currentVolume = (0.5f/(Vector3.Distance(this.transform.position, ball.position))*volumeMultiplier);
        // float currentVolume = Mathf.Clamp((Vector3.Distance(this.transform.position, ball.position) - distTrigger),0,5)*volumeMultiplier;
        float currentVolume = (distTrigger - Vector3.Distance(this.transform.position, ball.position))*volumeMultiplier;
        audioSource.volume = currentVolume;
        // Debug.Log("Volume: " + currentVolume);
    }
}
