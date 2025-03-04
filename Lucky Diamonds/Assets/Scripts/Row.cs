using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Row : MonoBehaviour
{
    private enum SymbolPositions
    {
        // y-positions of each symbol on the reel
        DiamondTop = -7,
        Crown = -5,
        Watermelon = -3,
        Bar = -1,
        Seven = 1,
        Cherry = 3,
        Lemon = 5,
        DiamondBottom = 7
    }
    
    private int _randomValue; // how long a reel will be spinning until it stops
    private float _timeInterval; // time between translations of reel positions while spinning
    
    public bool rowStopped; // if a reel is stopped
    public string stoppedSlot; // name of a symbol shown when reel stops

    private const int _UPPER_BOUND = -7; // y position of the top symbol of a reel
    private const int _LOWER_BOUND = 7; // y position of the bottom symbol of a reel
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rowStopped = true; // reels not spinning yet
        //GameControl.SpinButtonPressed += StartSpinning;
    }

    public void StartSpinning()
    {
        stoppedSlot = "";
        StartCoroutine("SpinReel");
    }

    private IEnumerator SpinReel()
    {
        rowStopped = false; // reels are now spinning
        _timeInterval = .01f;
        
        //string[] selectedSymbols = ;
        
        
        // "spins" reel at a constant speed of 56 iterations which is a multiple of 8 as there are 8 steps between each symbol
        for (int i = 0; i < 56; i++)
        {
            if (transform.position.y <= _UPPER_BOUND) // top of the reel reached
            {
                // "loop" back reel such that the lower symbol is displayed
                transform.position = new Vector3(transform.position.x, _LOWER_BOUND, 5);
            }

            // move reel down by .25 to simulate spinning animation
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, 5);

            yield return new WaitForSeconds(_timeInterval); // wait .01 seconds before "spinning" reel
        }
        
        //_randomValue = Random.Range(60, 100); // each row has its own spinning cycle

        /*  RNG must be a multiple of 8 because that is how many total steps of .25 there are between each symbol.
         *  Adjust RNG if not a multiple of 8.
         *  The denominator is 8 so there are 7 possible remainders; 0 <= r <= d - 1 (division algorithm)
         *  To figure out what to adjust by (x), use x = d - r (derived from the division algorithm) 
         */ 
        /*
        switch (_randomValue % 8)
        {
            case 1:
                _randomValue += 7; // x = 8 - 1 = 7
                break;
            case 2:
                _randomValue += 6; // x = 8 - 2 = 6
                break;
            case 3:
                _randomValue += 5; // x = 8 - 3 = 5
                break;
            case 4:
                _randomValue += 4; // x = 8 - 4 = 4
                break;
            case 5:
                _randomValue += 3; // x = 8 - 5 = 3
                break;
            case 6:
                _randomValue += 2; // x = 8 - 6 = 2
                break;
            case 7:
                _randomValue += 1; // x = 8 - 7 = 1
                break;
        }

        // reel starts to slow down based on a row's spinning cycle determined by RNG which is a multiple of 8 as that is
        // how many steps between each symbol
        for (int i = 0; i < _randomValue; i++)
        {
            if (transform.position.y <= _UPPER_BOUND) // top of the reel reached
            {
                // "loop" back reel such that the lower symbol is displayed
                transform.position = new Vector3(transform.position.x, _LOWER_BOUND, 5);
            }
            
            // move reel down by .25 to simulate spinning animation
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, 5);

            // as iteration grows, if it is greater than a portion of randomValue, increase time which slows down reel spin
            // separate ifs because the later ifs override the past ifs progressively increasing timeInterval thus slowing it down more
            if (i > Mathf.RoundToInt(_randomValue * .25f)) _timeInterval = .05f;
            if (i > Mathf.RoundToInt(_randomValue * .5f)) _timeInterval = .1f;
            if (i > Mathf.RoundToInt(_randomValue * .75f)) _timeInterval = .15f;
            if (i > Mathf.RoundToInt(_randomValue * .95f)) _timeInterval = .2f;

            yield return new WaitForSeconds(_timeInterval);
        }
        */

        /*
        switch (transform.position.y)
        {
            case (int) SymbolPositions.DiamondTop:
                stoppedSlot = "Diamond";
                break;
            case (int) SymbolPositions.Crown:
                stoppedSlot = "Crown";
                break;
            case (int) SymbolPositions.Watermelon:
                stoppedSlot = "Watermelon";
                break;
            case (int) SymbolPositions.Bar:
                stoppedSlot = "Bar";
                break;
            case (int) SymbolPositions.Seven:
                stoppedSlot = "Seven";
                break;
            case (int) SymbolPositions.Cherry:
                stoppedSlot = "Cherry";
                break;
            case (int) SymbolPositions.Lemon:
                stoppedSlot = "Lemon";
                break;
            case (int) SymbolPositions.DiamondBottom:
                stoppedSlot = "Diamond";
                break;
        }
*/
        
        rowStopped = true; // reel stopped spinning now
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
