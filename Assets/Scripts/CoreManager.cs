using System.Collections;
using System.Collections.Generic;

public class CoreManager
{
    [System.Serializable]
    public class Frame //this is one level of the game
    {
        bool completed; //whether or not the player has completed this level, in any mode
        int timeLimit; //number of seconds to put on the timer
        int highScore; //highest score player has ever earned on this frame, in any mode
    }

    public class Set //this is a string of levels in the game.
    {
        int length; //number of frames in the set
        Frame[] frames; //array of frames in the set
        bool completed; //whether or not the player has completed this set
        int highScore; //highest score player has ever earned on this set

        public Set()
        {
            
        }
    }
}
