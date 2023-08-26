using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject music = GameObject.Find("Music 1(Clone)");
        DestroyImmediate(music);
    }
}
