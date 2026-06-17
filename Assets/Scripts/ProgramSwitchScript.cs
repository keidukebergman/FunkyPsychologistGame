using UnityEngine;

public class ProgramSwitchScript : MonoBehaviour
{
    public bool callOngoing = false;
    public bool afterCallSatisf = false;
    public bool satisfaction = false;
    public bool commercial = false;

    public void SwitchToNews()
    {
        callOngoing = false;
        afterCallSatisf = true;
    }

    public void SwitchToCommercial()
    {
        callOngoing = false;
        satisfaction = false;
        commercial = true;
    }

    public void SwitchToEntertainment()
    {
        callOngoing = false;
        afterCallSatisf = false;
        satisfaction = false;
        commercial = false;
    }
}
