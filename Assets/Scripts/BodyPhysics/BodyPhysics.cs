using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/* Self-written physics system. Used for adding forces, velocities,
 * acceleration, and position translations. */

public class BodyPhysics : MonoBehaviour
{

    // Apply forces to all bodies in a list
    public static void UpdateForces(List<GameObject> system, GameObject body)
    {
        for (int i = 0; i < system.Count; i++)
        {
            if (system[i] != body)
            {
                GameObject otherBody = system[i];
                ApplyForces(body, otherBody);
            }
        }
    }

    // Update forces on bodyOne applied by bodyTwo
    private static void ApplyForces(GameObject objectA, GameObject objectB)
    {
        Body bodyOne = objectA.GetComponent<Body>();
        Body bodyTwo = objectB.GetComponent<Body>();

        // Gravitational constant in m^3 / kg * s^2
        //const double G = 6.67408E-11;
        // Gravitational constant in km^3 / kg * s^2
        //const double G = 6.67408E-20;
        // Gravitational constant in km^3 / kg * yr^2
        //const double G = 2.105E-5;
        // Gravitational constant in km^3 / SMU * yr^2
        //const double G = 4.186845E25;
        // Gravitational constant in km^3 / SMU * s^2
        //const double G = 1.32764855E10;
        // Gravitational constant in AU^3 / SMU * yr^2
        const double G = 39.4784176044;
        // Gravitational constant in pc / SMU * (km/s)^2; Use with orbital mechanics
        //const double G = 4.302E-3;

        // Vector of distances
        Vector3 distances = new Vector3(bodyTwo.Coords[0]-bodyOne.Coords[0],
                                        bodyTwo.Coords[1]-bodyOne.Coords[1],
                                        bodyTwo.Coords[2]-bodyOne.Coords[2]);
        double totalDistance = Sqrt(Mathf.Pow(distances[0], 2) + Mathf.Pow(distances[1], 2) + Mathf.Pow(distances[2], 2));

        // Calculate force
        double totalForce = (G*bodyOne.Mass*bodyTwo.Mass)/(totalDistance * totalDistance);
        double inverseDistance = 1.0/totalDistance;
        
        Vector3 temp = bodyOne.Forces;
        Vector3 dComponents = distances; // Distance components
        Vector3 fComponents = new Vector3(); // Force components

        // Sum components
        for (int i = 0; i < 3; i++)
        {
            // TODO use max(distance) to set maximum distance
            dComponents[i] *= (float)inverseDistance;
            fComponents[i] = dComponents[i]*(float)totalForce;

            if (Math.Abs(distances[i]) < 0.001) // If less than tolerance, set to zero; TODO set to r1 + r2
            {
                temp[i] += 0;
            }
            // If on positive side of body, subtract
            else if (distances[i] > 0)
            {
                temp[i] += fComponents[i];
            }
            // If on negative side of body, add
            else if (distances[i] < 0)
            {
                temp[i] -= fComponents[i];
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
        b.Velocity += b.Acceleration * (float)(Time.fixedDeltaTime);
        b.Coords += b.Velocity * (float)(Time.fixedDeltaTime);
        obj.transform.position = b.Coords;
        return tempPosition != b.Coords;
    }

    // Inverse square root algorithm
    private static double Sqrt(double value)
    {
        if (Math.Abs(value) < 0.001)
            return 0;

        FloatIntUnion union;
        union.tmp = 0;
        float xhalf = 0.5f*(float) value;
        union.f = (float) value;
        union.tmp = 0x5f375a86 - (union.tmp >> 1);
        union.f = union.f*(1.5f - xhalf*union.f*union.f);
        return union.f*value;
    }

    // Goofiness
    [StructLayout(LayoutKind.Explicit)]
    private struct FloatIntUnion
    {
        [FieldOffset(0)] public float f;
        [FieldOffset(0)] public int tmp;
    }
}
