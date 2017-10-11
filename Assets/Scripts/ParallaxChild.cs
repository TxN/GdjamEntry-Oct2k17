using UnityEngine;

public class ParallaxChild : MonoBehaviour {
	public Transform Target = null;
	public float ParallaxCoef = 1;

	Vector3 _startPos;

	private void Start() {
		_startPos = transform.position;
	}

	private void Update() {
		Vector3 newPos = Target.position * ParallaxCoef;
		newPos.z = _startPos.z;
		transform.position = newPos;
	}
}
