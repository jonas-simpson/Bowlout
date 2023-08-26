using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{
    //local variables
    public float force;
    public float maxSpin;
    public float gravityMultiplier;
    public float trailTime;
    public bool grounded;

    public List<GameObject> collisions;
    public List<AudioClip> fallSounds;
    public List<AudioClip> landSounds;
    public AudioClip whoosh;

    public GameManager gameManager;

    // public AudioClip roll;
    public AudioSource rollSource;
    public float rollScale, rollPitch, rollLerp;

    public Collider drumZone;

    public TrailRenderer[] trails;

    public float sparkMin, sparkMax;

    //local components
    public Rigidbody rb;
    public AudioSource audioSource;
    public EffectSpawn effect;

    //external components
    public InputManager input;
    public Transform cam;
    public DrumSound drumSound;
    //public GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        effect = gameObject.GetComponent<EffectSpawn>();
        rb.maxAngularVelocity = maxSpin;
        cam = GameObject.FindObjectOfType<CameraStats>().gameObject.transform;
        trails = gameObject.GetComponentsInChildren<TrailRenderer>();
        SetTrails(false);
        drumZone = GameObject.Find("Drum Trigger").GetComponent<Collider>();
        // drumSound = GameObject.FindObjectOfType<DrumSound>();
        SetGravity();
    }

    private void Update()
    {
        // ApplyGravity();
        if(rb.velocity.magnitude < 2)
        {
            foreach(TrailRenderer trail in trails)
            {
                trail.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach(TrailRenderer trail in trails)
            {
                trail.gameObject.SetActive(true);
            }
        }
        float currentPitch = Mathf.Lerp(rollPitch, rb.angularVelocity.magnitude, rollLerp);
        PlayAudio(rollSource, currentPitch*rollScale);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void OnCollisionEnter(Collision other)
    {
        // if(!audioSource.isPlaying)
            PlayAudio(PullRandomLand(), Mathf.Abs(rb.velocity.magnitude)/8f, Random.Range(.75f, 1f));
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
            if(!collisions.Contains(other.gameObject))
            {
                collisions.Add(other.gameObject);
            }
        }
        if(other.gameObject.tag == "Pin")
        {
            drumSound.enabled = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = false;
            collisions.Remove(other.gameObject);
        }
        StartCoroutine(RequestSound());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Drum Zone")
        {
            Debug.Log("Entering Drum Zone");
            drumSound.enabled = true;
            Debug.Log(drumSound.isActiveAndEnabled);
        }
        if(other.gameObject.tag == "Fall Out")
        {
            //Player has fallen out of bounds.
            // Debug.Log("Die!");
            // gameManager.WaitToRestart();
            StartCoroutine(gameManager.WaitToRestart());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Drum Zone")
        {
            Debug.Log("Exiting the Drum Zone");
            drumSound.enabled = false;
        }
    }

    public void ApplyForce(Vector2 inputVector)
    {
        //Vector3 worldVector = new Vector3(inputVector.y, 0.0f, -inputVector.x);

        //acquire camera rotation
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        //project forward and right vectors on the horizontal plane
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * -inputVector.x + right * inputVector.y;

        rb.AddTorque(moveDir*force, ForceMode.Impulse);

        //play audio
        PlayAudio(whoosh, Random.Range(0.25f, 0.5f), Random.Range(0.75f, 1.25f));

        //enable trails
        // SetTrails(true);
        // StartCoroutine(SetTrailsForTime(trailTime));

        //spawn effects
        Vector3 spawnDirection = new Vector3(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)).normalized;
        // effect.Spawn(spawnDirection, Random.Range(sparkMin,sparkMax));
    }

    private void SetGravity()
    {
        rb.useGravity = false;
    }

    public void ApplyGravity()
    {
        rb.AddForce(Vector3.up * (-9.81f * gravityMultiplier), ForceMode.Acceleration);
    }

    public void PlayAudio(AudioClip _audio)
    {
        audioSource.PlayOneShot(_audio);
    }
    public void PlayAudio(AudioClip _audio, float _volume)
    {
        audioSource.PlayOneShot(_audio, _volume);
    }
    public void PlayAudio(AudioClip _audio, float _volume, float _pitch)
    {
        audioSource.pitch = _pitch;
        audioSource.PlayOneShot(_audio, _volume);
        // audioSource.pitch = 1;
    }
    public void PlayAudio(AudioSource _source, float _pitch)
    {
        _source.pitch = _pitch;
    }

    public AudioClip PullRandomFall()
    {
        return fallSounds[Random.Range(0, fallSounds.Count)];
    }

    public IEnumerator RequestSound()
    {
        yield return new WaitForFixedUpdate();
        if(collisions.Count == 0)
        {
            PlayAudio(PullRandomFall(), 0.5f);
        }
    }

    public AudioClip PullRandomLand()
    {
        return landSounds[Random.Range(0, landSounds.Count)];
    }

    public void SetTrails(bool _state)
    {
        foreach(TrailRenderer trail in trails)
        {
            trail.gameObject.SetActive(_state);
        }
    }

    // public IEnumerator SetTrailsForTime(float _seconds)
    // {
    //     Debug.Log("Enabling Trails");
    //     yield return new WaitForSeconds(_seconds);
    //     Debug.Log("Disabling Trails");
    //     SetTrails(false);
    // }
}
