using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extGrabbedBreak : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BOOSH");
        Destroy(gameObject);
    }
}
