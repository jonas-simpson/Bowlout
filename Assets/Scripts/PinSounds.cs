using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSounds : MonoBehaviour
{
    public List<AudioClip> Sounds_pin;

    public AudioClip PullRandom()
    {
        return Sounds_pin[Random.Range(0, Sounds_pin.Count)];
    }
}
