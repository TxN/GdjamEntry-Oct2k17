﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EventSys;

public class ShipControl : MonoBehaviour {
    public float RotateMomentum = 0.5f;

    public ParticleSystem MoveForwardParticles;
    public ParticleSystem MoveBackwardsParticles;

    SpaceGameState _state = null;
    AudioSource jet_engine = null;

    private Rigidbody2D body;
	void Start () {
        body = GetComponent<Rigidbody2D>();
        _state = SpaceGameState.Instance;
        jet_engine = GetComponent<AudioSource>(); 
	}
	

	void Update () {
        if (_state.LockState == ControlsState.Unlocked) {
			float rotation = Input.GetAxis("Horizontal");

			if ( Mathf.Abs(rotation) > 0 ) {
				body.AddTorque(-RotateMomentum * rotation);
				EventManager.Fire<Event_ShipRotate>(new Event_ShipRotate());
			}

            if ( (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _state.CanBurn) {
                body.AddForce(transform.TransformDirection(Vector2.up), ForceMode2D.Impulse);
                EventManager.Fire<Event_Accelerate>(new Event_Accelerate { direction = 1 });
                _state.BurnFuel();
                MoveForwardParticles.Play();
                jet_engine.Play();
            }

            if ( (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ) && _state.CanBurn) {
                body.AddForce(transform.TransformDirection(-Vector2.up), ForceMode2D.Impulse);
                EventManager.Fire<Event_Accelerate>(new Event_Accelerate { direction = -1 });
                _state.BurnFuel();
                MoveBackwardsParticles.Play();
                jet_engine.Play();
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
