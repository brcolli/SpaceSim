using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class Body : MonoBehaviour
{

    private void Start()
    {
        Mass = gameObject.GetComponent<Rigidbody>().mass;
        Radius = gameObject.GetComponent<SphereCollider>().radius;
    }

    /* Properties */

    public Body(float mass, float radius, float major, float eccentricity, Vector3? velocity=null,
                Vector3? position=null, float? theta=null, float? z=null)
    {
        this.Mass = mass;
        this.Radius = radius;
        this.Forces = new Vector3();
        this.Acceleration = new Vector3();

        if (position == null)
        {
            this.Coords = Coordinates.SysCoords(major, eccentricity, theta, z);
        }
        else
        {
            this.Coords = position ?? default(Vector3);
        }
        if (velocity == null)
        {
            this.Velocity = new Vector3();
        }
    }

    /* Getters and Setters */

    public double Mass { get; set; }
    public double Radius { get; set; }
    public Vector3 Coords { get; set; }
    public Vector3 Forces { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; set; }
}
