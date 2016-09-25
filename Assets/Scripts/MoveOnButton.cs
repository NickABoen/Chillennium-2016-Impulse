using UnityEngine;
using System.Collections;

public class MoveOnButton : MonoBehaviour {

    public float growth_factor;

    private Vector3 scale;
	// Use this for initialization
	void Start () {
        scale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        CompulsionSystem.enCycleState current_state = Object.FindObjectOfType<CompulsionSystem>().ocd_cycle;
        if(current_state == CompulsionSystem.enCycleState.Anxiety)
        {
            float growth = Time.deltaTime * growth_factor;
            gameObject.transform.localScale = new Vector3(Mathf.Clamp(gameObject.transform.localScale.x + growth, 0, scale.x),
                Mathf.Clamp(gameObject.transform.localScale.x + growth, 0, scale.y),
                Mathf.Clamp(gameObject.transform.localScale.z + growth, 0, scale.z));
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
	}
}
