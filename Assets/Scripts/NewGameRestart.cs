using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameRestart : MonoBehaviour {

    public string gameScene;

	// Use this for initialization
	void Start () {
	
	}

    bool isIncreasing = true;
	// Update is called once per frame
	void Update () {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        if (isIncreasing)
        {
            color.a += Time.deltaTime;
            if (color.a >= 1.0f) isIncreasing = false;
        }
        else
        {
            color.a -= Time.deltaTime;
            if (color.a <= 0.0f) isIncreasing = true;
        }
        renderer.color = color;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider)
            {
                NewGameRestart restart_button = hit.collider.gameObject.GetComponent<NewGameRestart>();
                if(restart_button != null)
                {
                    SceneManager.LoadScene(gameScene);
                }
            }
        }
	}
}
