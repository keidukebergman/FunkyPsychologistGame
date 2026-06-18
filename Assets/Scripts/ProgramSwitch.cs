using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityImage = UnityEngine.UI.Image;
using DrawingImage = System.Drawing.Image;


public class ProgramSwitch : MonoBehaviour
{
    [SerializeField] SingleGifPlayer programScript;
    [SerializeField] ProgramSwitchScript clientsScript;
    public GameObject myPrefabPlayer;

    public string[] nameValuesArray;
    public string[] nameValuesEntertainment;
    public string[] nameValuesTelemarket = { "fidgetSpinner", "newtonsCradle" };

    public float maxWaitTime = 5.0f;
    public bool timerIsRunning = false;
    public bool entertainment = false;
    public bool commercialOn = false;
    public bool callOngoing = false;
    public bool afterCallSatisf = false;
    public bool telemChoiceTime = false;
    public bool telemChoiceTimer = true;
    public bool distractionAppeared = false;
    public bool tvTurnedOn = true;
    public string[] clientOrder = { "Kaiju", "Boss" };
    public string clientNameEnd = "null";
    public int currentDist = 0;
    public int currentClient = 0;
    public int distrToBuy = 0;

    private bool playsCommercial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //If it's Kaiju time, then we do:

        if (clientsScript.callOngoing == true && clientsScript.afterCallSatisf == false)
        {
            RandomEntertainment();
        }
        else if (clientsScript.callOngoing == false & clientsScript.afterCallSatisf == true)
        {
            entertainment = true;
            var clientName = clientOrder[currentClient];

            if (clientsScript.satisfaction == true)
            {
                clientNameEnd = "/" + clientName + "GoodEnd.gif";
            }
            else
            {
                clientNameEnd = "/" + clientName + "BadEnd.gif";
            }

            currentClient = currentClient + 1;
            clientsScript.afterCallSatisf = false;
            Satisfaction(clientNameEnd);
        }
        else if (clientsScript.callOngoing == false & clientsScript.afterCallSatisf == false & tvTurnedOn == false & clientsScript.commercial == true)
        {
            clientsScript.commercial = false;
            Telemarket();
            
        }
        else if (clientsScript.callOngoing == false & clientsScript.afterCallSatisf == false & tvTurnedOn == false & clientsScript.commercial == false & entertainment == false)
        {
            if (!playsCommercial)
            {
                RandomEntertainment();
                Debug.Log("Ooops, you are in the ent if!:" + entertainment);
            }
        }
    }


    public void TurnOnTv()
    {

        if (tvTurnedOn == true)
        {
            
            int entProgram = UnityEngine.Random.Range(0, 5);
            programScript.gifPath = nameValuesEntertainment[entProgram];
            GameObject clonePlayer = Instantiate(myPrefabPlayer);
            GetComponent<SingleGifPlayer>().enabled = false;
            clonePlayer.GetComponent<SingleGifPlayer>().enabled = true;
            StartCoroutine(EntProgramTimer());
            Destroy(clonePlayer, 5.0f);
            tvTurnedOn = false;
        }



    }

    public void RandomEntertainment()
    {
        entertainment = true;
        int entProgram = UnityEngine.Random.Range(0, 4);
        programScript.gifPath = nameValuesEntertainment[entProgram];

        GameObject clonePlayer = Instantiate(myPrefabPlayer);
        GetComponent<SingleGifPlayer>().enabled = false;
        clonePlayer.GetComponent<SingleGifPlayer>().enabled = true;
        StartCoroutine(EntProgramTimer());
        Destroy(clonePlayer, 5.0f);

    }

    public void DistractionAppears()
    {

        GameObject.Find(nameValuesTelemarket[distrToBuy]).SetActive(true);
        distrToBuy = distrToBuy + 1;
    }

    public void Telemarket()
    {

        entertainment = true;

        programScript.gifPath = nameValuesTelemarket[currentDist];
        currentDist = currentDist + 1;
        telemChoiceTime = true;

        playsCommercial = true;

        GameObject clonePlayer = Instantiate(myPrefabPlayer);
        GetComponent<SingleGifPlayer>().enabled = false;
        clonePlayer.GetComponent<SingleGifPlayer>().enabled = true;
        StartCoroutine(TelProgramTimer());
        Destroy(clonePlayer, 5.0f);
        entertainment = false;
        Debug.Log("Telemarket done, ent state:" + entertainment);

    }

    public void Satisfaction(string clientEnd)
    {
        for (int i = 0; i < nameValuesArray.Length; i++)
        {
            if (nameValuesArray[i] == clientEnd)
            {
                entertainment = true;
                Debug.Log(clientEnd);
                programScript.gifPath = nameValuesArray[i];
                GameObject clonePlayer = Instantiate(myPrefabPlayer);
                GetComponent<SingleGifPlayer>().enabled = false;
                clonePlayer.GetComponent<SingleGifPlayer>().enabled = true;
                StartCoroutine(EntProgramTimer());
                Destroy(clonePlayer, 5.0f);
            }
        }
    }

    IEnumerator TelProgramTimer()
    {
        float timeElapsed = 0f;
        // 3. Loop every frame until the button is pressed OR time runs out
        while (timeElapsed < maxWaitTime || !Input.GetKeyDown(KeyCode.Keypad1) || !Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Increase timer by the time passed since the last frame
            timeElapsed += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) & distractionAppeared == false)
            {
                DistractionAppears();
                distractionAppeared = true;
            }
            if (timeElapsed >= maxWaitTime)
                break;
            // Pause the coroutine until the next frame
            yield return null;
        }

        telemChoiceTimer = false;
        telemChoiceTime = false;
        programScript.gifPath = "/black-screen.gif";
        distractionAppeared = false;
        playsCommercial = false;
        yield break;
    }
    IEnumerator EntProgramTimer()
    {
        yield return new WaitForSeconds(maxWaitTime);
        entertainment = false;
    }
}
