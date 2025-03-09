using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static event Action SpinButtonPressed;
    public static event Action BetButtonPressed;
    
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI playAmountText;
    
    public static float PlayAmount = 1f; // float in case I add quarter play size
    //public static float PrizeValue;
    private bool _resultsChecked = false;
    
    public void OnSpinClick()
    {
        if (!SpinSymbol.isSpinning)
        {
            RandomNumberGenerator.CalculateSelectedSymbols();
            
            SpinButtonPressed?.Invoke(); // invoke event if there are subscribers/listeners (not null)
        }
    }

    public void OnBetClick()
    {
        if (!SpinSymbol.isSpinning)
        {
            switch (PlayAmount)
            {
                case 1:
                    PlayAmount = 5;
                    break;
                case 5:
                    PlayAmount = 10;
                    break;
                case 10:
                    PlayAmount = 100;
                    break;
                case 100:
                    PlayAmount = 1000;
                    break;
                case 1000:
                    PlayAmount = 1;
                    break;
            }
            
            playAmountText.text = $"{PlayAmount:C}";
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if rows are still spinning
        if (SpinSymbol.isSpinning)
        {
            //Debug.Log($"Reels still spinning, setting prize value to 0: 0");
            //RandomNumberGenerator.PrizeValue = 0;
            winText.enabled = false;
            _resultsChecked = false;
        }
        
        if (!SpinSymbol.isSpinning && !_resultsChecked)
        {
            _resultsChecked = true;
            winText.enabled = true;
            
            winText.text = $"{RandomNumberGenerator.PrizeValue:C}";
        }
    }
}
