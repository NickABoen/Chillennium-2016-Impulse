using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnxietySystem : MonoBehaviour {

    //Anxiety rises over time. Finished puzzles can set it back, but this value reduces over time.
    //Even though the game rewards the player for completing puzzles, it should be obvious that a 
    //win condition is not being approached.

    public int relief_time;
    public float anxiety_buildup;
    public float run_time;
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
        run_time -= 2 * Time.deltaTime; //I don't expect people to stay in this state long unless they figure out what's going on.
        CalculateAnxiety();
    }

    public void RaiseAnxiety(bool solved)
    {
        //Debug.Log("Raising Anxiety");
        if (!solved)
        {
            run_time += Time.deltaTime;
            CalculateAnxiety();
        }
    }

    void CalculateAnxiety()
    {
        run_time = Mathf.Min(run_time, 70);
        anxiety_buildup = Mathf.Clamp(((float)1 / 60) * (run_time * run_time),0,50);// (1/60) * x^2
    }

    public bool TickRelief(List<GameObject> object_list = null)
    {
        MessageSystem.enAggressiveness aggressiveness = gameObject.GetComponent<MessageSystem>().aggressiveness;
        relief_time_left -= (Time.deltaTime * (int)(aggressiveness + 1));

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
