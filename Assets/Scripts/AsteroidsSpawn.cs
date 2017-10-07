using System.Collections;
using UnityEngine;

public class AsteroidsSpawn : MonoBehaviour {

	public int asteroidsMax = 29;
	private Transform tx;
	//private GameObject[] asteroids = new GameObject[asteroidsMax];
	private GameObject[] asteroids;
	public GameObject[] prefabs = new GameObject[2];

	public float asteroidSize = 1;
	public float asteroidDistance = 10;
	public float asteroidClipDistance = 1;
	private float asteroidDistanceSqr;
	private float asteroidClipDistanceSqr;
	public float asteroidRadius = 1;

	private int updateEvery = 5;
	private int counter = 0;

	GameObject root;

    public static AsteroidsSpawn Instance = null;

    void Awake() {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		asteroids = new GameObject[asteroidsMax];
		root = new GameObject();
		root.transform.position = Vector2.zero;
		root.name = "Asteroid Field";

		tx = transform;
		asteroidDistanceSqr = asteroidDistance * asteroidDistance;
		asteroidClipDistanceSqr = asteroidClipDistance * asteroidClipDistance;
	}

	private void CreateAsteroids()
	{
		for (int i = 0; i < asteroidsMax; i++)
		{
			asteroids[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)]) as GameObject;
			Vector2 pos = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
			while (Physics2D.OverlapCircle(pos, asteroidRadius)) {
				pos = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
			}
		
			//asteroids[i].transform.position = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
			//asteroids[i].transform.position = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
			asteroids[i].transform.position = pos;
			asteroids[i].transform.parent = root.transform;
			//asteroids[i].GetComponent<Rigidbody2D>().angularVelocity = Random.insideUnitCircle * 2f;
			asteroids[i].GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-100f, 100f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
		if (asteroids[0] == null) CreateAsteroids();

		if (counter == updateEvery)
		{
			counter = 0;

			for (int i = 0; i < asteroidsMax; i++)
			{
				if ((asteroids[i].transform.position - tx.position).sqrMagnitude > asteroidDistanceSqr)
				{
					asteroids[i].transform.position = Random.insideUnitCircle.normalized * asteroidDistance + (Vector2)tx.position;
					asteroids[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}
			}
		}
	}
}
