using TMPro;
using UnityEngine;

public class MoneyVault : GameAction
{
    public float coins;
    public TextMeshPro moneyDisplay;
    public string vaultName = "";

    public override void Activate()
    {
        coins += 25;
    }

    public override void Deactivate()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int thing = Plus1(6);
        UpdateMoney();
    }

    int Plus1 (int number)
    {
        return number+1;
    }
    void UpdateMoney()
    {
        coins = coins - (1 * Time.deltaTime);
        moneyDisplay.text = vaultName + "\n" + coins.ToString();
    }
}
