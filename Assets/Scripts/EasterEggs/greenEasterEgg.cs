using UnityEngine;

public class greenEasterEgg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //The first easter egg, and easiest. If we put an object in the crystal, turn it cyan.
    {
        other.GetComponent<Renderer>().material.color = Color.cyan;
    }
}