using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Holds all objects in the system. */

public class BodyContainer : MonoBehaviour
{

    // TODO: SQLite
    private void Start()
    {
        ObjectPool = new List<GameObject>();
        ObjectPool.Capacity = 10; // 8 planets, Sun, and Moon
    }

    public List<GameObject> ObjectPool { get; set; }
}
