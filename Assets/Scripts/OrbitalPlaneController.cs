using UnityEngine;
using System.Collections;

/* Controller for the invisible plane used to place objects. */

public class OrbitalPlaneController : MonoBehaviour {

	void Start ()
	{

	}

	void Update ()
	{
        // Focus to center of camera
        if (GameObject.FindGameObjectWithTag("Universe").GetComponent<BodyContainer>().ObjectPool.Count < 1)
            transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 20.0f));
    }
}
