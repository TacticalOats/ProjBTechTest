using UnityEngine;
using UnityEngine.UI;

public class playerPickUp : MonoBehaviour
{
    #region Variables - Object Pickup & Drop

    private bool canGrab;

    [HideInInspector] //Designers don't need to alter this shit, hide it from their asses
    public bool canCarry = true;

    [HideInInspector]
    public GameObject grabbedItem;

    private GameObject curItem;

    private GameObject lastTouched;

    #endregion Variables - Object Pickup & Drop

    #region Variables - Cursor Changing

    public Sprite curDef;
    public Sprite curGrabbable;
    public Sprite curGrabbed;
    public Image curImage;

    #endregion Variables - Cursor Changing

    public Text lbs; //Used to display weight

    private void Update()
    {
        checkTag("Item", "Item:Breakable");        //Sometime update to use LINQ, checks for tags of grabbable items
        if (canGrab == true)
        { //When the object is grabbable, run the objInterect() method
            objInteract();
        }
        changeCur(); //Simply assures we grab the right cursor overlay for different object returns
    }

    //RELIES ON "Mike" TO FUNCTION PROPERLY

    #region checkTag() Method

    private bool checkTag(string tag, string tag2)
    { //Mimics LINQ till we actually use LINQ
        RaycastHit imTouching;
        if (Physics.Raycast(transform.position, transform.forward, out imTouching, 5.0f))
        { //You should know how raycasts work.
            if (imTouching.transform.tag == tag || imTouching.transform.tag == tag2)
            {
                imTouching.transform.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.05f); //Draws outline on grabbable object
                pounds(imTouching.rigidbody.mass);//Sends mass to the pounds() method
                curItem = imTouching.transform.gameObject; //stores the gameobject into a variable
                if (lastTouched != curItem)
                { //Assures that we don't keep writing outlines to every outlineable object
                    if (lastTouched != null && lastTouched.transform.GetComponent<Renderer>() == true)
                    {
                        lastTouched.transform.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
                    }
                    lastTouched = curItem;
                }
                Debug.DrawRay(transform.position, transform.forward * imTouching.distance, Color.white); //Useful for designers to know if their object is reachable
                return canGrab = true;
            }
        }//If we can't pick it up, then dump the gameobject variable and report back
        Debug.DrawRay(transform.position, transform.forward * imTouching.distance, Color.white);
        lbs.text = null;
        if (curItem != null) //If we still have something stored in curItem, reset its outline and get rid of the reference
        {
            if (curItem.GetComponent<Renderer>() == true)//Due to breakable objects, we might need to make sure they actually have a renderer
            {
                curItem.transform.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.0f);
            }
            curItem = null;
        }
        return canGrab = false;
    }

    #endregion checkTag() Method

    #region objInteract() Method

    private void objInteract()
    {
        if (Input.GetMouseButtonDown(1))
        { //Right click
            if (canCarry == true) //If we actually can pick something up
            {
                grabbedItem = curItem; //Holds our gameobject variable for manipulation and dumps the temp variable
                curItem = null;
                grabbedItem.transform.SetParent(transform.GetChild(0));
                grabbedItem.AddComponent<extGrabbedBehave>(); //Inserts an object behaviour script into our grabbed object
                canCarry = false;
                return; //Stops "fall through"
            }

            if (canCarry == false) //If we right-click again, drop it
            {
                if (grabbedItem.GetComponent<extGrabbedBehave>() != null) //If the script actually exsists, then do it.
                {
                    extGrabbedBehave eGBScript = grabbedItem.GetComponent<extGrabbedBehave>();
                    eGBScript.dropMe = true; //The beauty of using external scripts
                }
                return;
            }
        }
        if (Input.GetMouseButtonDown(0))
        { //Left-click, throwing
            if (canCarry == false)
            {
                extGrabbedBehave eGBScript = grabbedItem.GetComponent<extGrabbedBehave>();
                eGBScript.throwMeB = true; //Tells the internal script to run the throw function.
                return;
            }
        }
    }

    #endregion objInteract() Method

    #region changeCur() Method

    private void changeCur()
    {
        if (canCarry == false) //Carrying takes importance over the report of our raycast
        {
            curImage.sprite = curGrabbed;
        }
        else if (canGrab == true) //If we can't carry, then Raycast is second most important to read
        {
            curImage.sprite = curGrabbable;
        }
        else //If all else fails, put our cursor to normal
        {
            curImage.sprite = curDef;
        }
    }

    #endregion changeCur() Method

    #region pounds() Method

    private void pounds(float mass)
    {
        lbs.text = mass.ToString() + "lbs"; //If the weight is above or equal to 80lbs, then make text red. Otherwise, white text.
        if (mass >= 80)
        {
            lbs.color = Color.red;
        }
        else { lbs.color = Color.white; }
    }

    #endregion pounds() Method
}