using UnityEngine;

public class fireInsert : MonoBehaviour
{//This code is inject into the object that is caught on fire, injector is used as some objects can be "thrown" into the fire.
    //Timer Initalization
    private bool beginTimer = false;
    private float timer0 = 0;
    private bool beginTimer1 = false;
    [HideInInspector] //Go away designers...
    public float timer1 = 0;

    private void Awake() //Initalizing
    {
        beginTimer = true;
        GetComponent<Renderer>().material.color = Color.black;
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SphereCollider>().radius = 1.0f;
    }

    private void Update() //Timers
    {
        if (beginTimer == true)
        {
            timer0 += Time.deltaTime;
            if (timer0 >= 6) //After 6 seconds, start forcing us to drop this object.
            {
                if (transform.GetComponent<extGrabbedBehave>() == true)
                {
                    transform.GetComponent<extGrabbedBehave>().dropMe = true;
                }//In the future, forcing us to report back as not-grabbable probably would be better
            }
            if (timer0 >= 10) //After 10 seconds, destroy ourselves
            {
                Destroy(gameObject);
            }
        }
        if (beginTimer1 == true) //Used for fire spread, found in OnTriggerStay
        {
            timer1 += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other) //We use this for fire spread, but we also could have used this to set ourselves on fire in general.
    {
        if (other.gameObject.GetComponent<fireInsert>() != true && (other.gameObject.tag == "Item" || other.gameObject.tag == "Item:Breakable"))
        {
            beginTimer1 = true;
            if (timer1 >= 3)
            {
                other.gameObject.AddComponent<fireInsert>(); //If we're burning, and 3 seconds has passed after a collision with a burnable object, start igniting shit
            }
        }
    }//Instead of always worrying about destroying an injected object, we could have set a time till the injector was automatically removed.
}