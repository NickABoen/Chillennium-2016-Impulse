﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DraggingSystem : MonoBehaviour {

    public GameObject currently_dragging;
    private bool has_interacted = false;
    private GameObject previous_dragging;
    private bool cancel_momentum = false;

	// Use this for initialization
	void Start () {
        currently_dragging = null;
	}

    void FixedUpdate()
    {
        if (cancel_momentum && previous_dragging != null)
        {
            previous_dragging.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            previous_dragging.GetComponent<Rigidbody2D>().angularVelocity = 0;
            cancel_momentum = false;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit.collider != null)
        {
            Touchable touchable_component = hit.transform.gameObject.GetComponent<Touchable>();
            if(touchable_component != null && touchable_component.enabled)
            {
                touchable_component.Touch();
                has_interacted = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            previous_dragging = currently_dragging;
            cancel_momentum = true;
            currently_dragging = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null)
            {
                Draggable draggable_component = hit.transform.gameObject.GetComponent<Draggable>();
                SwitchComponent switch_component = hit.transform.gameObject.GetComponent<SwitchComponent>();
                Tappable tappable_component = hit.transform.gameObject.GetComponent<Tappable>();
                MoveOnButton moveon_component = hit.transform.gameObject.GetComponent<MoveOnButton>();
                RestartButton restart_component = hit.transform.gameObject.GetComponent<RestartButton>();
                if (draggable_component != null && draggable_component.enabled)
                {
                    currently_dragging = hit.transform.gameObject;
                    has_interacted = true;
                }
                else if (switch_component != null && switch_component.enabled)
                {
                    switch_component.Toggle();
                    has_interacted = true;
                }

                if(tappable_component != null && tappable_component.enabled)
                {
                    tappable_component.Tap();
                    has_interacted = true;
                }

                if(moveon_component != null && moveon_component.enabled)
                {
                    gameObject.GetComponent<CompulsionSystem>().MoveOn();
                }

                if(restart_component != null && restart_component.enabled)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        if(currently_dragging != null)
        {
            Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPoint.z = currently_dragging.transform.position.z;
            currently_dragging.transform.position = newPoint;
        }
	}

    public bool PlayerHasInteracted()
    {
        return has_interacted;
    }

    public void ResetInteracted()
    {
        has_interacted = false;
    }
}
