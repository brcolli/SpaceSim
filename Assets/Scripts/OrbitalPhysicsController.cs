using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Updates physics for a given object by adding influence of all objects to each other
 * and applying the force. */

public class OrbitalPhysicsController : MonoBehaviour {

    private GameObject _universe;
    private GameObject _center;

    private List<GameObject> _objectPool;
    private bool _placing;

    void Start ()
    {
        _universe = GameObject.FindGameObjectWithTag("Universe");
        _center = _universe.GetComponent<PlayerStateController>().Center;
        _objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;
    }

	void FixedUpdate ()
	{
        _placing = _universe.GetComponent<PlayerStateController>().Placing;
        
        // TODO use Body not GameObject
        // Look for a change in size // TODO: Be aware that this pauses all object force updates while placing
        if (!_placing && _objectPool.Count != 1)
	    {
            BodyPhysics.UpdateForces(_objectPool, gameObject); // Update force for this object
            BodyPhysics.UpdateBodyPhysics(gameObject);
            //gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Body>().Forces);
        }

        // If go too far out, delete
	    if (gameObject.transform.position.sqrMagnitude - _center.transform.position.sqrMagnitude > 1E6)
	    {
	        _objectPool.Remove(gameObject);
	        GameObject.Destroy(gameObject);
	    }
	}
}
