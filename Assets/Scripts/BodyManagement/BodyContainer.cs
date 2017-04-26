using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Holds all objects in the system.
/// </summary>

public class BodyContainer : MonoBehaviour
{
    
    private bool physicsEnabled_;

    private void Start()
    {
        // Physics enabled by default
        physicsEnabled_ = true;

        ObjectPool = new List<GameObject>();
        ObjectPool.Capacity = 10; // 8 planets, Sun, and Moon

        // If items pre-placed
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        if (planets.Length != 0)
            ObjectPool.AddRange(planets);
        if (stars.Length != 0)
            ObjectPool.AddRange(stars);
    }

    /// <summary>
    /// Changes all system object's masses
    /// </summary>
    /// <param name="coefficient">
    /// The coefficient to multiply by
    /// </param>
    public void ChangeAllMass(double coefficient)
    {
        foreach (GameObject obj in ObjectPool)
        {
            Body b = obj.GetComponent<Body>();
            b.Mass *= coefficient;
        }
    }

    /// <summary>
    /// Changes all system object's radii
    /// </summary>
    /// <param name="coefficient">
    /// The coefficient to multiply by
    /// </param>
    public void ChangeAllRadius(double coefficient)
    {
        foreach (GameObject obj in ObjectPool)
        {
            Body b = obj.GetComponent<Body>();
            b.Radius *= coefficient;
        }
    }

    /// <summary>
    /// Disables or enables physics for all objects
    /// </summary>
    public void ChangePhysicsState()
    {
        physicsEnabled_ = !physicsEnabled_; // Switch

        // Set active physics manager for all objects
        foreach (GameObject obj in ObjectPool)
            obj.GetComponent<OrbitalPhysicsController>().enabled = physicsEnabled_;
    }

    /* Getters and setters */

    public List<GameObject> ObjectPool { get; set; }
}
