using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSource : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Item:Breakable"){
            if (other.gameObject.GetComponent<fireInsert>() != true)
            {
                other.gameObject.AddComponent<fireInsert>();
            }
        }
    }
}
