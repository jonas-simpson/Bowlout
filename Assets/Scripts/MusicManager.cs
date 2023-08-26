using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject[] musicArray = GameObject.FindGameObjectsWithTag("Music");
        foreach(GameObject music in musicArray)
        {
            if(music != this.gameObject)
            {
                music.SetActive(false);
            }
        }
    }
}