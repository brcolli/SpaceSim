using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls state that the player is in; ie placing an object, in menu, etc.
/// </summary>

public class PlayerStateController : MonoBehaviour
{
    
    [SerializeField] private GameObject canvas_;

    /// <summary>
    /// Units that mass is measured in
    /// </summary>
    public enum UnitsOfMass
    {
        kilograms,
        pounds
    };

    /// <summary>
    /// Units that distance is measured in (only for calculations,
    /// does NOT change actual positions
    /// </summary>
    public enum UnitsOfDistance
    {
        kilometers,
        miles
    };

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

    /// <summary>
    /// Changes the units for information displayed
    /// </summary>
    /// <param name="newUnitType">
    /// The unit to change to
    /// </param>
    public void ChangeUnits(string newUnitType)
    {
        BodyContainer bc = gameObject.GetComponent<BodyContainer>();
        DisplayInfo di = canvas_.GetComponent<DisplayInfo>();

        // Select based on unit string passed
        switch (newUnitType)
        {
            case "km":
                if (DistanceUnits != UnitsOfDistance.kilometers)
                {
                    DistanceUnits = UnitsOfDistance.kilometers;
                    bc.ChangeAllRadius(1.60934);
                }
                break;
            case "m":
                if (DistanceUnits != UnitsOfDistance.miles)
                {
                    DistanceUnits = UnitsOfDistance.miles;
                    bc.ChangeAllRadius(.621371);
                }
                break;
            case "kg":
                if (MassUnits != UnitsOfMass.kilograms)
                {
                    MassUnits = UnitsOfMass.kilograms;
                    bc.ChangeAllMass(.453592);
                }
                break;
            case "lb":
                if (MassUnits != UnitsOfMass.pounds)
                {
                    MassUnits = UnitsOfMass.pounds;
                    bc.ChangeAllMass(2.20462);
                }
                break;
            default:
                return;
        }
        di.UpdateInformation();
    }

    /* Getters and setters */

    public bool Placing { get; set; }
    public UnitsOfMass MassUnits { get; set; }
    public UnitsOfDistance DistanceUnits { get; set; }
}
