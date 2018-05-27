using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extGrabbedBehave : MonoBehaviour {
    [HideInInspector]
    public bool dropMe;
    [HideInInspector]
    public bool throwMeB;
    private void Awake()
    {
        transform.GetComponent<Rigidbody>().useGravity = false;
        dropMe = false;
    }
    void FixedUpdate () {
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = transform.parent.position;
        if (dropMe == true)
        {
            fall();
        }
        if (throwMeB == true)
        {
            throwMe();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {   
        playerPickUp pPUScript = GetComponentInParent<playerPickUp>();
        pPUScript.canCarry = true;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.parent = null;
        Destroy(gameObject.GetComponent<extGrabbedBehave>());
    }
    private void fall(){
        GetComponentInParent<playerPickUp>().canCarry = true;
        transform.parent = null;
        transform.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject.GetComponent<extGrabbedBehave>());
    }
    private void throwMe(){
        if (transform.gameObject.tag == "Item:Breakable"){
            transform.gameObject.AddComponent<extGrabbedBreak>();
        }
        playerPickUp pPUScript = GetComponentInParent<playerPickUp>();
        transform.GetComponent<Rigidbody>().AddForce(transform.parent.forward * GetComponentInParent<playerBehavior>().throwForce, ForceMode.Impulse);
        pPUScript.canCarry = true;
        transform.parent = null;
        transform.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject.GetComponent<extGrabbedBehave>());
    }
}
