using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SectorScore
{
    public Collider2D collider;
    public int score;
}

public class WheelManager : MonoBehaviour
{
    public Button spinButton; // Reference to the button that spins the wheel
    public Transform wheelTransform; // Reference to the transform of the wheel object
    public float initialSpinForce = 1000f; // The initial force to apply when spinning the wheel
    public GameObject needle; // Reference to the needle object
    private float spinForce; // The current force applied when spinning the wheel
    private bool isSpinning = false;
    private float spinTimer = 0f;
    private float spinDuration = 10f; // Duration of the wheel spin in seconds

    public List<SectorScore> sectorScores;


    void Start()
    {
        // Add an onClick event listener to the button
        spinButton.onClick.AddListener(SpinWheel);
    }

    void Update()
    {
        if (isSpinning)
        {
            // Rotate the wheel gradually
            float step = spinForce * Time.deltaTime;
            wheelTransform.Rotate(Vector3.back, step);

            // Update the spin timer
            spinTimer += Time.deltaTime;
            if (spinTimer >= spinDuration)
            {
                // Stop spinning after the duration has passed
                isSpinning = false;
                spinTimer = 0f;

                // Determine score based on the needle's position
                int score = DetermineScore();
                Debug.Log("Score: " + score);
            }
            else
            {
                // Gradually reduce the spin force to simulate deceleration
                spinForce -= (initialSpinForce / spinDuration) * Time.deltaTime;
                if (spinForce < 0f)
                {
                    spinForce = 0f;
                }
            }
        }
    }

    void SpinWheel()
    {
        if (!isSpinning)
        {
            // Set spinning flag to true
            isSpinning = true;
            spinForce = initialSpinForce; // Reset spin force
        }
    }

    int DetermineScore()
    {
        foreach (var sector in sectorScores)
        {
            if (sector.collider.OverlapPoint(needle.transform.position))
            {
                return sector.score;
            }
        }
        return 0; // Default score if needle doesn't overlap with any sector
    }

}
