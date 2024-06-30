using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 10f;
    float timer = 0f;
    float checkDelay = 3f;
    bool isTimerRunning = false;

    public int damage { get; private set;}

    private void Awake()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        timer = 0f;
        isTimerRunning = true;
    }

    public void SetDamage(int dmg)
    {
        this.damage = dmg;
    }

    public void SetSpeed(float bonusSpeed)
    {
        moveSpeed = bonusSpeed;
    }


    void Update()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);

        if (isTimerRunning)
        {
            // Increment the timer by the time elapsed since the last frame
            timer += Time.deltaTime;

            // If the timer has reached the delay time
            if (timer >= checkDelay)
            {
                isTimerRunning = false; // Stop the timer

                // Check if the object is active
                if (gameObject.activeSelf)
                {
                    // Destroy the game object if it is active
                    Destroy(gameObject);
                }
            }
        }
    }
}
