using UnityEngine;
using System.Collections;

/* Child class for all planets. Main difference between stars is composition. */

public class Planet : Body {

    /*public Planet(float mass, float radius, float major, float eccentricity, Vector3? velocity = null,
                  Vector3? position = null, float? theta = null, float? z = null) : base(mass, radius,
                  major, eccentricity, velocity, position, theta, z)
    {
        // TODO: Implement density, temperature, other planet characteristics
    }*/

    public Planet(double m, double r) : base(m, r)
    {
        
    }

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().mass = 3.003e-6f; // Mass of the Earth = 5.97e24 = 3.003e-6
    }
}
