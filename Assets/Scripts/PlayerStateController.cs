using UnityEngine;
using System.Collections;

/* Controls state that the player is in; ie placing an object, in menu, etc. */

public class PlayerStateController : MonoBehaviour
{

    public GameObject Center; // Center of the Universe

    void Start()
    {
        Center = gameObject;
        Placing = false;
    }

    public bool Placing { get; set; }
}
