using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates a body based on placement of mouse.
/// </summary>

public class CreateBody : MonoBehaviour
{

    [SerializeField] private GameObject _planet;
    [SerializeField] private GameObject _star;

    private void Start()
    {
    }

    // TODO Make function to disable button

    /// <summary>
    /// Places a planet object using a raycast to determine plane of placement
    /// </summary>
    public void CreatePlanet()
    {
        // Place planet based on raycast
        GetComponent<PlayerStateController>().Placing = true;

        // Launch ray
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = Vector3.zero;

        // On hit, mark and place
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            coordinates = hit.point;
        GameObject planet = (GameObject) Instantiate(_planet, coordinates, transform.rotation);
        planet.name = _planet.name;

        Body p = planet.GetComponent<Body>();
        //p.Mass = 3.003E-6;
        //p.Mass = 1.0E6;
        p.Mass = 5.97E24;
    }

    /// <summary>
    /// Places a star object using a raycast to determine plane of placement
    /// </summary>
    public void CreateStar()
    {
        // Place star based on raycast
        GetComponent<PlayerStateController>().Placing = true;

        // Launch ray
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = Vector3.zero;

        // On hit, mark and place
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            coordinates = hit.point;
        GameObject star = (GameObject)Instantiate(_star, coordinates, transform.rotation);
        star.name = _star.name;

        Body s = star.GetComponent<Body>();
        s.Mass = 1;
        //s.Mass = 1.989E30;
    }
    
    /// <summary>
    /// Instantiates a premade GameObject
    /// </summary>
    /// <param name="obj">
    /// The object to be instantiated
    /// </param>
    public void CreateGeneral(GameObject obj)
    {
        // Place planet based on raycast
        GetComponent<PlayerStateController>().Placing = true;

        // Launch ray
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 coordinates = Vector3.zero;

        // On hit, mark and place
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            coordinates = hit.point;
        GameObject newObj = Instantiate(obj, coordinates, transform.rotation);
        newObj.name = obj.name;
    }
}
