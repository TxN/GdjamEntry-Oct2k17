using UnityEngine;
using System.Collections;

public class OxygenTank : MonoBehaviour {

    public float thrustTime = 2f;
    public float forceMul = 4f;

    bool thrusting = false;

    public GameObject connectedBody;

    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        if (thrusting)
        {
            body.AddForce(transform.TransformDirection(-Vector2.up)*forceMul);
        }
	}

     public void StartThrust()
    {
        Debug.Log("Start thrust");
        Invoke("StopThrust", thrustTime);
        thrusting = true;
    }

    void StopThrust()
    {
        thrusting = false;
        FixedJoint2D joint = GetComponent<FixedJoint2D>();
        if (joint != null)
        {
            Destroy(joint);
        }
        Debug.Log("Stop thrust");
        connectedBody.GetComponent<ShipControl>().RemoveTank(gameObject);
        Destroy(this.gameObject, 2f);
    }
}
