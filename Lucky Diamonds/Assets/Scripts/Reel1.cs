using System.Collections;
using UnityEngine;

public class Reel1 : MonoBehaviour
{
    public LeanTweenType easeType;
    [HideInInspector] public bool firstReelStopped;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstReelStopped = true; // reel 1 not spinning yet
        GameControl.SpinButtonPressed += StartSpinning;
    }

    private void StartSpinning()
    {
        StartCoroutine("SpinReel1");
    }

    private IEnumerator SpinReel1()
    {
        firstReelStopped = false;

        string selectedSymbol1 = RandomNumberGenerator.SelectedSymbols[0];
        
        spinBaseAmount();
        
        
        
        firstReelStopped = true;
        yield return null;
    }

    private void spinBaseAmount()
    {
        spinBatch1();
    }

    private void spinBatch1()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (i)
                {
                    case 0:
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -2, 1).setEase(easeType);
                        break;
                    case 1:
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -3, 1).setEase(easeType);
                        break;
                    case 2:
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -4, 1).setEase(easeType);
                        break;
                }
                
            }
        }
    }
    
    private void OnDestroy()
    {
        GameControl.SpinButtonPressed -= StartSpinning;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
