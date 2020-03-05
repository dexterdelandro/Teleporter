using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum Direction { left, right };
	float dx = 0.1f;
	SpriteRenderer renderer;
	public Direction facing;
    // Start is called before the first frame update
    void Start()
    {
		renderer = GetComponent<SpriteRenderer>();
		facing = Direction.right;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.LeftArrow)){
			if (facing != Direction.left)
			{
				facing = Direction.left;
				renderer.flipX = true;
			}
			else {
				//renderer.flipX = false;
			}
			transform.position = new Vector3(transform.position.x - dx, transform.position.y, transform.position.z);			
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			if (facing != Direction.right)
			{
				facing = Direction.right;
				renderer.flipX = false;
			}
			else {
				//renderer.flipX = false;
			}
			transform.position = new Vector3(transform.position.x + dx, transform.position.y, transform.position.z);

		}

		if (transform.position.x+ renderer.bounds.extents.x / 2 > Manager.instance.totalCamWidth)
		{
			transform.position =new Vector3(Manager.instance.totalCamWidth-renderer.bounds.extents.x/2, transform.position.y, transform.position.x);
		}
		if (transform.position.x- renderer.bounds.extents.x / 2 < -Manager.instance.totalCamWidth) {
			transform.position = new Vector3(-Manager.instance.totalCamWidth+renderer.bounds.extents.x / 2, transform.position.y, transform.position.x);

		}
	}
}
