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
    [SerializeField] private Text infoText_;
    [SerializeField] private GameObject universe_;

    private Body currentBody_;
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            // Check if mouse is over a body
            // Find mouse position in world space
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Body selectedBody = GetBodyIntersectingRay(mouseRay);

            if (selectedBody)
            {
                currentBody_ = selectedBody;

                // Turn panel on or off
                infoPanel_.SetActive(!infoPanel_.activeSelf);

                // TODO dynamically update
                string data = selectedBody.gameObject.name +
                              "\nMass (units): " + selectedBody.Mass +
                              "\nRadius(units): " + selectedBody.Radius +
                              "\nDensity(units): 0" +
                              "\nPosition(units): " + selectedBody.gameObject.transform.position +
                              "\nForce(units): 0" +
                              "\nVelocity(units): 0" +
                              "\nAcceleration(units): 0";

                // Get text components and write
                infoText_.text = data;
            }
        }
    }

    /// <summary>
    /// Deletes the object that is selected, deactivates panel
    /// </summary>
    public void DeleteSelected()
    {
        infoPanel_.SetActive(false);
        if (currentBody_)
        {
            // Remove from object pool
            GameObject currGameObject = currentBody_.gameObject;
            List<GameObject> objectPool = universe_.GetComponent<BodyContainer>().ObjectPool;
            objectPool.Remove(currGameObject);

            // Reset center if needed
            if (objectPool.Count < 1)
                universe_.GetComponent<PlayerStateController>().Center = universe_; // TODO make an empty game object with location equal to last center

            GameObject.Destroy(currentBody_.gameObject);
        }
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
    public Body GetBodyIntersectingRay(Ray ray)
    {

        int layerMask = 1 << 9; // Only orbital objects
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return null;

        return hit.transform.gameObject.GetComponent<Body>();
    }
}
