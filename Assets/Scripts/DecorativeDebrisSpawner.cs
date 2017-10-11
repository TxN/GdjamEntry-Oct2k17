using UnityEngine;
using EventSys;

public class DecorativeDebrisSpawner : MonoBehaviour {
	public bool ParentToThis = false;
	public int asteroidsMax = 29;

	private GameObject[] asteroids;
	public GameObject[] prefabs = new GameObject[2];

	public float asteroidSize = 1;
	public float asteroidDistance = 10;
	public float asteroidClipDistance = 1;
	private float asteroidDistanceSqr;
	private float asteroidClipDistanceSqr;
	public float asteroidRadius = 1;

	private int _updateEvery = 10;
	private int _counter = 0;

	GameObject _root;

	public Transform Target = null;

	void Awake() {
		EventManager.Subscribe<Event_Jump>(this, OnJump);
	}

	private void OnDestroy() {
		EventManager.Unsubscribe<Event_Jump>(OnJump);
	}

	void OnJump(Event_Jump e) {
		this.enabled = false;
	}

	void Start() {
		asteroids = new GameObject[asteroidsMax];
		_root = new GameObject();
		_root.transform.position = Vector2.zero;
		_root.name = "DecorativeDebrisField";
		_root.transform.position = transform.position;
		if (ParentToThis) {
			_root.transform.SetParent(transform,true);
			_root.transform.position = transform.position;
		}
		asteroidDistanceSqr = asteroidDistance * asteroidDistance;
		asteroidClipDistanceSqr = asteroidClipDistance * asteroidClipDistance;
	}

	private void CreateAsteroids() {
		for (int i = 0; i < asteroidsMax; i++) {
			asteroids[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)], _root.transform) as GameObject;
			//asteroids[i].transform.SetParent(_root.transform,true);
			Vector3 pos = FindSpawnPos();
			asteroids[i].transform.position = pos;	
			Vector3 tmpScale = asteroids[i].transform.localScale;
			tmpScale *= Random.Range(0.85f, 1.2f);
			asteroids[i].transform.localScale = tmpScale;
			var spin = asteroids[i].AddComponent<Spinner>();
			spin.EulersPerSecond = Random.insideUnitSphere * 45f;
		}
	}

	void Update() {
		_counter++;
		if (asteroids[0] == null) {
			CreateAsteroids();
		}

		if (_counter == _updateEvery) {
			_counter = 0;
			for (int i = 0; i < asteroidsMax; i++) {
				if ((asteroids[i].transform.position - Target.position).sqrMagnitude > asteroidDistanceSqr) {
					asteroids[i].transform.position = FindRespawnPos();
				}
			}
		}
	}

	Vector3 FindSpawnPos() {
		Vector3 pos = (Vector3)Random.insideUnitCircle * asteroidDistance + _root.transform.position;
		//pos.z = 30 + Random.Range(-3f, 3f);
		return pos;
	}

	Vector3 FindRespawnPos() {
		Vector3 pos = (Vector3)Random.insideUnitCircle.normalized * Random.Range(asteroidClipDistance, asteroidDistance) + _root.transform.position;
	//	pos.z = 30 + Random.Range(-3f, 3f);
		return pos;
	}
}
