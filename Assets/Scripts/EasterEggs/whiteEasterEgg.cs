using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteEasterEgg : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "playerMod"){
            collision.transform.position = new Vector3(18.5f, 24, -13.46f);
        }
    }
}
