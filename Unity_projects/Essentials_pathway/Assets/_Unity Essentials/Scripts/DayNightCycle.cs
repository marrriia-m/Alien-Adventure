using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("How many seconds should a full day take?")]
    public float dayLengthInSeconds = 120f; // Default: 2 minutes for a full rotation

    // Start angle and current time
    private float currentTime = 0f;

    void Update()
    {
        // Progress currentTime, loop when reaching a full day
        currentTime += Time.deltaTime;
        if (currentTime > dayLengthInSeconds)
        {
            currentTime -= dayLengthInSeconds;
        }

        // Calculate rotation (360 degrees per full day)
        float dayFraction = currentTime / dayLengthInSeconds;
        float rotationX = dayFraction * 360f;

        // Apply rotation (around X-axis)
        transform.rotation = Quaternion.Euler(rotationX, 170, 0); // Y=170 is the default Unity sun angle
    }
}
