using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public float dx = 0;
	private bool onScreen = false;
	SpriteRenderer renderer;
	BoxCollider2D collider;

	void Start()
    {
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		dx = Random.Range(0.02f, 0.1f+Manager.instance.score/5000);
		if (transform.position.x >= 0) dx *= -1;
    }

    // Update is called once per frame
    void Update()
    {
		//makes sure enemies bounce off the walls
		//since they spawn off screen, they must first be on screen before they can bounce
		if (!onScreen && Mathf.Abs(transform.position.x)<=Manager.instance.totalCamWidth)onScreen = true;
		if (onScreen && Mathf.Abs(transform.position.x) >= Manager.instance.totalCamWidth) dx *= -1;

		//makes enemies bounce off walls


		if (dx >= 0)
		{
			renderer.flipX = false;
		}
		else {
			renderer.flipX = true;
		}

		if (Manager.instance.gameOver) Destroy(gameObject);



		//moves the enemy
		transform.position = new Vector3(transform.position.x + dx, transform.position.y, transform.position.z);
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		//if zombie collides with arrow, delete arrow and zombie, increase score
		if (col.gameObject.layer == 9)
		{
			Manager.instance.numEnemies--;
			Manager.instance.score++;
			Destroy(col.gameObject);
			Destroy(gameObject);
		}

		if (col.gameObject.layer == 10) {
			Destroy(col.gameObject);
			Manager.instance.gameOver = true;
		}


	}
}
