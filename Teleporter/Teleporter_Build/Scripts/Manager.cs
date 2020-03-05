using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{

	public static Manager instance;

	public bool gameStart = false;
	public bool gameOver = false;

	public float score = 0;
	public int numEnemies = 0;
	public Arrow arrowPrefab;
	public Arrow tempArrow;
	public Enemy enemyPrefab;
	public Enemy tempEnemy;

	public Player player;

	public GameObject redGem;
	public GameObject blueGem;
	public GameObject greenGem;

	public List<Enemy> enemies;

	private float shootTimer = 0;
	private float secsPerArrow = 0.65f;
	private float spawnTimer = 0;
	private float secsPerZombie = 1.0f;

	private Vector3 spawnLocation;

	public Camera camera;

	public GameObject instructions;
	public GameObject gameOverText;
	public GameObject background;
	
	public float totalCamWidth;

    // Start is called before the first frame update
    void Start()
    {
		if (instance == null)
		{
			instance = this;
		}
		else {
			Destroy(this);
		}
		gameOverText.transform.localScale = new Vector3(0, 0, 0);
		totalCamWidth = camera.orthographicSize * camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
		if (!gameOver) {
			CheckTeleportation();
			Shoot();
			if (!gameStart) CheckStart();
			if (gameStart)
			{
				SpawnEnemies();
			}
		}
		if(gameOver)EndGame();
	}

	void CheckStart() {
		//when the user presses p to play, hide the instructions
		if (Input.GetKeyDown(KeyCode.P)){
			gameStart = true;
			instructions.transform.localScale = new Vector3(0, 0, 0);
		}
	}

	void CheckTeleportation() {
		if (Input.GetKeyDown(KeyCode.Q)) player.transform.position = new Vector3(redGem.transform.position.x, redGem.transform.position.y, player.transform.position.z);
		if (Input.GetKeyDown(KeyCode.S)) player.transform.position = new Vector3(greenGem.transform.position.x, greenGem.transform.position.y, player.transform.position.z);
		if (Input.GetKeyDown(KeyCode.E)) player.transform.position = new Vector3(blueGem.transform.position.x, blueGem.transform.position.y, player.transform.position.z);
	}
	
	void SpawnEnemies()
	{
		if (spawnTimer < Time.time) {
			tempEnemy = Instantiate(enemyPrefab);
			switch (Random.Range(1, 5)) {
				case 1:
					//top left
					spawnLocation = new Vector3(-10.6f, 1.3f, 7);
					break;
				case 2:
					//top right
					spawnLocation = new Vector3(10.6f, 1.3f, 7);
					break;
				case 3:
					//bottom left
					spawnLocation = new Vector3(-10.6f, -3.1f, 7);
					break;
				case 4:
					//bottom right
					spawnLocation = new Vector3(10.6f, -3.1f, 7);
					break;
			}
			tempEnemy.transform.position = spawnLocation;
			numEnemies++;
			enemies.Add(tempEnemy);
			spawnTimer = Time.time + secsPerZombie;

		}
	}

	void Shoot() {
		if (Input.GetKey(KeyCode.Space) &&shootTimer<Time.time) {
			tempArrow = Instantiate(arrowPrefab);
			tempArrow.direction = (Arrow.Direction)(int)player.facing;
			tempArrow.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
			shootTimer = Time.time + secsPerArrow;
		}
	}

	void EndGame() {
		//if (enemies.Count > 0) {
		//	for (int i = 0; i < enemies.Count; i++) Destroy(enemies[i].gameObject);
		//	enemies.Clear();
		//}
		gameOverText.transform.localScale = new Vector3(1, 1, 1);
		background.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, -7);
			
	}


	void OnGUI() {
		if (gameStart&&!gameOver) {
			GUI.color = Color.white;

			// Increase text size
			GUI.skin.box.fontSize = 20;

			GUI.skin.box.wordWrap = true;

			GUI.Box(new Rect(0, 0, 175, 60), "Score: " + score + "\nEnemies Alive: " + numEnemies);
		}

		if (gameOver) {
			GUI.color = Color.white;

			// Increase text size
			GUI.skin.box.fontSize = 50;

			GUI.skin.box.wordWrap = true;

			GUI.Box(new Rect(Screen.width/2-125, Screen.height/2-30, 250, 60), "Score: " + score );
		}
	}
}
