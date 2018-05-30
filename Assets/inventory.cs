using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    // Use this for initialization
    public Text text;
    public Button button;
    public Button button2;
    public Sprite Image1;
    public Sprite emptyInv;
    public bool boxOneFilled = false;
    public bool boxTwoFilled = false;
    public GameObject prefab;
    public Transform prefab1;
    [HideInInspector]
    public GameObject box1;
    public GameObject box2;
    

    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = (transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, forward, out hit, 5.0f))
        {
            if (hit.transform.tag == "inventoryCube")
            {
                text.enabled = true;
                if (Input.GetKeyDown("space"))
                {
                    if (boxOneFilled == false)
                    {
                        prefab = hit.collider.gameObject;
                        box1 = prefab;
                        prefab.transform.position = new Vector3(75.0F, 5.0F, 0);
                        button.GetComponent<Image>().sprite = Image1;
                        boxOneFilled = true;
                    }

                    else if (boxTwoFilled == false)
                    {
                        prefab = hit.collider.gameObject;
                        box2 = prefab;
                        prefab.transform.position = new Vector3(78.0F, 5.0F, 0);
                        button2.GetComponent<Image>().sprite = Image1;
                        boxTwoFilled = true;
                    }
                    }
                }
            }
            else
            {
                text.enabled = false;
            }

        

        if (Input.GetKeyDown("1") && boxOneFilled == true)
        {
            Vector3 forward2 = (transform.TransformDirection(Vector3.forward));
            RaycastHit hit2;
            if (Physics.Raycast(transform.position, forward, out hit2, 1.0f) == false)
            {
                button.GetComponent<Image>().sprite = emptyInv;
                Vector3 carry = transform.position + forward * 2;
                box1.transform.position = carry;
                boxOneFilled = false;
                box1.transform.SetParent(transform.GetChild(0));
                box1.AddComponent<extGrabbedBehave>();
            }
            else { }
        }

        if (Input.GetKeyDown("2") && boxTwoFilled == true)
          {
            Vector3 forward2 = (transform.TransformDirection(Vector3.forward));
            RaycastHit hit2;
            if (Physics.Raycast(transform.position, forward, out hit2, 1.0f) == false)
            {
                button2.GetComponent<Image>().sprite = emptyInv;
                Vector3 carry = transform.position + forward * 2;
                box2.transform.position = carry;
                boxTwoFilled = false;
                box2.transform.SetParent(transform.GetChild(0));
                box2.AddComponent<extGrabbedBehave>();

            }
            else { }
            }
        }
          
    }



        

