using UnityEngine;
using EventSys;

public class AsteroidsSpawn : MonoBehaviour {
	public bool ParentToThis = false;
	public int asteroidsMax = 29;
	private Transform tx;

	private GameObject[] asteroids;
	public GameObject[] prefabs = new GameObject[2];

	public float asteroidSize = 1;
	public float asteroidDistance = 10;
	public float asteroidClipDistance = 1;
	private float asteroidDistanceSqr;
	private float asteroidClipDistanceSqr;
	public float asteroidRadius = 1;

	private int _updateEvery = 7;
	private int _counter = 0;

	GameObject root;

    void Awake() {
		EventManager.Subscribe<Event_Jump>(this, OnJump);
    }

	private void OnDestroy() {
		EventManager.Unsubscribe<Event_Jump>(OnJump);
	}

	void OnJump(Event_Jump e) {
		enabled = false;
	}

	void Start () {
		asteroids = new GameObject[asteroidsMax];
		root = new GameObject();
		root.transform.position = Vector2.zero;
		root.name = "Asteroid Field";
		if ( ParentToThis ) {
			root.transform.SetParent(transform, false);
		}

		tx = transform;
		asteroidDistanceSqr = asteroidDistance * asteroidDistance;
		asteroidClipDistanceSqr = asteroidClipDistance * asteroidClipDistance;
	}

	private void CreateAsteroids()
	{
		for (int i = 0; i < asteroidsMax; i++)
		{
			asteroids[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)]) as GameObject;
			Vector2 pos = FindSpawnPos();	
			asteroids[i].transform.position = pos;
			asteroids[i].transform.parent = root.transform;
						Vector3 tmpScale = asteroids[i].transform.localScale;
			tmpScale *= Random.Range(0.85f, 1.2f);
			asteroids[i].transform.localScale = tmpScale;
			asteroids[i].GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-100f, 100f);
		}
	}
	
	void Update () {
		_counter++;
		if (asteroids[0] == null) {
			CreateAsteroids();
		}

		if (_counter == _updateEvery) {
			_counter = 0;

			for (int i = 0; i < asteroidsMax; i++) {
				if ((asteroids[i].transform.position - tx.position).sqrMagnitude > asteroidDistanceSqr) {
					asteroids[i].transform.position = FindRespawnPos();
					asteroids[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}
			}
		}
	}

	Vector2 FindSpawnPos() {
		Vector2 pos = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
		while (Physics2D.OverlapCircle(pos, asteroidRadius)) {
			pos = Random.insideUnitCircle * asteroidDistance + (Vector2)tx.position;
		}
		return pos;
	}

	Vector2 FindRespawnPos() {
		Vector2 pos = Random.insideUnitCircle.normalized * Random.Range(asteroidClipDistance, asteroidDistance) + (Vector2)tx.position;
		while (Physics2D.OverlapCircle(pos, asteroidRadius)) {
			pos = Random.insideUnitCircle.normalized * Random.Range(asteroidClipDistance, asteroidDistance) + (Vector2)tx.position;
		}
		return pos;
	}
}
