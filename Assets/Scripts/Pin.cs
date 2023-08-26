using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public Rigidbody rb;
    public bool grounded, moving, down;
    public float maxRot, minVel, acceleration, lastVelocity, accelerationScale;

    public PinManager pinManager;
    public AudioSource audioSource;

    public TrailRenderer[] trails;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pinManager = GameObject.FindObjectOfType<PinManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        trails = gameObject.GetComponentsInChildren<TrailRenderer>();
        SetKinematic(false);
        foreach(TrailRenderer trail in trails)
        {
            trail.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if moving, moving
        if(rb.angularVelocity.magnitude >= minVel)
        {
            moving = true;
            foreach(TrailRenderer trail in trails)
            {
                trail.gameObject.SetActive(true);
            }
        }
        else
        {
            moving = false;
            foreach(TrailRenderer trail in trails)
            {
                trail.gameObject.SetActive(false);
            }
        }

        if((Mathf.Abs(transform.rotation.x) >= maxRot
        || Mathf.Abs(transform.rotation.z) >= maxRot)
        && down == false)
        {
            //when rotation surpasses threshold, mark as DOWN
            down = true;
            pinManager.PinDown();
        }

        //sound for sudden movement
        acceleration = Mathf.Abs((rb.angularVelocity.magnitude) - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = rb.angularVelocity.magnitude;
        if(acceleration != 0)
            // Debug.Log(acceleration);
        if(acceleration >= accelerationScale && !audioSource.isPlaying && grounded)
        {
            PlayPinSound(acceleration/50f);
        }

        if(Mathf.Abs(rb.angularVelocity.magnitude) <= 0.1 && grounded)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }

    public void SetKinematic(bool _state)
    {
        rb.useGravity = _state;
    }

    public bool CheckIfMoving()
    {
        if(moving && grounded && !down)
            return true;
        else
            return false;
        // if(rb.velocity.magnitude >= minMove)
        //     return true;
        // else
        //     return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        float audioLevel = other.relativeVelocity.magnitude / 10f;
        PlayPinSound(audioLevel);

        if(other.gameObject.CompareTag("Player"))
        {
            //if player, enter bowl mode
            // pinManager.PinDown();
            pinManager.EnterBowl();
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void PlayPinSound(float _volume)
    {
        // if(_volume > 1.0)
        // {
        //     audioSource.pitch = _volume/1.1f;
        // }
        // else
        //     audioSource.pitch = 1;
        audioSource.PlayOneShot(pinManager.sounds.PullRandom(), _volume);
        // Debug.Log("Volume: " + _volume.ToString());
    }
}
