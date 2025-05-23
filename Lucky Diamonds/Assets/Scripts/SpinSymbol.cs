using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinSymbol : MonoBehaviour
{
    [SerializeField] public LeanTweenType easeType;
    [HideInInspector] public static bool isSpinning;
    
    private const int SPIN_DURATION = 3; // default 
    private const int SPIN_SPEED = 7;
    
    private int[] symbolSpawnCount = new int[3]; // how many symbols have spawned for a column/reel
    private int[] selectedSpawnNumber = new int[3]; // spawn count number indicating when to spawn the selected symbols for each column/reel
    private readonly float[] validPositions = { 1f, 0f, -1f }; // define absolute stop positions
    private int[] reelStopTimes;
    private bool[][] symbolStopped = new bool[3][]; // track stopped symbols individually
    
    // holds the selected symbols we spawned at respective slot to compare with RNG.selectedSymbols for 
    private string[] symbolSelected = new string[3]; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpinning = false; // reel 1 not spinning yet
        
        InputManager.SpinButtonPressed += StartSpinning; // subscribe to event

        
        
        for (int index = 0; index < 3; index++)
        {
            symbolStopped[index] = new bool[3];
        }
        
        reelStopTimes = ReelStopTimes(SPIN_DURATION);
        
        for (int i = 0; i < 3; i++)
        {
            selectedSpawnNumber[i] = (SPIN_SPEED * reelStopTimes[i]) - 1;
        }
    }

    private void StartSpinning()
    {
        symbolSpawnCount = new int[3]; // Reset spawn counts for all columns
        
        StartCoroutine("SpinReel1");
    }

    private IEnumerator SpinReel1()
    {
        isSpinning = true; // we are now spinning
        float spinTime = 0f; // elapsed spin time

        // new spin, symbols are not stopped
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                symbolStopped[i][j] = false;
            }
        }
        
        // actually start spinning
        while (spinTime < SPIN_DURATION)
        {
            //SymbolSpawner.selectedBatch[1, 0].transform.position += Vector3.down * (SPIN_SPEED * Time.deltaTime);
            
            // iterate through all symbols (all rows & columns) in a single frame.
            for (int i = 0; i < 3; i++)
            {
                // Repopulate List because we will remove a symbol after randomly instantiating it to prevent duplicate symbols on the same rows
                SymbolSpawner.availableSpritePrefabs = new List<GameObject>(SymbolSpawner.originalSymbolPrefabs);
                
                for (int j = 0; j < 3; j++)
                {
                    /*  If the stop time for a column (j) is finished, stop the symbols on this column (j).
                     *  Subtracting stop times by -0.01 as a tolerance or else the third column (j = 2) will not correctly stop.
                     *
                     *  If time is up for a column, we also need to check if the symbol is not stopped which means
                     *  we have not applied the tween effect yet. Otherwise, tweens will be applied every frame causing
                     *  an "out of available spaces for tweening" error.
                     *
                     *  Without !symbolStopped[i][j], the tween would be applied again every frame. I.e., If spinTime is still >= stopTimes[j] - 0.01,
                     *  the stopping condition is still true, but we already applied the tween in the previous frame. 
                     */
                    if (spinTime >= reelStopTimes[j] - 0.01 && !symbolStopped[i][j])
                    {
                        symbolStopped[i][j] = true; // symbol is now stopped
                        
                        ApplyBounceEffect(SymbolSpawner.symbolBatch1[i, j]); // apply tween for the symbol
                        
                        
                        //GameObject.Find("Machine").GetComponent<AudioSource>().Play();
                        
                        // play sfx only if the selected symbol that was spawned came from a winning RNG set
                        // won't play if symbolSet is "None" since that was set in the default case which meant no winning symbols
                        if (GameObject.Find("Machine") != null 
                            && RandomNumberGenerator.symbolSet == symbolSelected[j])
                        {
                            GameObject.Find("Machine").GetComponent<AudioSource>().Play();
                        }
                        
                        continue; // this column is now stopped, continue to the next column's iteration
                    }
                    
                    // move symbol downward only if the symbol is not stopped
                    if (!symbolStopped[i][j]) 
                    {
                        SymbolSpawner.symbolBatch1[i, j].transform.position += Vector3.down * (SPIN_SPEED * Time.deltaTime);
                    }
                    
                    // if a symbol reaches the bottom threshold (y = -1.5), destroy and replace it. 
                    // this is the minimum threshold for a symbol to be out of frame and where the two symbols above are only visible
                    if (SymbolSpawner.symbolBatch1[i, j].transform.position.y <= -1.5f)
                    {
                        // destroy old symbol which will be destroyed after the frame
                        Destroy(SymbolSpawner.symbolBatch1[i, j]);
                        
                        symbolSpawnCount[j]++; // Increase spawn count for this column
                        
                        GameObject selectedSymbolPrefab;
                        
                        /*  Selected symbol of a reel/column will normally be at y = 0 + (spinSpeed * reelStopTime).
                         *  For example, a spin speed of 2 with a reel stop time of 1 means our symbols travel a distance of 2
                         *  which when added to y-pos of the middle row (selected symbol row) of 0, gives y = 2.
                         * 
                         *  Since we are removing symbols at y = -1.5f and replacing them at y = 1.5 this means our selected symbol that
                         *  would normally be at y = 2 has moved down to y = 1.5 at this moment. This will be the 1st symbol that is
                         *  spawned at this y-pos for this reel/column.
                         *
                         *  This means our selected symbols will spawn at spawnCount = (spinSpeed * reelStopTime) - 1; y = (2 * 1) - 1 = 1.
                         *
                         *  Note that reelStopTime is given by the ReelStopTimes method.
                         */
                        if ((j == 0 && symbolSpawnCount[j] == 6)
                            || (j == 1 && symbolSpawnCount[j] == 13)
                            || (j == 2 && symbolSpawnCount[j] == 20))
                        {
                            selectedSymbolPrefab = SymbolSpawner.originalSymbolPrefabs.Find(prefab => prefab.name == RandomNumberGenerator.SelectedSymbols[j]);

                            if (selectedSymbolPrefab != null)
                            {
                                Debug.Log($"Selected Symbol: {selectedSymbolPrefab.name}");
                                symbolSelected[j] = selectedSymbolPrefab.name;

                            }
                            else
                            {
                                Debug.LogWarning($"Null selectedSymbolPrefab: {selectedSymbolPrefab}");
                            }
                            
                        }
                        else
                        {
                            // Randomly select a symbol
                            int randomSymbolIndex = Random.Range(0, SymbolSpawner.availableSpritePrefabs.Count);
                            selectedSymbolPrefab = SymbolSpawner.availableSpritePrefabs[randomSymbolIndex];

                            // Remove the random symbol from available pool to avoid duplicates
                            SymbolSpawner.availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        }
                        
                        // spawn a new random symbol at the top (y = 1.5)
                        Vector3 spawnPosition = new Vector3(SymbolSpawner.symbolBatch1[i, j].transform.position.x, 1.5f, SymbolSpawner.symbolBatch1[i, j].transform.position.z);
                        SymbolSpawner.symbolBatch1[i, j] = Instantiate(selectedSymbolPrefab, spawnPosition, Quaternion.identity);
                    }
                }
            }
            
            spinTime += Time.deltaTime; 
            
            yield return null; // wait until the next frame to spin all the symbols again
        }
        
        isSpinning = false; // all symbols are done spinning

        if (RandomNumberGenerator.isWin && GameObject.Find("ResultManager") != null)
        {
            GameObject.Find("ResultManager").GetComponent<AudioSource>().Play();
        }
        
        yield return null;
    }

    /*  Calculates the equal time intervals of a specified duration in seconds which will be the time each reel stops at.
     *  Since there are three reels, we will multiply the duration by 1/3, 2/3, 3/3 each representing the reel respectively.
     * 
     *  tᵢ = ( i / n ) × duration, for i = 1, 2, ..., n
     * 
     *  where:
     *  tᵢ = Stopping time for reel i
     *  n = Total number of reels
     *  duration = Total spin duration
     *
     *  Normal mode: 3 seconds
     *  Turbo mode: 1 second
     *
     *  Parameters:
     *  duration - Total spin duration. Should be a multiple of 3 since three total reels which evenly divides to calculate ints for reelStopTimes[].
     *
     *  Returns:
     *  3-length float array where each index contains the stopping time for its respective reel
     */
    private int[] ReelStopTimes(int duration)
    {
        if (duration != 3)
        {
            throw new ArgumentException($"Total spin duration must be 3 for normal mode, not \"{duration}\".", nameof(duration));
        }
        
        return new[] {duration / 3, duration * 2 / 3, duration};
    }

    /*  Applies an overshot bouncing tween effect on a symbol GameObject. Called when a column stops spinning. 
     *  Uses LeanTween library https://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html.
     *
     *  Parameters:
     *  symbol - GameObject of the symbol to tween
     * 
     */
    private void ApplyBounceEffect(GameObject symbol)
    {
        float currentY = symbol.transform.position.y; // y-position currently off by a few decimal places from the correct position
        float closestY = FindClosestPosition(currentY); // finds the closest correct y-position from its current y-position

        // Overshoot slightly below the final position
        float overshootY = closestY - 0.1f;

        // Tween to overshot position. Smaller time equals faster speed, Longer time equals slower speed. 
        LeanTween.moveY(symbol, overshootY, 0.1f).setOnComplete(() =>
        {
            // tween back to the closest correct y-position
            LeanTween.moveY(symbol, closestY, 0.1f).setEase(easeType);
        });
    }
    
    /*  Finds the closest valid y-position {-1, 0, 1} to the current y-position of a symbol.
     *  Allows us to move symbols to their correct positions because the symbols are slightly off after they are initially done spinning.
     *  For example, the middle rows should be at y=0, but they will be off by a few decimal places so a final correction is needed.
     *
     *  Parameters:
     *  currentY - current y-position of the symbol
     *
     *  Returns:
     *  closest y-position from the current symbol
     *  row 0: 1
     *  row 1: 0
     *  row 2: -1
     */
    private float FindClosestPosition(float currentY)
    {
        float closestY = validPositions[0]; // assume first element is the closest y position
        
        // current distance to closest y
        float distanceToClosestY = Mathf.Abs(closestY - currentY); // 1D axis distance = |y2 - y1|

        foreach (float targetY in validPositions)
        {
            float distanceToTargetY = Mathf.Abs(targetY - currentY); // distance to potentially new closest y
            
            // if our new closest distance is less than our current closest distance, then we found our new closest y-position
            if (distanceToTargetY < distanceToClosestY)
            {
                closestY = targetY; // swap old closest y with new closest y
                distanceToClosestY = distanceToTargetY; // swap old closest distance to new closest y distance
            }
        }
        return closestY;
    }
    
    private void OnDestroy()
    {
        InputManager.SpinButtonPressed -= StartSpinning;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}