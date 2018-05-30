using UnityEngine;

public class itemAudioBreak : MonoBehaviour
{
    private bool breakTime = false;
    private bool breakTimeDie = false;

    private void FixedUpdate()
    {
        if (transform.GetComponent<extGrabbedBehave>() == true && transform.GetComponent<extGrabbedBehave>().throwMeB == true) //If we have an external script and throwing is true, then break
        {
            transform.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/glassShatterICE"); //Yup, the sound effect was made with ice.
            breakTime = true;
        }
        if (breakTimeDie == true && GetComponent<AudioSource>().isPlaying == false) //Stops us from destroying before we actually hear a shatter
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>(); //When this code is injected, make us a audio source
        transform.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/solidHit"); //Pulls from the "Resources" folder
        transform.GetComponent<AudioSource>().spatialBlend = 1.0f; //3D Audio
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (breakTimeDie == false)
        {
            if (breakTime == true)
            {//Give the illusion the object is gone until the audio stops playing, then actually delete it.
                transform.GetComponent<AudioSource>().Play();
                Destroy(GetComponent<BoxCollider>());
                Destroy(GetComponent<MeshRenderer>());
                breakTimeDie = true;
            }
            else
            {
                transform.GetComponent<AudioSource>().Play(); //If it's not time to be broken, just make a "bong" sound
            }
        }
    }
}