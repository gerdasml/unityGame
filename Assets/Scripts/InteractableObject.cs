using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public Sprite sprite;
    public List<InteractableObject> dependencies;
}
