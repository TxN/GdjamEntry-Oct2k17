using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
    public AnimationCurve lerpCoef;
    public Transform player;

    public float initDelta = 10f;

    float initZ = 0;
    float moveError;
	void Start () 
    {
        initZ = transform.position.z;
	}
	
	void LateUpdate ()
    {
        moveError = Vector3.Distance(transform.position, player.position) - initDelta;
        float cLerp = lerpCoef.Evaluate(moveError);
        Vector3 newPos = Vector3.Lerp(transform.position, player.position, cLerp);
        newPos.z = initZ;
        transform.position = newPos;
        //Debug.Log(moveError);
	}



}
