using UnityEngine;
using System.Collections;

public class DraggingSystem : MonoBehaviour {

    public GameObject currently_dragging;

	// Use this for initialization
	void Start () {
        currently_dragging = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            currently_dragging = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null)
            {
                Draggable draggable_component = hit.transform.gameObject.GetComponent<Draggable>();
                if(draggable_component != null && draggable_component.enabled)
                    currently_dragging = hit.transform.gameObject;
            }
        }

        if(currently_dragging != null)
        {
            Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPoint.z = currently_dragging.transform.position.z;
            currently_dragging.transform.position = newPoint;
        }
	}
}
