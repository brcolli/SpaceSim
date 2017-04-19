using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows user to load and save scenes
/// </summary>

public class LoadSaveScene : MonoBehaviour
{

    /// <summary>
    /// Loads a scene given an integer representation
    /// </summary>
    /// <param name="level">
    /// The integer corresponding to the scene
    /// </param>
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SaveScene()
    {
        
    }
}
