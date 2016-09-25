using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnxietySystem : MonoBehaviour {

    //Anxiety rises over time. Finished puzzles can set it back, but this value reduces over time.
    //Even though the game rewards the player for completing puzzles, it should be obvious that a 
    //win condition is not being approached.

    public int relief_time;
    public float anxiety_buildup;
    public float reduce_rate;
    public float raise_rate;

    public float relief_time_left;
    
	// Use this for initialization
	void Start () {
        relief_time_left = relief_time;
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

    public bool TickRelief(List<GameObject> object_list = null)
    {
        relief_time_left -= Time.deltaTime;

        foreach(GameObject gob in object_list)
        {
            SpriteRenderer renderer = gob.transform.GetComponent<SpriteRenderer>();
            if (renderer == null) continue;
            if(relief_time_left >= (relief_time / 2))
            {
                Color newAlpha = renderer.color;
                newAlpha.a = (relief_time_left - (relief_time/2)) / (relief_time/2);
                renderer.color = newAlpha;
            }
            else
            {
                renderer.enabled = false;
            }
        }

        if(relief_time_left <= 0.0f)
        {
            Reset_Relief();
            return true;
        }
        return false;
    }

    public void Reset_Relief()
    {
        relief_time_left = relief_time;
    }
}
