using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStage : MonoBehaviour
{
    public Frame myFrame, prior;
    public Material complete, next, locked;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if(prior != null) //first level has no prior
        {
            if(PlayerPrefs.GetInt(prior.shortName+"_complete") != 0)
            {
                //this stage is unlocked
                if(PlayerPrefs.GetInt(myFrame.shortName+"_complete") != 0)
                {
                    //this level has been completed
                    renderer.material = complete;
                    Debug.Log(myFrame.shortName + " has been completed.");
                }
                else
                {
                    //this level is the next level
                    renderer.material = next;
                    Debug.Log(myFrame.shortName + " is the next level.");
                }
            }
            else
            {
                //this stage is not unlocked
                renderer.material = locked;
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log(myFrame.shortName + " is locked.");
            }
        }
        else if(PlayerPrefs.GetInt(myFrame.shortName+"_complete") == 0)
        {
            //level 1 is not complete
            renderer.material = next;
            Debug.Log(myFrame.shortName + " is the next level.");
        }
    }
}
