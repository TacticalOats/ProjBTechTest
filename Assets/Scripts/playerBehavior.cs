using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    //Dynamic Variables
    public Rigidbody pCamera;
    public Rigidbody pModel;
    public float pSpeed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
        camMove();

    }

    private void Move()
    {
        //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * pSpeed);

        var movX = Input.GetAxisRaw("Horizontal");
        var movY = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(new Vector3(movX, 0, movY).normalized * Time.deltaTime * (pSpeed * 2.5f));
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(new Vector3(movX, 0, movY).normalized * Time.deltaTime * (pSpeed/2));
        }
        else
        {
            transform.Translate(new Vector3(movX, 0, movY).normalized * Time.deltaTime * pSpeed);
        }

    }

    private void camMove()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")));
        pCamera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0));
    }
}