using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/* Instantiates a body based on placement of mouse. */

public class CreateBody : MonoBehaviour
{

    [SerializeField] private GameObject _planet;
    [SerializeField] private GameObject _star;
    //private List<GameObject> _objectPool;

    private void Start()
    {

    }

    // TODO Make function to disable button

    public void CreatePlanet()
    {
        // Place planet based on raycast
        GetComponent<PlayerStateController>().Placing = true;
        RaycastHit hit;
        //LayerMask layerMask = LayerMask.NameToLayer("Orbital Objects"); // To ignore all but orbital plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = Vector3.zero;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            coordinates = hit.point;
        GameObject planet = (GameObject) Instantiate(_planet, coordinates, transform.rotation);
    }

    public void CreateStar()
    {
        // Place star based on raycast
        GetComponent<PlayerStateController>().Placing = true;
        RaycastHit hit;
        //LayerMask layerMask = LayerMask.NameToLayer("Orbital Objects"); // To ignore all but orbital plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = Vector3.zero;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            coordinates = hit.point;
        GameObject star = (GameObject)Instantiate(_star, coordinates, transform.rotation);
    }
}
