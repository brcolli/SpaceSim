using UnityEngine;
using System.Collections;

/// <summary>
/// Controls state that the player is in; ie placing an object, in menu, etc.
/// </summary>

public class PlayerStateController : MonoBehaviour
{

    public GameObject Center; // Center of the Universe

    void Awake()
    {
        Center = gameObject;
        Placing = false;

        // If items pre-placed
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        if (planets.Length != 0)
            Center = planets[0];
        if (stars.Length != 0)
            Center = stars[0];
    }

    /* Getters and setters */

    public bool Placing { get; set; }
}
