using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Instance;
    public int betAmountIndex;
    public List<float> betAmountTable;
    public float _betAmount;
    public static event System.Action<int> OnBetIndexChanged;
    public float betAmount
    {
        get { return _betAmount; }
        set
        {
            _betAmount = value;
            UI.Instance.betText.text = "DEM " + _betAmount.ToString("F0");
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseBetAmount()
    {
        if (betAmountIndex < betAmountTable.Count - 1)
        {
            if (GameManager.Instance.money > betAmountTable[betAmountIndex])
            {
                betAmountIndex++;
                betAmount = betAmountTable[betAmountIndex];
            }
        }
        OnBetIndexChanged?.Invoke(betAmountIndex);
    }

    public void DecreaseBetAmount()
    {
        if (betAmountIndex > 0)
        {
            betAmountIndex--;
            betAmount = betAmountTable[betAmountIndex];
        }
        OnBetIndexChanged?.Invoke(betAmountIndex);
    }

    public void MinBetAmount()
    {
        betAmountIndex = 0;
        betAmount = betAmountTable[betAmountIndex];

        OnBetIndexChanged?.Invoke(betAmountIndex);
    }

    public void MaxBetAmount()
    {
        Debug.Log("fixing max bet");
        betAmountIndex = FitMaxBet(betAmountTable.Count - 1);
        betAmount = betAmountTable[betAmountIndex];

        OnBetIndexChanged?.Invoke(betAmountIndex);
    }

    public int FitMaxBet(int index)
    {

        if (GameManager.Instance.money >= 1f)
        {
            if (GameManager.Instance.money >= betAmountTable[index])
            {
                return index;
            }
            else
            {
                return FitMaxBet(index - 1);
            }
        }
        return 0;
    }
}
