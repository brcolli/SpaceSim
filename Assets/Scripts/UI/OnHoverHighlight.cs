using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls button highlighting.
/// </summary>

public class OnHoverHighlight : MonoBehaviour {

    private bool selected_ = false;
    private Color startingColor_;

    private void Start()
    {
        startingColor_ = gameObject.GetComponentInChildren<Text>().color;
    }

    /// <summary>
    /// Change between highlighted yellow and base color
    /// </summary>
    public void Highlight()
    {
        if (!selected_)
            gameObject.GetComponentInChildren<Text>().color = Color.yellow;
        else
            gameObject.GetComponentInChildren<Text>().color = startingColor_;
        selected_ = !selected_;
    }
}
