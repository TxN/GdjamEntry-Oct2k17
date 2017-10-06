using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : MonoBehaviour {
    public float rotateMomentum = 0.5f;

    List<GameObject> connectedTanks = new List<GameObject>();

    private Rigidbody2D body;
	void Start () 
    {
        body = GetComponent<Rigidbody2D>();

	}
	

	void Update ()
    {
	    if (Input.GetKey(KeyCode.A)) {
            body.AddTorque(rotateMomentum);
        }
       if (Input.GetKey(KeyCode.D)) {
            body.AddTorque(-rotateMomentum);
        }

       if (Input.GetKeyDown(KeyCode.W))
       {
           body.AddForce(transform.TransformDirection(Vector2.up), ForceMode2D.Impulse);
       }

       if (Input.GetKeyDown(KeyCode.S))
       {
           body.AddForce(transform.TransformDirection(-             Vector2.up), ForceMode2D.Impulse);
       }

       if (Input.GetKeyDown(KeyCode.Space))
       {
           TurnOnAllTanks();
       }
       

	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<OxygenTank>())
        {
            coll.gameObject.GetComponent<OxygenTank>().connectedBody = this.gameObject;
            FixedJoint2D joint = coll.gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = body;
            connectedTanks.Add(coll.gameObject);
            
        }
    }

    public void RemoveTank(GameObject tank)
    {
        connectedTanks.Remove(tank);
    }

    void TurnOnAllTanks()
    {
        foreach (var tank in connectedTanks)
        {
            tank.GetComponent<OxygenTank>().StartThrust();
        }
    }
}
