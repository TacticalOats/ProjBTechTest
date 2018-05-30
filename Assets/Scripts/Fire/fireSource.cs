using UnityEngine;

public class fireSource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {//Self-sustaining code. Simply, if another burnable object touches us, set it on fire
        if (other.tag == "Item" || other.tag == "Item:Breakable")
        {
            if (other.gameObject.GetComponent<fireInsert>() != true)
            {
                other.gameObject.AddComponent<fireInsert>();
            }
        }
    }//In the future, we could probably use OnTriggerStay and a timer to force the object to stay in for a time before igniting
}