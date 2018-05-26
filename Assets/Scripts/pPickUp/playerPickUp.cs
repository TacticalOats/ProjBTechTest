using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPickUp : MonoBehaviour
{
    private bool canGrab;
    public bool canCarry = true;
    [HideInInspector]
    public GameObject grabbedItem;
    private GameObject curItem;
    void Update()
    {
        //Sometime move checkTag to LINQ
        checkTag("Item", "Item:Breakable");
        if (canGrab == true){
            objInteract();
        }
    }

    private bool checkTag(string tag, string tag2) {
        RaycastHit imTouching;
        if (Physics.Raycast(transform.position, transform.forward, out imTouching, 5.0f)){
            if (imTouching.transform.tag == tag || imTouching.transform.tag == tag2){
                curItem = imTouching.transform.gameObject;
                Debug.DrawRay(transform.position, transform.forward * imTouching.distance, Color.white);
                return canGrab = true;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * imTouching.distance, Color.white);
        curItem = null;
        return canGrab = false;
    }

    private void objInteract(){
        if (Input.GetMouseButtonDown(1)){
            if (canCarry == true)
            {
                grabbedItem = curItem;
                curItem = null;
                grabbedItem.transform.SetParent(transform.GetChild(0));
                grabbedItem.AddComponent<extGrabbedBehave>();
                canCarry = false;
                return;
            }
            
            if (canCarry == false)
            {
                extGrabbedBehave eGBScript = grabbedItem.GetComponent<extGrabbedBehave>();
                eGBScript.dropMe = true;
                canCarry = true;
                return;
            }
        }
    if (Input.GetMouseButtonDown(0)){
        if (canCarry == false){
                extGrabbedBehave eGBScript = grabbedItem.GetComponent<extGrabbedBehave>();
                eGBScript.throwMeB = true;
                return;
            }
        }
    }
}
