using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Updates physics for a given object by adding influence of all objects to each other
 * and applying the force. */

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

	void FixedUpdate ()
	{
        // Look for a change in size
	    if (c != _objectPool.Count && !_universe.GetComponent<PlayerStateController>().Placing)
	    {
	        c = _objectPool.Count - 1;
	        c++;

            Debug.Log("Updating forces");
	        GameObject body = gameObject;
            BodyPhysics.UpdateForces(ref _objectPool, ref body); // Update force for this object
            Debug.Log("Final forces: " + body.GetComponent<Body>().Forces);
            //gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Body>().Forces);
	    }
	    BodyPhysics.UpdateBodyPhysics(gameObject);
	}
}
