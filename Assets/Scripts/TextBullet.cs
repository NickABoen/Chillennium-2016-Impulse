using UnityEngine;
using System.Collections;

public class TextBullet : MonoBehaviour {

    public float speed;
    public float angle_off_zero;

    public Vector2 direction;

	// Use this for initialization
	void Start () {
        Vector2 position = gameObject.transform.position;
        if (direction == Vector2.zero)
        {
            direction = -1 * position;
            direction = Quaternion.Euler(0, 0, angle_off_zero) * direction;
            direction.Normalize();
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
	}

    public void RotateHeading(int degrees)
    {
        direction = Quaternion.Euler(0, 0, angle_off_zero) * direction;
    }
}
