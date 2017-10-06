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

	private int updateEvery = 5;
	private int counter = 0;

	GameObject root;

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
			asteroids[i].transform.position = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
			asteroids[i].transform.parent = root.transform;
			//asteroids[i].GetComponent<Rigidbody2D>().angularVelocity = Random.insideUnitCircle * 2f;
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
