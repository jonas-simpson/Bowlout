using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOutMusic : MonoBehaviour
{
    public float speed;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GameObject.Find("BG Music").GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume -= Time.deltaTime * speed;
    }

}
