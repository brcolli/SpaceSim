using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controller for the invisible plane used to place objects.
/// </summary>

public class OrbitalPlaneController : MonoBehaviour
{

    private List<GameObject> _objectPool;

	void Start ()
	{
	    _objectPool = GameObject.FindGameObjectWithTag("Universe").GetComponent<BodyContainer>().ObjectPool;

	}

	void Update ()
	{
        // Focus to center of camera
        if (_objectPool.Count < 1)
            transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 20.0f));
    }
}
