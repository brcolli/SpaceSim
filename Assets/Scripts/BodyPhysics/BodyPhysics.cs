using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Self-written physics system. Used for adding forces, velocities,
/// acceleration, and position translations.
/// </summary>

public class BodyPhysics : MonoBehaviour
{
    
    /// <summary>
    /// Apply forces to all bodies in a list
    /// </summary>
    /// <param name="system">
    /// The collection of all Body objects
    /// </param>
    /// <param name="body">
    /// The Body object to update forces on
    /// </param>
    public static void UpdateForces(List<GameObject> system, GameObject body, double G)
    {
        for (int i = 0; i < system.Count; i++)
        {
            if (system[i] != body)
            {
                GameObject otherBody = system[i];
                ApplyForces(body, otherBody, G);
            }
        }
    }

    /// <summary>
    /// A wrapper for applying forces to a GameObject
    /// </summary>
    /// <param name="objectA">
    /// The object to update forces on
    /// </param>
    /// <param name="objectB">
    /// The object applying the force
    /// </param>
    private static void ApplyForces(GameObject objectA, GameObject objectB, double G)
    {
        Body bodyOne = objectA.GetComponent<Body>();
        Body bodyTwo = objectB.GetComponent<Body>();

        // Vector of distances
        Vector3 distances = new Vector3(objectB.transform.position[0] - objectA.transform.position[0],
                                        objectB.transform.position[1] - objectA.transform.position[1],
                                        objectB.transform.position[2] - objectA.transform.position[2]);
        double totalDistance = Sqrt(Mathf.Pow(distances[0], 2) + Mathf.Pow(distances[1], 2) + Mathf.Pow(distances[2], 2));

        // Calculate force
        double totalForce = (G * bodyOne.Mass * bodyTwo.Mass) / (totalDistance * totalDistance);
        double inverseDistance = 1.0 / totalDistance;

        Vector3 temp = bodyOne.Forces;
        Vector3 dComponents = distances; // Distance components
        Vector3 fComponents = new Vector3(); // Force components

        // Sum components
        for (int i = 0; i < 3; i++)
        {
            // TODO use max(distance) to set maximum distance
            dComponents[i] *= (float)inverseDistance;
            fComponents[i] = dComponents[i] * (float)totalForce;

            if (Math.Abs(distances[i]) < 0.001) // If less than tolerance, set to zero; TODO set to r1 + r2
            {
                // temp[i] += 0;
            }
            // If on positive side of body, subtract
            else if (distances[i] > 0)
            {
                temp[i] -= fComponents[i];
            }
            // If on negative side of body, add
            else if (distances[i] < 0)
            {
                temp[i] += fComponents[i];
            }
        }
        
        bodyOne.Forces = temp;
        bodyOne.Acceleration = bodyOne.Forces / (float)bodyOne.Mass;
    }

    /// <summary>
    /// Pseudo-updates velocity, position, based on acceleration and returns true if object moved, else false
    /// </summary>
    /// <param name="obj">
    /// The object that is being updated
    /// </param>
    /// <returns>
    /// Whether or not it will move
    /// </returns>
    public static bool TestMovement(GameObject obj)
    {
        Vector3 tempPosition = obj.transform.position;
        Vector3 newPosition = tempPosition;
        Body objBody = obj.GetComponent<Body>();
        Body b = new Body(objBody.Mass, objBody.Radius);
        b.Velocity += b.Acceleration*(float) (Time.deltaTime);
        newPosition += b.Velocity*(float) (Time.deltaTime);
        return tempPosition != newPosition;
    }

    /// <summary>
    /// Updates velocity, position, based on acceleration and returns true if object moved, else false
    /// </summary>
    /// <param name="obj">
    /// The object that is being updated
    /// </param>
    public static void UpdateBodyPhysics(GameObject obj)
    {
        Body b = obj.GetComponent<Body>();
        b.Velocity += b.Acceleration * (float)(Time.fixedDeltaTime);
        obj.transform.position += b.Velocity * (float)(Time.fixedDeltaTime);
    }

    /// <summary>
    /// Inverse square root algorithm
    /// </summary>
    /// <param name="value">The base</param>
    /// <returns>T
    /// he square root of value
    /// </returns>
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
