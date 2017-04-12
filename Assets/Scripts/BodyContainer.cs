using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Holds all objects in the system.
/// </summary>

public class BodyContainer : MonoBehaviour
{
    
    private void Start()
    {
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

    /* Getters and setters */

    public List<GameObject> ObjectPool { get; set; }
}
