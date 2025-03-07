using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSymbol : MonoBehaviour
{
    [SerializeField] public LeanTweenType easeType;
    [HideInInspector] public static bool isSpinning;
    
    private const int SPIN_DURATION = 3;
    private const float SPIN_SPEED = 2f;
    
    private readonly float[] validPositions = { 1f, 0f, -1f }; // define absolute stop positions
    private float[] stopTimes; // stop times for each reel
    private bool[][] symbolStopped = new bool[3][]; // track stopped symbols individually
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpinning = false; // reel 1 not spinning yet
        
        GameControl.SpinButtonPressed += StartSpinning; // subscribe to event
        
        for (int index = 0; index < 3; index++)
        {
            symbolStopped[index] = new bool[3];
        }
        
        stopTimes = ReelStopTimes(SPIN_DURATION);
        
    }

    private void StartSpinning()
    {
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

        //string selectedSymbol1 = RandomNumberGenerator.SelectedSymbols[0];
        
        // actually start spinning
        while (spinTime < SPIN_DURATION)
        {
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
                    if (spinTime >= stopTimes[j] - 0.01 && !symbolStopped[i][j])
                    {
                        symbolStopped[i][j] = true; // symbol is now stopped
                        ApplyBounceEffect(SymbolSpawner.symbolBatch1[i, j]); // apply tween for the symbol
                        continue; // this column is now stopped, continue to the next column's iteration
                    }
                    
                    // move symbol downward only if the symbol is not stopped
                    if (!symbolStopped[i][j]) 
                    {
                        SymbolSpawner.symbolBatch1[i, j].transform.position += Vector3.down * (SPIN_SPEED * Time.deltaTime);
                    }
                    
                    // if a symbol reaches the bottom threshold (y = -1.5), destroy and replace it
                    if (SymbolSpawner.symbolBatch1[i, j].transform.position.y <= -1.5f)
                    {
                        // destroy the old symbol
                        Destroy(SymbolSpawner.symbolBatch1[i, j]);
                        
                        // randomly select symbol
                        int randomSymbolIndex = Random.Range(0, SymbolSpawner.availableSpritePrefabs.Count);
                        GameObject selectedSymbol = SymbolSpawner.availableSpritePrefabs[randomSymbolIndex];

                        // spawn a new random symbol at the top (y = 1.5)
                        Vector3 spawnPosition = new Vector3(SymbolSpawner.symbolBatch1[i, j].transform.position.x, 1.5f, SymbolSpawner.symbolBatch1[i, j].transform.position.z);
                        SymbolSpawner.symbolBatch1[i, j] = Instantiate(selectedSymbol, spawnPosition, Quaternion.identity);
                        
                        // remove the random symbol we spawned to prevent spawning the same symbol on the same rows
                        SymbolSpawner.availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                    }
                }
            }

            spinTime += Time.deltaTime; 
            
            yield return null; // wait until the next frame to spin all the symbols again
        }
        
        isSpinning = false; // all symbols are done spinning
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
     *  Parameters:
     *  duration - total spin duration
     *
     *  Returns:
     *  3-length float array where each index contains the stopping time for its respective reel
     */
    private float[] ReelStopTimes(int duration)
    {
        return new[] {duration / 3f, duration * 2 / 3f, duration};
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
        GameControl.SpinButtonPressed -= StartSpinning;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}