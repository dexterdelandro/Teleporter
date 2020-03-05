using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	public enum Direction { left, right };
	public Direction direction;
	public float dx = 0.3f;
	SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
		renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (direction == Direction.left)
		{
			renderer.flipX = true;
			transform.position = new Vector3(transform.position.x - dx, transform.position.y, transform.position.z);
		}
		else {
			renderer.flipX = false;
			transform.position = new Vector3(transform.position.x + dx, transform.position.y, transform.position.z);
		}

		//delete arrow if it goes off screen
		if (Mathf.Abs(transform.position.x) >= Manager.instance.totalCamWidth)
		{
			Destroy(gameObject);
		}
	}
}
