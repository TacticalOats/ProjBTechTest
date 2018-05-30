using UnityEngine;

public class extGrabbedBehave : MonoBehaviour
{
    [HideInInspector] //On second thought, unless you actually have this shit running in inspector mode, you'll never see these anyway
    public bool dropMe;

    [HideInInspector]
    public bool throwMeB; //No, throw me is not a "bae", it means bool.

    private void Awake()
    {
        transform.GetComponent<Rigidbody>().useGravity = false; //Since this is an injected code, when the code gets activated, don't let the object fall.
        dropMe = false; //On second thought, this could probably have been called in the variable itself.
    }

    private void FixedUpdate()
    {
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = transform.parent.position;
        if (dropMe == true) //Using playerPickUp, we can say which of these to preform
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

    #region fall() Method
    private void fall() //Fall Physics
    {
        GetComponentInParent<playerPickUp>().canCarry = true; //Communicates back that our hand is cleared
        transform.parent = null;
        transform.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject.GetComponent<extGrabbedBehave>());
    }
    #endregion

    #region throwMe() Method
    private void throwMe() //Throw Physics
    {
        playerPickUp pPUScript = GetComponentInParent<playerPickUp>();
        transform.GetComponent<Rigidbody>().AddForce(transform.parent.forward * GetComponentInParent<playerBehavior>().throwForce, ForceMode.Impulse); //Adds force to object
        pPUScript.canCarry = true; //Don't show we can carry till after this thing already starts launching
        transform.parent = null;
        transform.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject.GetComponent<extGrabbedBehave>());
    }
    #endregion
}