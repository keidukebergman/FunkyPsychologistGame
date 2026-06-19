using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public long money = 9000; // start ridiculous by default

    public void AddMoney(long amount)
    {
        money += amount;
        Debug.Log($"+{amount} money | Total: {money}");
    }

    public bool SpendMoney(long amount)
    {
        if (money < amount)
        {
            Debug.Log("Damn it!");
            return false;
        }

        money -= amount;
        Debug.Log($"-{amount} money | Total: {money}");
        return true;
    }
}

/* define rewards in call script like

[SerializeField] private MoneyManager moneyManager;
[SerializeField] private long q1CorrectReward = 5000;
[SerializeField] private long q2CorrectReward = 20000;

Then call function

public void ResolveCall(bool q1Correct, bool q2Correct)
{
    if (q1Correct)
    {
        moneyManager.AddMoney(q1CorrectReward);
    }

    if (q2Correct)
    {
        moneyManager.AddMoney(q2CorrectReward);
    }
}


Smth after phone call

distractionsManager.TriggerAssetOpportunity();
*/