using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchComponent : MonoBehaviour {

    public bool isOn = false;
    public Sprite OnSprite;
    public Sprite OffSprite;
    public AudioClip clip_1, clip_2, clip_3;

    List<AudioClip> clip_list;

	// Use this for initialization
	void Start () {
        clip_list = new List<AudioClip> { clip_1, clip_2, clip_3 };
	}
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = isOn ? OnSprite : OffSprite;
	}

    public void Toggle()
    {
        isOn = !isOn;
        AudioSource.PlayClipAtPoint(clip_list[Random.Range(0,clip_list.Count)], Camera.main.transform.position);
    }
}
