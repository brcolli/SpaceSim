using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used to place the object.
/// </summary>

public class PlacementController : MonoBehaviour {

    private GameObject _universe;
    private List<GameObject> _objectPool;
    private bool _stay = false;

    void Start()
    {
        _universe = GameObject.FindGameObjectWithTag("Universe");
        _objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;
    }

    void Update()
    {
        // Get state; manage placement
        if (!Input.GetMouseButtonDown(0) && !_stay)
        {
            // Find orbital plane
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                transform.position = hit.point;

            // Right click to cancel placement
            if (Input.GetMouseButtonDown(1))
            {
                _universe.GetComponent<PlayerStateController>().Placing = false;
                Destroy(gameObject);
            }
        }
        else if (!_stay)
        {
            // Turn off placing state
            _stay = true;

            _objectPool.Add(gameObject);
            _universe.GetComponent<PlayerStateController>().Placing = false;
            // TODO event listener, assign center
            if (_objectPool.Count == 1)
            {
                _universe.GetComponent<PlayerStateController>().Center = gameObject;
            }
        }
        if (_stay)
        {
            // TODO update coords
            //gameObject.GetComponent<Body>().Coords = transform.position;
        }
    }

    /* Getters and Setters */

    public float Mass { get; set; }
    public float Radius { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
}
