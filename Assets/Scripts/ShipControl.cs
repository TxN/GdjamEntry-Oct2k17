using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventSys;

public class ShipControl : MonoBehaviour {
    public float RotateMomentum = 0.5f;

    public ParticleSystem MoveForwardParticles;
    public ParticleSystem MoveBackwardsParticles;

    SpaceGameState _state = null;

    private Rigidbody2D body;
	void Start () {
        body = GetComponent<Rigidbody2D>();
        _state = SpaceGameState.Instance;
	}
	

	void Update () {
        if (_state.LockState == ControlsState.Unlocked) {
            if (Input.GetKey(KeyCode.A)) {
                body.AddTorque(RotateMomentum);
            }
            if (Input.GetKey(KeyCode.D)) {
                body.AddTorque(-RotateMomentum);
            }

            if (Input.GetKeyDown(KeyCode.W) && _state.CanBurn) {
                body.AddForce(transform.TransformDirection(Vector2.up), ForceMode2D.Impulse);
                EventManager.Fire<Event_Accelerate>(new Event_Accelerate { direction = 1 });
                _state.BurnFuel();
                MoveForwardParticles.Play();
            }

            if (Input.GetKeyDown(KeyCode.S) && _state.CanBurn) {
                body.AddForce(transform.TransformDirection(-Vector2.up), ForceMode2D.Impulse);
                EventManager.Fire<Event_Accelerate>(new Event_Accelerate { direction = -1 });
                _state.BurnFuel();
                MoveBackwardsParticles.Play();
            }
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        var caps = coll.gameObject.GetComponent<SafeCapsule>();

        if ( caps ) {
            _state.InteractWithCapsule(caps);
        }
    }
}
