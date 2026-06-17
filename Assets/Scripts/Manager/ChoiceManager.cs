using UnityEngine;
using UnityEngine.Windows;

public class ChoiceManager : MonoBehaviour
{
    public CallResponse[] _genericResponses;
    public CallResponse[] _positiveResponses;
    public CallResponse[] _negativeResponses;

    private Client _currentClient;
    private int _currentChoicesAmount;

    public string[] PresentFirstChoice()
    {
        _currentChoicesAmount = 4;
        string[] choices = new string[_currentChoicesAmount];

        for (int i = 0; i < choices.Length; i++)
            choices[i] = _currentClient.AvailableResponses[i].Response.Response;

        return choices;
    }

    public string[] PresentSecondChoice()
    {
        _currentChoicesAmount = 2;
        string[] choices = new string[_currentChoicesAmount];

        choices[0] = _positiveResponses[Random.Range(0, _positiveResponses.Length-1)].Response;
        choices[1] = _negativeResponses[Random.Range(0, _negativeResponses.Length-1)].Response;

        return choices;
    }

    public (int choice, bool approved) ListenToChoice(PlayerInput input)
    {
        bool approved = false;
        int choice = -1;

        if (input.Key1Down)
        {
            choice = 0;
        }
        else if (input.Key2Down)
        {
            choice = 1;
        }
        else if (input.Key3Down)
        {
            choice = 2;
        }
        else if (input.Key4Down)
        {
            choice = 3;
        }

        if (choice > _currentChoicesAmount - 1)
            choice = -1;

        if (choice >= 0)
        {
            if (_currentChoicesAmount == 4)
                approved = _currentClient.AvailableResponses[choice].LikesIt;
            else if (_currentChoicesAmount == 2)
                approved = choice == 0 && _currentClient.LikesFinalPositiveResponse;
        }

        return (choice, approved);
    }

    internal void Prepare(Client client)
    {
        _currentClient = client;
    }
}