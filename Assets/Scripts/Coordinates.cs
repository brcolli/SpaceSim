using UnityEngine;
using System.Collections;

public class Coordinates
{
    public static Vector3 SysCoords(float major, float eccentricity, float? z = null, float? theta = null)
    {
        // Coords to return
        Vector3 coords = new Vector3();

        // If theta was not passed, give it a random value between 0 and 2pi
        if (theta == null)
        {
            theta = Random.Range(0, 2*Mathf.PI);
        }

        // If z was not passed, give it a value of 0
        if (z == null)
        {
            coords[2] = 0F;
        }

        // TODO: Use error bounds
        // If not a multiple of pi/2 but ignoring multiples of pi
        if (theta%(Mathf.PI/2) == 0 && theta%Mathf.PI != 0)
        {
            coords[0] = 0F;
        }
        else
        {
            coords[0] = (float)major*Mathf.Cos((float)theta);
        }

        // TODO: Use error bounds
        // If a multiple of pi, set to 0
        if (theta%Mathf.PI == 0)
        {
            coords[1] = 0F;
        }
        else
        {
            float minor = Mathf.Sqrt(Mathf.Pow(major, 2F)*(1 + Mathf.Pow(eccentricity, 2F)));
            coords[1] = minor*Mathf.Sin((float)theta);
        }

        return coords;
    }
}