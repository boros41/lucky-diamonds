using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static event Action SpinButtonPressed;
    
    [SerializeField] private TextMeshProUGUI prizeText;
    
    public static float BetAmount = 1f; // rename this to playAmount
    //public static float PrizeValue;
    private bool _resultsChecked = false;
    
    public void OnMouseClick()
    {
        if (!SpinSymbol.isSpinning)
        {
            RandomNumberGenerator.CalculateSelectedSymbols();
            
            SpinButtonPressed?.Invoke(); // invoke event if there are subscribers/listeners (not null)
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
            prizeText.enabled = false;
            _resultsChecked = false;
        }
        
        if (!SpinSymbol.isSpinning && !_resultsChecked)
        {
            _resultsChecked = true;
            prizeText.enabled = true;
            
            prizeText.text = $"{RandomNumberGenerator.PrizeValue:C}";
        }
    }
}
