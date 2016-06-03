using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbitalPhysicsController : MonoBehaviour {

    private GameObject _universe;

    private List<GameObject> _objectPool;
    private int c;

    void Start ()
    {
        _universe = GameObject.FindGameObjectWithTag("Universe");
        _objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;
        c = _objectPool.Count - 1;
        c++; // To correct for goofiness
    }

	void Update ()
	{
        // Look for a change in size
	    if (c != _objectPool.Count && !_universe.GetComponent<PlayerStateController>().Placing)
	    {
	        c = _objectPool.Count - 1;
	        c++;

            Debug.Log("Updating forces");
	        GameObject body = gameObject;
            BodyPhysics.UpdateForces(ref _objectPool, ref body); // Update force for this object
	        gameObject.GetComponent<Body>().Forces = body.GetComponent<Body>().Forces; // Update reference
            Debug.Log("Final forces: " + body.GetComponent<Body>().Forces);
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Body>().Forces);
	    }
	}
}
