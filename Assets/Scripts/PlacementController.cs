using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementController : MonoBehaviour {

    [SerializeField] private float Major;
    [SerializeField] private float Eccentricity;
    [SerializeField] private float Theta;

    private GameObject _universe;
    private List<GameObject> _objectPool;
    private bool _stay = false;
    private bool _changeY = true;

    void Start()
    {
        _universe = GameObject.FindGameObjectWithTag("Universe");
        _objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
    }

    void Update()
    {
        // Get state; manage placement
        if (!Input.GetMouseButtonDown(0) && !_stay)
        {
            // Find orbital plane
            RaycastHit hit;
            //LayerMask layerMask = LayerMask.NameToLayer("Orbital Objects"); // To ignore all but orbital plane
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
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;

            _objectPool.Add(gameObject);
            _universe.GetComponent<BodyContainer>().ObjectPool = _objectPool; // Update
            // If first object placed, make center of camera
            //if (_objectPool.Count == 1)
            //{
            _universe.GetComponent<PlayerStateController>().Placing = false;
            _changeY = false;
            if (_objectPool.Count == 1)
                _universe.GetComponent<PlayerStateController>().Center = gameObject;
            //}
        }/*
        if (Input.GetMouseButton(0) && _objectPool.Count > 1 && _changeY && _stay) // Remove Count > 1?
        {
            Vector3 temp = Input.mousePosition;
            temp.z = Camera.main.transform.position.magnitude; // TODO: Change based on camera position. Use Ray?
            temp = Camera.main.ScreenToWorldPoint(temp);
            transform.position = new Vector3(transform.position.x, temp.y, transform.position.z);
        }
        else if (_changeY && _stay) // Left click released
        {
            _universe.GetComponent<PlayerStateController>().Placing = false;
            _changeY = false;
        }*/
        if (_stay)
        {
            // TODO update coords
            gameObject.GetComponent<Body>().Coords = transform.position;
        }
    }

    /* Getters and Setters */

    public float Mass { get; set; }
    public float Radius { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
}
