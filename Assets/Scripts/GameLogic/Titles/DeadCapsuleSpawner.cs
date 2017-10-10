using UnityEngine;

public class DeadCapsuleSpawner : MonoBehaviour {
	public GameObject CapsuleFab = null;
	public int Count = 5;
	public float Radius = 5f;
	public Vector3 VelocityDir;
	public float AngVel = 120f;

	private void Start() {
		for (int i = 0; i < Count; i++) {
			GameObject caps = Instantiate(CapsuleFab, transform);
			caps.SetActive(true);
			Rigidbody2D body = caps.GetComponent<Rigidbody2D>();
			caps.transform.position = Random.insideUnitSphere * Radius + transform.position;
			body.angularVelocity = Random.Range(-AngVel, AngVel);
			body.velocity = VelocityDir * Random.Range(0.25f, 1f);
		}
	}
}
