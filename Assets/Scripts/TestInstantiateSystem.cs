using UnityEngine;
using System.Collections;

public class TestInstantiateSystem : MonoBehaviour {

    public GameObject prefab;
	// Use this for initialization
	void Start () {
        GameObject.Instantiate(prefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
