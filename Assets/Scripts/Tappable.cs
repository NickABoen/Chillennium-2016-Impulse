using UnityEngine;
using System.Collections;

public class Tappable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Tap()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
