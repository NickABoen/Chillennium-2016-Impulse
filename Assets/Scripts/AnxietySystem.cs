using UnityEngine;
using System.Collections;

public class AnxietySystem : MonoBehaviour {

    //Anxiety rises over time. Finished puzzles can set it back, but this value reduces over time.
    //Even though the game rewards the player for completing puzzles, it should be obvious that a 
    //win condition is not being approached.

    public int relief_time;
    public float anxiety_buildup;
    public float reduce_rate;
    public float raise_rate;

    private float relief_time_left;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReduceAnxiety()
    {
        //Debug.Log("Reducing Anxiety");
    }

    public void RaiseAnxiety(bool solved)
    {
        //Debug.Log("Raising Anxiety");
    }

    public bool TickRelief()
    {
        return false;
    }

    public void Reset_Relief()
    {
        relief_time_left = relief_time;
    }
}
