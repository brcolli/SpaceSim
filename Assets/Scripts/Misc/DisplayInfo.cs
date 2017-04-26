using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// For displaying various information about an object when it is clicked
/// </summary>

public class DisplayInfo : MonoBehaviour
{

    [SerializeField] private GameObject infoPanel_;
    [SerializeField] private GameObject settingsWindow_;
    [SerializeField] private Text infoText_;
    [SerializeField] private GameObject universe_;

    // Info fields
    [SerializeField] private InputField nameField_;
    [SerializeField] private InputField massField_;
    [SerializeField] private InputField radiusField_;
    [SerializeField] private InputField volumeField_;
    [SerializeField] private InputField densityField_;

    private GameObject currentObj_;
	
	void Update () {

        // Settings window on pressing 's'
        if (Input.GetKeyDown(KeyCode.S))
        {
            settingsWindow_.SetActive(!settingsWindow_.activeSelf);
        }

        // Information panel
        if (Input.GetMouseButtonDown(0))
        {

            // Check if mouse is over a body
            // Find mouse position in world space
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject selectedObj = GetBodyIntersectingRay(mouseRay);

            if (selectedObj)
            {
                // Turn panel on or off
                if (currentObj_ == selectedObj || infoPanel_.activeSelf == false)
                    infoPanel_.SetActive(!infoPanel_.activeSelf);

                currentObj_ = selectedObj;

                UpdateInformation();
            }
        }
    }

    /// <summary>
    /// Updates the information panel about a body
    /// </summary>
    public void UpdateInformation()
    {
        if (currentObj_)
        {
            Body currentBody = currentObj_.GetComponent<Body>();

            double currentMass = currentBody.Mass;
            double currentRadius = currentBody.Radius;

            double currBodyVolume = ((4.0 / 3.0) * Mathf.PI * Mathf.Pow((float)currentRadius, 3));

            // Update information
            nameField_.text = currentObj_.name;
            massField_.text = currentMass.ToString();
            radiusField_.text = currentRadius.ToString();
            volumeField_.text = currBodyVolume.ToString();
            densityField_.text = (currentMass / currBodyVolume).ToString();
        }

        /*
        string data = currentObj_.name +
                      "\nMass(units): " + currentMass +
                      "\nRadius(units): " + currentRadius +
                      "\nVolume(units): " + currBodyVolume +
                      "\nDensity(units): " + currentMass / currBodyVolume;

        // Get text components and write
        infoText_.text = data;*/
    }

    /// <summary>
    /// Deletes the object that is selected, deactivates panel
    /// </summary>
    public void DeleteSelected()
    {
        infoPanel_.SetActive(false);
        if (currentObj_)
        {
            // Remove from object 
            List<GameObject> objectPool = universe_.GetComponent<BodyContainer>().ObjectPool;
            objectPool.Remove(currentObj_);

            // Reset center if needed
            if (objectPool.Count < 1)
                universe_.GetComponent<PlayerStateController>().Center = universe_; // TODO make an empty game object with location equal to last center

            GameObject.Destroy(currentObj_.gameObject);
        }
    }


    /// <summary>
    /// Updates object's name
    /// </summary>
    /// <param name="newName">
    /// The new name
    /// </param>
    public void UpdateName(string newName)
    {
        currentObj_.name = newName;
    }

    /// <summary>
    /// Updates object's mass
    /// </summary>
    /// <param name="newMassStr">
    /// String representation of the new mass
    /// </param>
    public void UpdateMass(string newMassStr)
    {
        double newMass = Convert.ToDouble(newMassStr);

        Body currentBody = currentObj_.GetComponent<Body>();
        currentBody.Mass = newMass;

        // Update information to display new volume and density
        UpdateInformation();
    }

    /// <summary>
    /// Updates object's radius
    /// </summary>
    /// <param name="newRadiusStr">
    /// String representation of the new radius
    /// </param>
    public void UpdateRadius(string newRadiusStr)
    {
        double newRadius = Convert.ToDouble(newRadiusStr);

        Body currentBody = currentObj_.GetComponent<Body>();
        currentBody.Radius = newRadius;

        // Update information to display new volume and density
        UpdateInformation();
    }

    /// <summary>
    /// Updates object's radius given a change in volume
    /// </summary>
    /// <param name="newVolumeStr">
    /// String representation of the new volume
    /// </param>
    public void UpdateVolume(string newVolumeStr)
    {
        // Find the new radius value
        double newVolume = Convert.ToDouble(newVolumeStr);
        double newRadius = Mathf.Pow((float)(newVolume * (3.0 / 4.0)) / Mathf.PI, (float)1 / 3);

        // Call to update radius
        UpdateRadius(newRadius.ToString());
    }

    /// <summary>
    /// Finds the body intersected by a raycast
    /// </summary>
    /// <param name="ray">
    /// A ray for detection
    /// </param>
    /// <returns>
    /// The object the ray detects, or null
    /// </returns>
    public GameObject GetBodyIntersectingRay(Ray ray)
    {

        int layerMask = 1 << 9; // Only orbital objects
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return null;

        return hit.transform.gameObject;
    }
}
