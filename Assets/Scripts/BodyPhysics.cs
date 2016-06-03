using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        return;
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
    }

    // Update positions based on velocity
    private static void UpdatePositions(List<Body> system, float time)
    {
        
        if (time == 0F)
        {
            return;
        }

        for (int i = 0; i < system.Count; i++)
        {
            Vector3 temp = system[i].Coords;
            for (int j = 0; j < 3; j++)
            {
                temp[j] += UpdateVelocity(system[i], time)[i] * time;
            }

            system[i].Coords = temp;
        }
    }

    // Update velocity based on given time
    private static Vector3 UpdateVelocity(Body body, float time)
    {
        Vector3 temp = body.Velocity;

        for (int i = 0; i < 3; i++)
            temp[i] = UpdateAcceleration(body)[i]*time;

        body.Velocity = temp;

        return body.Velocity;
    }

    // Update acceleration based on current forces
    private static Vector3 UpdateAcceleration(Body body)
    {
        Vector3 temp = body.Acceleration;

        for (int i = 0; i < 3; i++)
            temp[i] = (float)body.Mass*body.Forces[i];

        body.Acceleration = temp;

        return body.Acceleration;
    }

    // Move the bodies according to start and end time
    private void Move(List<Body> system, float start, float end, int dt)
    {

        // Call position update and force update during every t+dt
        for (float t = start; t < end; t += (float)dt)
        {
            UpdatePositions(system, t);
            //UpdateForces(system);
        }
    }
}
