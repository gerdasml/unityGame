using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCollider : MonoBehaviour {

    public GameObject ExpectedCube;
    public bool IsExpectedCubeTouching { get; private set; }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject == ExpectedCube)
        {
            IsExpectedCubeTouching = true;
        }
    }

    public void OnCollisionExit(Collision c)
    {
        if (c.gameObject == ExpectedCube)
        {
            IsExpectedCubeTouching = false;
        }
    }
}