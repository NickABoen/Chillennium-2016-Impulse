using UnityEngine;
using System.Collections;

public class SwitchComponent : MonoBehaviour {

    public bool isOn = false;
    public Sprite OnSprite;
    public Sprite OffSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = isOn ? OnSprite : OffSprite;
	}
}
