using System.Collections;
using UnityEngine;

public class SpinSymbol : MonoBehaviour
{
    public LeanTweenType easeType;
    [SerializeField] public float spinDuration = 2;
    [HideInInspector] public static bool isSpinning;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpinning = false; // reel 1 not spinning yet
        GameControl.SpinButtonPressed += StartSpinning;
    }

    private void StartSpinning()
    {
        StartCoroutine("SpinReel1");
    }

    private IEnumerator SpinReel1()
    {
        isSpinning = true;

        string selectedSymbol1 = RandomNumberGenerator.SelectedSymbols[0];
        
        spinBaseAmount();
        
        
        
        isSpinning = false;
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
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -2, spinDuration).setEase(easeType);
                        break;
                    case 1:
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -3, spinDuration).setEase(easeType);
                        break;
                    case 2:
                        LeanTween.moveY(SymbolSpawner.symbolBatch1[i, j], -4, spinDuration).setEase(easeType);
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