using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Self-written physics system. Used for adding forces, velocities,
 * acceleration, and position translations. */

public class BodyPhysics : MonoBehaviour
{

    // Apply forces to all bodies in a list
    public static void UpdateForces(ref List<GameObject> system, ref GameObject body)
    {
        for (int i = 0; i < system.Count; i++)
        {
            if (system[i] != body)
            {
                GameObject otherBody = system[i];
                ApplyForces(ref body, ref otherBody);
            }
        }
    }

    // Update forces on bodyOne applied by bodyTwo
    private static void ApplyForces(ref GameObject objectA, ref GameObject objectB)
    {
        Body bodyOne = objectA.GetComponent<Body>();
        Body bodyTwo = objectB.GetComponent<Body>();

        // Gravitational constant in km^3 / kg * yr^2
        //const double G = 6.6463E-5F;
        // Gravitational constant in km^3 / SMU * yr^2
        const double G = 1.32195e25;
        // Gravitational constant in km^3 / SMU * s^2
        //const double G = 13274722847;
        // Gravitational constant in AU^3 / SMU * yr^2; Freaking big
        //const double G = 4.4257965e58;

        // Vector of distances
        Vector3 distances = new Vector3(bodyOne.Coords[0]-bodyTwo.Coords[0],
                                        bodyOne.Coords[1]-bodyTwo.Coords[1],
                                        bodyOne.Coords[2]-bodyTwo.Coords[2]);
        
        Vector3 temp = bodyOne.Forces;

        for (int i = 0; i < 3; i++)
        {
            if (Math.Abs(distances[i]) < 0.001) // If less than tolerance, set to zero
            {
                temp[i] += 0;
            }
            // If on positive side of body, subtract
            else if (distances[i] > 0)
            {
                temp[i] -= (float)(G * bodyOne.Mass * bodyTwo.Mass) / Mathf.Pow(distances[i], 2);
            }

            // If on negative side of body, add
            else if (distances[i] < 0)
            {
                temp[i] += (float)(bodyOne.Mass * bodyTwo.Mass) / Mathf.Pow(distances[i], 2);
            }
        }
        
        bodyOne.Forces = temp;
        bodyOne.Acceleration = bodyOne.Forces/(float)bodyOne.Mass;
    }

    // Pseudo-updates velocity, position, based on acceleration and returns true if object moved, else false
    public static bool TestMovement(GameObject obj)
    {
        Vector3 tempPosition = obj.transform.position;
        Body objBody = obj.GetComponent<Body>();
        Body b = new Body(objBody.Mass, objBody.Radius);
        b.Velocity += b.Acceleration*(float) (Time.deltaTime);
        b.Coords += b.Velocity*(float) (Time.deltaTime);
        return tempPosition != b.Coords;
    }

    // Updates velocity, position, based on acceleration and returns true if object moved, else false
    public static bool UpdateBodyPhysics(GameObject obj)
    {
        Vector3 tempPosition = obj.transform.position;
        Body b = obj.GetComponent<Body>();
        b.Velocity += b.Acceleration * (float)(Time.deltaTime);
        b.Coords += b.Velocity * (float)(Time.deltaTime);
        return tempPosition != b.Coords;
    }
}
