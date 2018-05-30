using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    #region Dynamic Variables

    //Dynamic Variables
    public Rigidbody pCamera;

    public Rigidbody pModel;
    public float pSpeed;
    public float throwForce;

    #endregion Dynamic Variables

    #region Audio Variables

    //Audio Variables
    public AudioClip Walking;

    public AudioClip Running;
    public AudioClip SlowWalk;

    #endregion Audio Variables

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;//Don't let our computer cursor wonder
    }

    private void Start()
    {
        #region Audio Injection

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            item.gameObject.AddComponent<itemAudio>();
        }

        GameObject[] itemsBreak = GameObject.FindGameObjectsWithTag("Item:Breakable");
        foreach (GameObject itemB in itemsBreak)
        {
            itemB.gameObject.AddComponent<itemAudioBreak>();
        }

        #endregion Audio Injection
    }

    private void FixedUpdate()
    {
        Move();
        camMove();
    }

    #region Move() Method

    private void Move()
    {
        //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * pSpeed);

        var movX = Input.GetAxisRaw("Horizontal");
        var movZ = Input.GetAxisRaw("Vertical"); //Most people say this is movY, but it's not, you're moving on the Z axis, not Y.
        if (Input.GetKey(KeyCode.LeftShift)) //Sprint
        {
            transform.Translate(new Vector3(movX, 0, movZ).normalized * Time.deltaTime * (pSpeed * 2));
        }
        else if (Input.GetKey(KeyCode.LeftControl)) //Walk Slow
        {
            transform.Translate(new Vector3(movX, 0, movZ).normalized * Time.deltaTime * (pSpeed / 2));
        }
        else
        { //If no shit click, just throw me into walking mode
            transform.Translate(new Vector3(movX, 0, movZ).normalized * Time.deltaTime * pSpeed);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {//All of this shit is because I wanted audio... and the shit above me is just ebola. Man, I'm glad this project is closed.
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetComponent<AudioSource>().clip = Running;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.GetComponent<AudioSource>().clip = SlowWalk;
            }
            else
            {
                transform.GetComponent<AudioSource>().clip = Walking;
            }
            if (transform.GetComponent<AudioSource>().isPlaying == false)
            {
                transform.GetComponent<AudioSource>().loop = true;
                transform.GetComponent<AudioSource>().Play();
            }
        }
        else
        { //Once again, if shit aint bein clicked, don't do shit.
            transform.GetComponent<AudioSource>().loop = false;
            transform.GetComponent<AudioSource>().Stop();
        }
    }

    #endregion Move() Method

    #region camMove() Method

    private void camMove()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")));
        pCamera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0));
        //I still have no fucking clue how to stop neck snapping. I've manage to script most of everything else,
        //but this is still the one little bastard I can never seem to fix. Fuck.
    }

    #endregion camMove() Method
}