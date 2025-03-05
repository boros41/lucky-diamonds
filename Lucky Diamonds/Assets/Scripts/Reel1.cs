using System.Collections;
using UnityEngine;

public class Reel1 : MonoBehaviour
{
    public LeanTweenType easeType;
    [SerializeField] public int spinTime = 2;
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
        //Debug.Log($"Reel 1 symbol: {selectedSymbol1}");

        LeanTween.moveY(gameObject, (float)SymbolString.SymbolPositions.DiamondTop, 1f)
                 .setOnComplete(
                     () =>
                     {
                         transform.position = new Vector3(transform.position.x, SymbolString.LOWER_BOUND, 5);
                         
                         int distance = GetDistanceToSymbol(SymbolString.LOWER_BOUND, SymbolString.SYMBOL_TO_POSITION[selectedSymbol1]);
                         int time = GetFinalSpinTime(distance, 3);

                         LeanTween.moveY(gameObject, SymbolString.SYMBOL_TO_POSITION[selectedSymbol1], time)
                                  .setEase(LeanTweenType.easeOutBack)
                                  .setOnComplete(() => firstReelStopped = true);
                         
                         
                     });
       

        //LeanTween.moveY(gameObject, SymbolString.SYMBOL_TO_POSITION[selectedSymbol1], 5f).setEase(easeType);


        
        /*
        // "spins" reel at a constant speed of 80 iterations which is a multiple of 8 as there are 8 steps between each symbol
        for (int i = 0; i < 80; i++)
        {
            if (transform.position.y <= SymbolString.UPPER_BOUND) // top of the reel reached
            {
                // "loop" back reel such that the lower symbol is displayed
                transform.position = new Vector3(transform.position.x, SymbolString.LOWER_BOUND, 5);
            }

            // move reel down by .25 to simulate spinning animation
            transform.position = new Vector3(transform.position.x, transform.position.y - SymbolString.SYMBOL_STEP, 5);

            yield return new WaitForSeconds(SymbolString.TIME_BETWEEN_SPIN); // wait .01 seconds before "spinning" reel
            //yield return null;
        }
        
        // while the reel is not at the selected symbol's position, spin more until it is.
        while (!Mathf.Approximately(transform.position.y, SymbolString.SYMBOL_TO_POSITION[selectedSymbol1]))
        {
            if (transform.position.y <= SymbolString.UPPER_BOUND) // top of the reel reached
            {
                // "loop" back reel such that the lower symbol is displayed
                transform.position = new Vector3(transform.position.x, SymbolString.LOWER_BOUND, 5);
            }
            
            // move reel down by .25 to simulate spinning animation
            transform.position = new Vector3(transform.position.x, transform.position.y - SymbolString.SYMBOL_STEP, 5);
            
            yield return new WaitForSeconds(SymbolString.TIME_BETWEEN_SPIN); // wait .01 seconds before "spinning" reel
            //yield return null;
        }
        */
        
        //firstReelStopped = true; // reel 1 is now stopped
        //Debug.Log("1st reel stopped!");
        
        yield return null;
    }

    private int GetDistanceToSymbol(int startSymbolY, int targetSymbolY)
    {
        return Mathf.Abs(targetSymbolY - startSymbolY);
    }

    private int GetFinalSpinTime(int distanceToSymbol, int spinSpeed)
    {
        return distanceToSymbol / spinSpeed;
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
