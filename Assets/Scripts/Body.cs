using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

/// <summary>
/// Class for all bodies in the system.
/// </summary>

public class Body : MonoBehaviour
{

    // FOR DEBUGGING ONLY
    [SerializeField] private double Mass_;
    [SerializeField] private double Radius_;

    private void Start()
    {
        if (this.Mass_ != 0 && this.Radius_ != 0)
        {
            this.Mass = Mass_;
            this.Radius = Radius_;
        }
        //Mass = gameObject.GetComponent<Rigidbody>().mass;
        //Radius = gameObject.GetComponent<SphereCollider>().radius;
    }

    /// <summary>
    /// Returns a Body object with given mass and radius
    /// </summary>
    /// <param name="m">
    /// Mass
    /// </param>
    /// <param name="r">
    /// Radius
    /// </param>
    public Body(double m, double r)
    {
        this.Mass = m;
        this.Radius = r;
        this.Forces = Vector3.zero;
        this.Acceleration = Vector3.zero;
        this.Velocity = Vector3.zero;
    }

    public Body() { }

    /*
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
    }*/

    // Copy Constructor
    /*public Body Copy()
    {
        Body b = new Body(this.Mass, this.Radius);
        return b;
    }*/

    /* Getters and Setters */

    public double Mass { get; set; }
    public double Radius { get; set; }
    public Vector3 Forces { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; set; }
}
