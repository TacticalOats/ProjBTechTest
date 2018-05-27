using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireInsert : MonoBehaviour {
    private bool beginTimer = false;
    private float timer0 = 0;
    private bool beginTimer1 = false;
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
        if (beginTimer == true){
            timer0 += Time.deltaTime;
            if (timer0 >= 6){
                if (transform.GetComponent<extGrabbedBehave>() == true)
                {
                    transform.GetComponent<extGrabbedBehave>().dropMe = true;
                }

            }
            if (timer0 >= 10){
                Destroy(gameObject);
            }
        }
        if (beginTimer1 == true){
            timer1 += Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<fireInsert>() != true && (other.gameObject.tag == "Item" || other.gameObject.tag == "Item:Breakable"))
        {
            beginTimer1 = true;
            if (timer1 >= 3)
            {
                other.gameObject.AddComponent<fireInsert>();
            }
        }
    }
}
