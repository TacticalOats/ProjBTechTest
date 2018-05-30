using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorerMode : MonoBehaviour {
    public Image explorerUI;
    public Text forceText;
    public Text walkSpeedText;
	void Update () {
		if (GetComponent<Dialog>().skipDialog == true){
            explorerUI.gameObject.SetActive(true);
            forceText.text = GetComponent<playerBehavior>().throwForce.ToString();
            walkSpeedText.text = GetComponent<playerBehavior>().pSpeed.ToString();

            if (Input.GetKeyDown(KeyCode.Equals)){
                GetComponent<playerBehavior>().throwForce++;
            }

            if (Input.GetKeyDown(KeyCode.Minus)){
                GetComponent<playerBehavior>().throwForce--;
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket)){
                GetComponent<playerBehavior>().pSpeed--;
            }
            
            if (Input.GetKeyDown(KeyCode.RightBracket)){
                GetComponent<playerBehavior>().pSpeed++;
            }
        }
	}
}
