using UnityEngine;
using System.Collections;

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
