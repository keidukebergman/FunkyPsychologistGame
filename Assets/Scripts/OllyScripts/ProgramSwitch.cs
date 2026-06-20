using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public class ProgramSwitch : MonoBehaviour
{
    [SerializeField] SingleGifPlayer programScript;
    [SerializeField] ProgramSwitchScript clientsScript;
    public GameObject myPrefabPlayer;

    [SerializeField] private AudioSource audioSource;
    public string gifName;


    public string[] nameValuesArray;
    public string[] nameValuesEntertainment;
    public string[] nameValuesTelemarket = { "/metronome.gif", "/bobblehead.gif", "/newtonsballs.gif", "/piano.gif", "/balloons.gif", "/fidget.gif", "/standee.gif" };
    public GameObject[] nameValuesDistractions = { };

    public float maxWaitTime = 5.0f;
    public float switchWaitTime = 2.0f;
    public bool timerIsRunning = false;
    public bool entertainment = false;
    public bool callOngoing = false;
    public bool afterCallSatisf = false;
    public bool telemChoiceTime = false;
    public bool telemChoiceTimer = true;
    public bool distractionAppeared = false;
    public bool tvTurnedOn = true;
    public bool switchingProgram = true;
    public string[] clientOrder = { "Boss", "Kaiju", "MrBusiness", "OtherPsychologist", "MsSodermalm", "MrFeminist", "MrAngerIssue" };
    public string clientNameEnd = "null";
    public int currentDist = 0;
    public int currentClient = 0;
    public int distrToBuy = 0;
    private bool playsCommercial = true;

    private bool ifRandomStart;

    private bool isSwitchingProgram = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (isSwitchingProgram)
            return;
        Debug.Log(
    $"call={clientsScript.callOngoing}, " +
    $"after={clientsScript.afterCallSatisf}, " +
    $"tv={tvTurnedOn}, " +
    $"commercial={clientsScript.commercial}, " +
    $"ent={entertainment}, " +
    $"switching={isSwitchingProgram}");

        if (clientsScript.restart)
        {
            RestartDistr();
            return;
        }

        //If it's Kaiju time, then we do:

        if (clientsScript.callOngoing == true && clientsScript.afterCallSatisf == false)
        {
            if (clientsScript.callOngoing && !isSwitchingProgram)
            {
                StartCoroutine(PlayAfterWhiteNoise(RandomEntertainment, maxWaitTime));
            }
        }
        else if (clientsScript.callOngoing == false && clientsScript.afterCallSatisf == true)
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
            StartCoroutine(PlayAfterWhiteNoise(() => Satisfaction(clientNameEnd), 8.0f));
        }
        else if (clientsScript.callOngoing == false && clientsScript.afterCallSatisf == false && tvTurnedOn == false && clientsScript.commercial == true)
        {
            clientsScript.commercial = false;
            StartCoroutine(PlayAfterWhiteNoise(Telemarket, maxWaitTime));
            Debug.Log("Telemarket done:" + entertainment);

        }
        else if (clientsScript.callOngoing == false && clientsScript.afterCallSatisf == false && tvTurnedOn == false && clientsScript.commercial == false && entertainment == false)
        {
            if (!isSwitchingProgram)
            {
                StartCoroutine(PlayAfterWhiteNoise(RandomEntertainment, maxWaitTime));
                Debug.Log("Ooops, you are in the ent if!:" + entertainment);
            }
        }


    }


    public void TurnOnTv()
    {

        if (tvTurnedOn == true)
        {
            tvTurnedOn = false;
        }



    }

    public void RandomEntertainment()
    {
        entertainment = true;
        ifRandomStart = true;
        int entProgram = UnityEngine.Random.Range(0, 6);
        PlayGifSound("/variety-show.gif");
        programScript.PlayGif(nameValuesEntertainment[entProgram]);
        StartCoroutine(EntProgramTimer(5.0f));

    }

    private void RestartDistr()
    {
        currentDist = 0;
        distrToBuy = 0;
        clientsScript.restart = false;
    }

    public void DistractionAppears()
    {
        if (distrToBuy >= nameValuesDistractions.Length)
            return;

        var newDistraction = nameValuesDistractions[distrToBuy];
        if (newDistraction != null)
        {
            newDistraction.SetActive(true);
        }
        distrToBuy = distrToBuy + 1;
    }
    private IEnumerator WhiteNoise()
    {
        Debug.Log("WhiteNoise started by " + Time.frameCount);
        ifRandomStart = false;
        programScript.PlayGif("/whitenoise.gif");

        PlayGifSound("/whitenoise.gif");

        yield return new WaitForSeconds(switchWaitTime);
        Debug.Log("WHITE NOISE " + Time.time);
    }
    public void Telemarket()
    {
        Debug.Log("Commercial started");
        ifRandomStart = false;
        telemChoiceTime = true;

        playsCommercial = true;
        programScript.PlayGif(nameValuesTelemarket[currentDist]);
        currentDist = currentDist + 1;
        StartCoroutine(TelProgramTimer());

        Debug.Log("Telemarket done, ent state:" + entertainment);

    }

    public void Satisfaction(string clientEnd)
    {
        ifRandomStart = false;

        entertainment = true;

        programScript.PlayGif(clientEnd);
        PlayGifSound(clientEnd);

        StartCoroutine(EntProgramTimer(8f));
    } 

    private void PlayGifSound(string gifName)
    {

        string clipName = System.IO.Path.GetFileNameWithoutExtension(gifName);
        Debug.Log("Loading: Audio/" + clipName);
        float t = Time.realtimeSinceStartup;

        AudioClip loadedClip = Resources.Load<AudioClip>("TV/" + clipName);

        Debug.Log("Load took: " + (Time.realtimeSinceStartup - t));
        if (loadedClip != null)
        {
            if (ifRandomStart == true)
            {
                audioSource.Stop();
                audioSource.clip = loadedClip;
                float randomTime = Random.Range(0f, audioSource.clip.length);

                // Set the playback head position
                audioSource.time = randomTime;

                // Start playback
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
                audioSource.clip = loadedClip;
               
                audioSource.Play();
            }
        }
        else { Debug.LogError("Couldn't load clip!"); }
    }

    IEnumerator TelProgramTimer()
    {
        float timeElapsed = 0f;
        // 3. Loop every frame until the button is pressed OR time runs out
        while (timeElapsed < maxWaitTime)
        { 
            // Increase timer by the time passed since the last frame
            timeElapsed += Time.deltaTime;
            if (distractionAppeared == false)
            {
                /*if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
                {*/
                    DistractionAppears();
                    distractionAppeared = true;
                //}
            }
            if (timeElapsed >= maxWaitTime)
                break;

            // Pause the coroutine until the next frame
            yield return null;
        }

        telemChoiceTimer = false;
        telemChoiceTime = false;
        //programScript.gifPath = "/black-screen.gif";
        distractionAppeared = false;


        yield break;
    }
    IEnumerator EntProgramTimer(float waitAfter)
    {
        yield return new WaitForSeconds(waitAfter);
        entertainment = false;
    }

    private IEnumerator PlayAfterWhiteNoise(System.Action nextAction, float waitAfter = 0f)
    {
        if (isSwitchingProgram)
        {
            Debug.Log("Ignored PlayAfterWhiteNoise - already switching");
            yield break;
        }

        isSwitchingProgram = true;

        Debug.Log("Switch START: " + Time.time);

        yield return WhiteNoise();

        nextAction?.Invoke();

        if (waitAfter > 0f)
            yield return new WaitForSeconds(waitAfter);

        Debug.Log("Switch END: " + Time.time);

        isSwitchingProgram = false;
    }
}
