using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceParticlesLevel2 : MonoBehaviour {

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
			points[i].size = starSize;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (points == null) CreateStars();

		counter++;

		if (counter == updateEvery) {
			counter = 0;

			for (int i = 0; i < starsMax; i++) {

				if ((points [i].position - tx.position).sqrMagnitude > starDistanceSqr) {
					//points [i].position = Random.insideUnitSphere.normalized * starDistance + tx.position;
					points [i].position = Random.insideUnitCircle.normalized * starDistance + (Vector2)tx.position;
					Vector3 offset = new Vector3 (points [i].position.x, points [i].position.y, zOffset);
					points [i].position = offset;
				}

				//if ((points [i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) {
				if ((points [i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) {
					float percent = (points [i].position - tx.position).sqrMagnitude / starClipDistanceSqr;
					points [i].color = new Color (1, 1, 1, percent);
					points [i].size = percent * starSize;
				}
			}

			pSystem.SetParticles (points, points.Length);
		}
	}
}
