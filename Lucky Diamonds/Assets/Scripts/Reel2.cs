using System.Collections;
using UnityEngine;

public class Reel2 : MonoBehaviour
{
    [HideInInspector] public bool secondReelStopped;
    [SerializeField] private Reel1 reel1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondReelStopped = true; // reel 2 not spinning yet
        GameControl.SpinButtonPressed += StartSpinning;
    }
    
    private void StartSpinning()
    {
        StartCoroutine("SpinReel2");
    }

    private IEnumerator SpinReel2()
    {
        secondReelStopped = false;
        
        string selectedSymbol2 = RandomNumberGenerator.SelectedSymbols[1];
        //Debug.Log($"Reel 2 symbol: {selectedSymbol2}");

        while (!reel1.firstReelStopped)
        {
            bool tweenFinished = false;
            
            LeanTween.moveY(gameObject, (float)SymbolString.SymbolPositions.DiamondTop, 1f)
                .setOnComplete(
                    () =>
                    {
                        transform.position = new Vector3(transform.position.x, SymbolString.LOWER_BOUND, 5);
                        tweenFinished = true;
                    });

            yield return new WaitUntil(() => tweenFinished);
        }
        
        int distance = GetDistanceToSymbol((int) transform.position.y, SymbolString.SYMBOL_TO_POSITION[selectedSymbol2]);
        int time = GetFinalSpinTime(distance, 3);

        LeanTween.moveY(gameObject, SymbolString.SYMBOL_TO_POSITION[selectedSymbol2], time)
            .setEase(LeanTweenType.easeOutBack)
            .setOnComplete(() => secondReelStopped = true);
        
        
        /*
        while (!reel1.firstReelStopped)
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
        
        /*
        // "spins" reel at a constant speed of 80 iterations which is a multiple of 8 as there are 8 steps between each symbol. 
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
        */
        
        /*
        // while this reel is not at the selected symbol's position and 1st reel stopped spinning
        while (!Mathf.Approximately(transform.position.y, SymbolString.SYMBOL_TO_POSITION[selectedSymbol2]))
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
        
        secondReelStopped = true; // reel 2 is now stopped
        //Debug.Log("2nd reel stopped!");
        */
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
