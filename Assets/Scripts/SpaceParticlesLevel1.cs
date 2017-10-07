using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceParticlesLevel1 : MonoBehaviour {
	private Transform tx;
	private ParticleSystem.Particle[] points;

	public int starsMax = 100;
	public float starSize = 1;
	public float starDistance = 10;
	public float starClipDistance = 1;
	private float starDistanceSqr;
	private float starClipDistanceSqr;
	private ParticleSystem pSystem;

	private int updateEvery = 5;
	private int counter = 0;
	public float zOffset = 10;
	public bool randomizeSize = false;
	public float lowRnd = 0;
	public float hiRnd = 10;
	//public float parallaxRate = 0.5;
	// Use this for initialization
	void Start () {
		tx = transform;
		starDistanceSqr = starDistance * starDistance;
		starClipDistanceSqr = starClipDistance * starClipDistance;
		pSystem = GetComponent<ParticleSystem>();
	}
	private void CreateStars()
	{
		points = new ParticleSystem.Particle[starsMax];

		for (int i = 0; i < starsMax; i++)
		{
			//points[i].position = Random.insideUnitSphere * starDistance + tx.position;
			points[i].position = Random.insideUnitCircle * starDistance + (Vector2)tx.position;
			Vector3 offset = new Vector3 (points [i].position.x, points [i].position.y, zOffset);
			points [i].position = offset;
			points[i].color = new Color(1, 1, 1, 1);
			if (randomizeSize) {
				points [i].size = Random.Range (lowRnd, hiRnd);
			}
			else points[i].size = starSize;
		}
	}
	// Update is called once per frame
	void Update () {
		if (points == null) CreateStars();
	}
}
