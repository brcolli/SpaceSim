using UnityEngine;
using System.Collections;

public class OrbitalPlaneController : MonoBehaviour {

	void Start ()
	{

	}

	void Update ()
	{
        // Focus to center of camera
        if (GameObject.FindGameObjectWithTag("Universe").GetComponent<BodyContainer>().ObjectPool.Count < 1)
            transform.position =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 20.0f));
    }
}
