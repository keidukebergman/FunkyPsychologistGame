
using System.Collections;
using UnityEngine;

public class DistractionsManager : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;

    [System.Serializable]
    public class Asset
    {
        public string name;
        public long price;
        public GameObject visual;
    }

    public Asset[] assets;

    private int currentIndex = 0;
    private bool canBuy = false;

    public float purchaseWindow = 5f;

    public void TriggerAssetOpportunity()
    {
        if (currentIndex >= assets.Length)
            return;

        StartCoroutine(PurchasePhase());
    }

    private IEnumerator PurchasePhase()
    {
        canBuy = true;

        Asset asset = assets[currentIndex];

        Debug.Log($"BUY WINDOW OPEN: {asset.name} for {asset.price}");

        float t = 0f;

        while (t < purchaseWindow)
        {
            t += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.B) && canBuy)
            {
                TryBuy(asset);
                break;
            }

            yield return null;
        }

        canBuy = false;
        currentIndex++;
    }

    private void TryBuy(Asset asset)
    {
        bool success = moneyManager.SpendMoney(asset.price);

        if (success && asset.visual != null)
        {
            asset.visual.SetActive(true);
        }

        Debug.Log($"Asset result: {asset.name}");
    }
}