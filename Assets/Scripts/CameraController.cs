using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Camera controller. Positions camera based on first object placed or object that absorbs the latter.
/// Rotates around single point with right click, zoom in and out with wheel.
/// </summary>

public class CameraController : MonoBehaviour
{

    private GameObject _universe;
    private GameObject _center;

    // public Transform target;
    public float Distance = 5.0f;
    public float XSpeed = 120.0f;
    public float YSpeed = 120.0f;
    public float YMinLimit = -20f;
    public float YMaxLimit = 80f;

    // Zoom min and max
    public float DistanceMin = .5f;
    public float DistanceMax = 15f;

    public float SmoothTime = 2f;
    float _rotationYAxis = 0.0f;
    float _rotationXAxis = 0.0f;

    // Base rotation speed (for action shots?)
    float _velocityX = 0.0f;
    float _velocityY = 0.0f;

    private bool _focused = false; // Focused flag

    void Start()
    {
        //transform.eulerAngles = new Vector3(10, transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 angles = transform.eulerAngles;
        _rotationYAxis = angles.y;
        _rotationXAxis = angles.x;

        _universe = GameObject.FindGameObjectWithTag("Universe");
        _center = _universe.GetComponent<PlayerStateController>().Center;
    }

    void Update()
    {
        List<GameObject> objectPool = _universe.GetComponent<BodyContainer>().ObjectPool;

        // If planet has been placed, change center focus
        if (objectPool.Count == 1 && !_focused)
        {
            _center = _universe.GetComponent<PlayerStateController>().Center;
            _focused = true;
        }

        // For edge case when all objects deleted
        if (objectPool.Count == 0 && _focused)
        {
            _focused = false;
        }
    }

    void LateUpdate()
    {

        if (_center)
        {
            // Get state
            bool placing = _universe.GetComponent<PlayerStateController>().Placing;

            // Move camera with right click
            if (Input.GetMouseButton(1) && !placing)
            {
                _velocityX += XSpeed*Input.GetAxis("Mouse X")*Distance*0.03f;
                _velocityY += YSpeed*Input.GetAxis("Mouse Y")*0.03f;
            }
            float TOLERANCE = 0.001f;
            if (Input.GetMouseButtonDown(1) && !placing &&
                (Math.Abs(_velocityX) > TOLERANCE && Math.Abs(_velocityY) > TOLERANCE))
            {
                _velocityY = 0.0f;
                _velocityX = 0.0f;
            }

            _rotationYAxis += _velocityX;
            _rotationXAxis -= _velocityY;
            _rotationXAxis = ClampAngle(_rotationXAxis, YMinLimit, YMaxLimit);
            Quaternion toRotation = Quaternion.Euler(_rotationXAxis, _rotationYAxis, 0);
            Quaternion rotation = toRotation;

            Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel")*5, DistanceMin, DistanceMax);
            RaycastHit hit;
            if (Physics.Linecast(_center.transform.position, transform.position, out hit))
            {
                Distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance - 5f);
            Vector3 position = rotation*negDistance + _center.transform.position;

            transform.rotation = rotation;
            transform.position = position;
            _velocityX = Mathf.Lerp(_velocityX, 0, Time.deltaTime*SmoothTime);
            _velocityY = Mathf.Lerp(_velocityY, 0, Time.deltaTime*SmoothTime);
        }
    }

    /// <summary>
    /// Constrains a given angle based on bounds
    /// </summary>
    /// <param name="angle">
    /// The angle to clamp
    /// </param>
    /// <param name="min">
    /// Minimum bound
    /// </param>
    /// <param name="max">
    /// Maximum bound
    /// </param>
    /// <returns>
    /// Clamped angle
    /// </returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}