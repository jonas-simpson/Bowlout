using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadStart()
    {
        SceneManager.LoadScene("Menu - Start");
    }

    public void LoadQuickPlay()
    {
        SceneManager.LoadScene("Menu - QuickPlay");
    }

    public void LoadSetPlay()
    {
        SceneManager.LoadScene("Menu - SetPlay");
    }

    public void LoadSingleFrame(string _frameName)
    {
        SceneManager.LoadScene(_frameName);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Menu - GameOver");
    }

    public void LoadVictory()
    {
        SceneManager.LoadScene("Menu - Victory");
    }

    public void LoadHub()
    {
        SceneManager.LoadScene("HUB");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Menu - Credits");
    }
}