using UnityEngine;
using System.Collections;

public class Touchable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Touch()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
