using System.Collections;
using UnityEngine;

public class MageBehaviour : MonoBehaviour
{
    public int health = 100;
    public float damageCooldown = 3f; // Cooldown duration in seconds
    public float blinkDuration = 1f; // Duration of the blink effect in seconds
    public float blinkInterval = 0.1f; // Interval between each blink in seconds
    private bool canTakeDamage = true; // Flag indicating if damage can be taken
    private Renderer characterRenderer; // Reference to the character's renderer components

    private void Start()
    {
        characterRenderer = GetComponent<Renderer>();
        UiManager.Instance.UpdateHealth(health);
    }

    private void Update()
    {
        // Check if damage cooldown is active
        if (!canTakeDamage)
        {
            // Decrease the cooldown timer
            damageCooldown -= Time.deltaTime;

            // Check if the cooldown has ended
            if (damageCooldown <= 0f)
            {
                // Enable taking damage again
                canTakeDamage = true;
                damageCooldown = 3f; // Reset the cooldown duration
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Check if damage can be taken
        if (canTakeDamage)
        {
            health -= damage;
            canTakeDamage = false; // Disable taking damage temporarily

            StartCoroutine(BlinkCharacter());
        }

        if(health <= 0)
        {
            GameManager.Instance.GameLost();
        }

        UiManager.Instance.UpdateHealth(health);
    }

    private IEnumerator BlinkCharacter()
    {
        // Store the initial visibility state of the character
        bool initialVisibility = characterRenderer.enabled;

        // Perform the blinking effect for the specified duration
        float blinkTimer = 0f;
        while (blinkTimer < blinkDuration)
        {
            // Toggle the visibility of the character
            characterRenderer.enabled = !characterRenderer.enabled;

            // Wait for the blink interval
            yield return new WaitForSeconds(blinkInterval);

            blinkTimer += blinkInterval;
        }

        // Restore the initial visibility state of the character
        characterRenderer.enabled = initialVisibility;
    }


}
