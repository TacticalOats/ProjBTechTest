using UnityEngine;

public class devCubeRed : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) //Used in narratorCube; Dialog Sequence
    {
        if (other.gameObject.name == "Lava")
        {
            GameObject.Find("playerMod").transform.GetComponent<Dialog>().devCube1 = false; //Report back that we broke.
            Destroy(gameObject);
        }
    }
}