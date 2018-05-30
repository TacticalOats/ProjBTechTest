using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{ //Over 200 lines of code, by far the longest consecutive piece of code in the project.
    #region Audio Sequencing Variables
    private bool[] audioSeq = new bool[] { false, false, false, false, false, false }; //Used to know which audio sequence we're on
    private AudioSource dialog; //You'll see me later
    [HideInInspector]//Go away designers
    public bool devCube1 = true; //Red Cube
    private bool devCube2 = true; //Blue Cube
    private bool goodBoy = false; //This was named based on the original line of dialog, where I would call you a "Good boy" for throwing the block into the lava
    private bool runDoor1 = false;
    private bool runDoor2 = false;
    private GameObject child; //You'll see me later
    #endregion

    #region Disable Dialog Variables
    public Text txt; //Shares the weight text
    public bool skipDialog = false;
    private float timer0 = 0; //Timers, yay!
    private bool runTimer = false;
    public GameObject path; //refers to the path you get in the play room when disabling dialog
    #endregion

    #region Lava Easter Egg Variables
    private bool lavaFirst = false;
    private bool pastSeqFire = false;
    #endregion

    #region White Easter Egg
    public GameObject oldMap;
    #endregion

    private void Awake() //Initializing, no way around it
    {
        child = transform.GetChild(0).gameObject; //Grabs child 0, honestly ".child" should be a default, just like ".parent"
        child.transform.gameObject.AddComponent<AudioSource>(); //We don't already have an audio source, so give us one
        dialog = child.transform.GetComponent<AudioSource>();
        playAudio("Audio/WordPlay/WordPlay0", false); //Part 0
        audioSeq[0] = true; //Let us know dialog has begun
        child.GetComponent<playerPickUp>().canCarry = false; //Stop us from playing with shit till I say so
        GameObject.Find("narratorCube").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("narratorCube2").GetComponent<Rigidbody>().isKinematic = true;
    }
    private void Update()
    {
        #region M&J Dialog Disable
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.J) && skipDialog == false){//The fun button
            skipDialog = true; //Start skipping dialog
            dialog.Stop(); //All dialog, playing or not, needs to stop
            runTimer = true; //Tells timer to start
            Destroy(GameObject.Find("Door1")); //Rather long destroy list, but it needs to happen.
            Destroy(GameObject.Find("door1End"));
            Destroy(GameObject.Find("Door2"));
            Destroy(GameObject.Find("door2End"));
            Destroy(GameObject.Find("devBlock1Trigger"));
            Destroy(GameObject.Find("FireTrigger"));
            Destroy(GameObject.Find("triggerFireRoom"));
            if (GameObject.Find("narratorCube") != null) { GameObject.Find("narratorCube").GetComponent<Rigidbody>().isKinematic = false; } //If I exsist, let me be free
            if (GameObject.Find("narratorCube2") != null) { GameObject.Find("narratorCube2").GetComponent<Rigidbody>().isKinematic = false; } //Same here
            child.GetComponent<playerPickUp>().canCarry = true; //Allow us to carry shit
            path.SetActive(true); //If we're in play room, let us walk back
            oldMap.SetActive(true);
            return;
        }

        if (runTimer == true) //Just controls the text that informs the user they disabled dialog
        {
            txt.text = "EXPLORER MODE ENABLED";
            txt.color = Color.green;
            timer0 += Time.deltaTime;

            if (timer0 > 5) //After 5 seconds, pretend nothing ever happened
            {
                txt.text = "";
                runTimer = false;
                return;
            }
        }
        #endregion
    }
    private void FixedUpdate()
    {
        if (skipDialog == false)
        {
            if (runDoor1 == true)
            {
                doorLower("Door1", "door1End");
            }
            if (runDoor2 == true)
            {
                doorLower("Door2", "door2End");
            }
            #region Dialog Sequence Handler
            if (dialog.isPlaying == false) //If the line of audio has stopped, then proceed
            {//In future reference, this should be done from 6 to 0, not 0 to 6. 0 to 6 causes fall through, explaining why we need return statements.
                if (audioSeq[0] == true) { audioSeq[0] = false; return; }
                if (audioSeq[1] == true) { Destroy(GameObject.Find("FireTrigger")); audioSeq[1] = false; return; }
                if (audioSeq[2] == true) { child.GetComponent<playerPickUp>().canCarry = true; GameObject.Find("narratorCube").GetComponent<Rigidbody>().isKinematic = false; Destroy(GameObject.Find("devBlock1Trigger")); audioSeq[2] = false; return; } //Enable red-block
                if (devCube1 == false) { child.GetComponent<playerPickUp>().canCarry = false; audioSeq[3] = true; devCube1 = true; return; }//DevCube1 gets destroyed
                if (audioSeq[3] == true)
                {
                    if (goodBoy == false) //If you didn't catch it, go to variables to see why this is called "goodBoy"
                    { //Moves door
                        playAudio("Audio/WordPlay/WordPlay03", false); //Part 4
                        goodBoy = true;
                        runDoor1 = true;
                        return;
                    }
                    if (runDoor1 == false) //Since the door has lowered, move on
                    {
                        playAudio("Audio/WordPlay/WordPlay04", false);
                        Destroy(GameObject.Find("Door1")); //Go ahead and delete door1, we no longer need it.
                        audioSeq[4] = true;
                        audioSeq[3] = false;
                        return;
                    }
                }
                if (audioSeq[4] == true) { child.GetComponent<playerPickUp>().canCarry = true; GameObject.Find("narratorCube2").GetComponent<Rigidbody>().isKinematic = false; audioSeq[4] = false; return; } //Enable Blue Block
                if (GameObject.Find("narratorCube2") == null && audioSeq[4] == false && audioSeq[5] == false && devCube2 == true) //If everything is fine and dandy, then move on. A lot to determine, I know
                {
                    playAudio("Audio/WordPlay/WordPlay05", false); //Part 5
                    audioSeq[5] = true;
                    runDoor2 = true;
                    devCube2 = false;
                    return;
                }
                if (audioSeq[5] == true) //All audio is complete, delete the last barrier
                {
                    Destroy(GameObject.Find("Door2"));
                    audioSeq[5] = false;
                    return;
                }
            }//This is what I call "3pm Code", I wrote it all when I was pulling an all-nighter, and it is very fucking messy because of it
            #endregion
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (skipDialog == false)
        {
            if (audioSeq[0] == false && other.name == "FireTrigger") //"Turn around" trigger
            {
                playAudio("Audio/WordPlay/WordPlay01", false); //Part 1
                audioSeq[1] = true;
                return;
            }
            if (audioSeq[1] == false && other.name == "devBlock1Trigger") //"Look at the cube" trigger
            {
                Destroy(GameObject.Find("devBlock1Trigger").GetComponent<CapsuleCollider>());
                other.gameObject.AddComponent<BoxCollider>();
                playAudio("Audio/WordPlay/WordPlay02", false); //Part 2
                audioSeq[2] = true;
                pastSeqFire = true;
                return;
            }
            if (other.name == "triggerFireRoom") //Room where you set objects on fire, last dialog
            {
                playAudio("Audio/WordPlay/WordPlay06", false);
                Destroy(other.gameObject);
            }
        }

        //Add in "Extras" here
    }

    private void OnTriggerStay(Collider other)
    {
        #region Lava Easter Egg
        if (other.name == "Lava") //This easter egg was planned from the get-go of the lava pit modeling.
        {//In fact, the dialog you here for this is actually the original dialog I recorded during the all-nighter
            if (lavaFirst == false && pastSeqFire == false && skipDialog == false) //That's right, if you want it, you cant skip dialog
            {
                playAudio("Audio/WordPlay/WordPlayLavaEEgg", true);
                lavaFirst = true;
                return;
            }
            else if (lavaFirst == true && dialog.isPlaying == false || pastSeqFire == true || skipDialog == true)
            {//Above more or less says that if it's anything else but the top, then do this, but also makes sure audio has finished playing
                transform.position = new Vector3(18.5f, 24, -13.46f); //Very specific, I know
            }
        }
        #endregion
    }

    #region playAudio() Method
    private void playAudio(string path, bool overrideAudio) //Plays audio, useful shortcut method
    {
        dialog.clip = Resources.Load<AudioClip>(path);
        if (dialog.isPlaying == false)
        {
            dialog.Play();
        }
        else if (overrideAudio == true) //Allows us to override the current audio playing, though this was never actually needed
        {
            dialog.Stop();
            dialog.Play();
        }
    }
    #endregion

    #region doorLower Method
    private void doorLower(string door, string endpoint)//This method gave me a lot of hell, yet it was the sequence handler's fault!
    {//In hind sight, I should of followed the "Never same piece of code twice" rule... seriously.
        if (GameObject.Find(door).GetComponent<Rigidbody>().position.y > 20)
        {
            GameObject.Find(door).GetComponent<Rigidbody>().transform.position = Vector3.Lerp(GameObject.Find(door).GetComponent<Rigidbody>().position, GameObject.Find(endpoint).GetComponent<Rigidbody>().position, Time.deltaTime / 2);
        }
        if (GameObject.Find(door).GetComponent<Rigidbody>().position.y <= 20) { runDoor1 = false; runDoor2 = false; }
    }
    #endregion
} //Wow, 200 lines of code later and this is easily the worst script I've ever written. Even my very first game had better code than this, not kidding.