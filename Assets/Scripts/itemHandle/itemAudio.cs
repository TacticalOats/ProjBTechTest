using UnityEngine;

public class itemAudio : MonoBehaviour
{
    private void Awake()
    {//When injected, add a audio source
        gameObject.AddComponent<AudioSource>();
        transform.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/solidHit");
        transform.GetComponent<AudioSource>().spatialBlend = 1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {//If this object touches something, make a "bong" sound.
        transform.GetComponent<AudioSource>().Play();
    }
}