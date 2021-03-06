﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Updates physics for a given object by adding influence of all objects to each other
/// and applying the force.
/// </summary>

public class OrbitalPhysicsController : MonoBehaviour {

    /* Attributes */

    private GameObject _universe;
    private GameObject _center;

    private List<GameObject> _objectPool;
    private bool _placing;

    void Start ()
    {
        _universe = GameObject.FindGameObjectWithTag("Universe");
        _objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;
    }

	void FixedUpdate ()
	{
        _placing = _universe.GetComponent<PlayerStateController>().Placing;
        _center = _universe.GetComponent<PlayerStateController>().Center; // TODO call event

        // TODO use Body not GameObject
        // Look for a change in size // TODO: Be aware that this pauses all object force updates while placing
	    if (_objectPool != null)
	    {
	        if (!_placing && _objectPool.Count != 1)
	        {
                double G = _universe.GetComponent<TimeManager>().GetG();
	            BodyPhysics.UpdateForces(_objectPool, gameObject, G); // Update force for this object
	            BodyPhysics.UpdateBodyPhysics(gameObject);
	        }

	        // If go too far out, delete (distance used is the approx. radius
            // of the solar system in AU*1.5.
            // TODO be aware that this breaks many scenes with far apart objects
	        if (Mathf.Abs(gameObject.transform.position.sqrMagnitude - _center.transform.position.sqrMagnitude) > 1E9)
	        {
	            _objectPool.Remove(gameObject);
	            GameObject.Destroy(gameObject);
	        }
	    }
	}
}
