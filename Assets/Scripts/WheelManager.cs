using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public Button spinButton; // Reference to the button that spins the wheel
    public Transform wheelTransform; // Reference to the transform of the wheel object
    public float initialSpinForce = 1000f; // The initial force to apply when spinning the wheel
    private float spinForce; // The current force applied when spinning the wheel
    private bool isSpinning = false;
    private float spinTimer = 0f;
    private float spinDuration = 10f; // Duration of the wheel spin in seconds

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
}
