using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the rate at which time moves, between
/// minutes, hours, days, months, years. Also holds the total amount
/// time passed.
/// </summary>

public class TimeManager : MonoBehaviour {

    [SerializeField] private Text timeText_;

    private bool paused_;

    /// <summary>
    /// Rate at which time is passing
    /// </summary>
    public enum StepUnit
    {
        minutes,
        hours,
        days,
        months,
        years
    };

    void Start()
    {
        mins_ = 0;
        hrs_ = 0;
        days_ = 0;
        months_ = 0;
        yrs_ = 0;
        TimeStep = StepUnit.minutes; // Default is minutes

        paused_ = false; // Default is running
    }

    void FixedUpdate()
    {
        // Update personal time units if not paused
        if (!paused_)
        {
            IncrementByTimeStep();
            timeText_.text = TotalTime();
        }
    }

    // The individual values for minutes,
    // hours, days, months, and years
    private int mins_, hrs_, days_, months_, yrs_;

    /// <summary>
    /// Increments the current time step unit,
    /// to a max of 'years'
    /// </summary>
    public void IncrementTimeStep()
    {
        // Increment unit
        if (TimeStep != StepUnit.years)
            TimeStep += 1;
    }

    /// <summary>
    /// Decrements the current time step unit,
    /// to a minimum of 'mins'
    /// </summary>
    public void DecrementTimeStep()
    {
        // Decrement unit
        if (TimeStep != StepUnit.minutes)
            TimeStep -= 1;
    }

    /// <summary>
    /// Pauses or unpauses time
    /// </summary>
    public void ChangePauseState()
    {
        paused_ = !paused_; // Change pause state
        gameObject.GetComponent<BodyContainer>().ChangePhysicsState(); // Disable physics
    }

    /// <summary>
    /// Increments the total saved time by whatever the current timestep is.
    /// </summary>
    public void IncrementByTimeStep()
    {
        switch (TimeStep)
        {
            case StepUnit.minutes:
                IncrementMinutes();
                break;
            case StepUnit.hours:
                IncrementHours();
                break;
            case StepUnit.days:
                IncrementDays();
                break;
            case StepUnit.months:
                IncrementMonths();
                break;
            case StepUnit.years:
                IncrementYears();
                break;
            default:
                break;
        }
    }

    private void IncrementMinutes()
    {
        if (mins_ == 59)
        {
            mins_ = 0;
            IncrementHours();
        }
        else
            mins_ += 1;
    }
    private void IncrementHours()
    {
        if (hrs_ == 23)
        {
            hrs_ = 0;
            IncrementDays();
        }
        else
            hrs_ += 1;
    }
    private void IncrementDays()
    {
        if (days_ == 29)
        {
            days_ = 0;
            IncrementMonths();
        }
        else
            days_ += 1;
    }
    private void IncrementMonths()
    {
        if (months_ == 10)
        {
            months_ = 0;
            IncrementYears();
        }
        else
            months_ += 1;
    }
    private void IncrementYears()
    {
        yrs_ += 1;
    }

    public double GetG()
    {
        PlayerStateController psc = gameObject.GetComponent<PlayerStateController>();

        // Get distance and mass units
        PlayerStateController.UnitsOfMass massUnits = psc.MassUnits;
        PlayerStateController.UnitsOfDistance distanceUnits = psc.DistanceUnits;

        double G = 0.0;

        switch (TimeStep)
        {
            case StepUnit.minutes:

                // Match weight units
                if (massUnits == PlayerStateController.UnitsOfMass.kilograms)
                {
                    // Gravitational constant in AU^3 / kg * min^2
                    G = 7.1765794E-41;
                }
                else
                {
                    // Gravitational constant in AU^3 / lbs * min^2
                    G = 3.2552455E-41;
                }
                break;

            case StepUnit.hours:

                // Match weight units
                if (massUnits == PlayerStateController.UnitsOfMass.kilograms)
                {
                    // Gravitational constant in AU^3 / kg * hrs^2
                    G = 2.5835686E-37;
                }
                else
                {
                    // Gravitational constant in AU^3 / lbs * hrs^2
                    G = 1.1718884E-37;
                }
                break;

            case StepUnit.days:

                // Match weight units
                if (massUnits == PlayerStateController.UnitsOfMass.kilograms)
                {
                    // Gravitational constant in AU^3 / kg * day^2
                    G = 1.4881355E-34;
                }
                else
                {
                    // Gravitational constant in AU^3 / lbs * day^2
                    G = 6.7500771E-35;
                }
                break;

            case StepUnit.months:

                // Match weight units
                if (massUnits == PlayerStateController.UnitsOfMass.kilograms)
                {
                    // Gravitational constant in AU^3 / kg * month^2
                    G = 1.3786115E-31;
                }
                else
                {
                    // Gravitational constant in AU^3 / lbs * month^2
                    G = 6.2532838E-32;
                }
                break;

            case StepUnit.years:

                // Match weight units
                if (massUnits == PlayerStateController.UnitsOfMass.kilograms)
                {
                    // Gravitational constant in AU^3 / kg * yrs^2
                    G = 1.9852005E-29;
                }
                else
                {
                    // Gravitational constant in AU^3 / lbs * yrs^2
                    G = 9.0047287E-30;
                }
                break;
        }

        return G;

        /* UNUSED GRAVITATIONAL CONSTANTS */
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
        //const double G = 39.4784176044;
        // Gravitational constant in pc / SMU * (km/s)^2; Use with orbital mechanics
        //const double G = 4.302E-3;
    }

    /* Getters and setters */

    public StepUnit TimeStep { get; set; }

    public string TotalTime()
    {
        return yrs_.ToString()+":"+months_.ToString()+":"+days_.ToString()+":"+hrs_.ToString()+":"+mins_.ToString();
    }
}
