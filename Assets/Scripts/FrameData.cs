using UnityEngine;

[System.Serializable]
public class FrameData
{
    //This class does not inherit from Monobehavior. Every Frame in the game saves data to its own FrameData.
    //This class is where that data is stored.

    public bool completed; //whether or not the player has completed this level, in any mode
    public int highScore; //highest score player has ever earned on this frame, in any mode
    // public int currentLives;

    public FrameData(Frame _frame)
    {
        Debug.Log("Frame High Score: " + _frame.highScore.ToString());
        Debug.Log("Frame Current Score: " + _frame.currentScore.ToString());

        if(_frame.currentScore > _frame.highScore)
        {
            highScore = _frame.currentScore;
            Debug.Log("New high score! " + _frame.currentScore.ToString() + ", " + _frame.highScore.ToString());
        }
        else
        {
            highScore = _frame.highScore;
            Debug.Log("Not a high score. " + _frame.currentScore.ToString() + ", " + _frame.highScore.ToString());
        }
        completed = true; //we only save when the level has been completed
    }
}