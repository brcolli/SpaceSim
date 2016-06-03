using UnityEngine;
using System.Collections;

public class Star : Body {

    public Star(float mass, float radius, float major, float eccentricity, Vector3? velocity = null,
                  Vector3? position = null, float? theta = null, float? z = null) : base(mass, radius,
                  major, eccentricity, velocity, position, theta, z)
    {
        // TODO: Implement density, temperature, luminosity, other star characteristics
    }

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().mass = 1; // Mass of the Sun = 1.989e30 = 1 SMU
    }
}
