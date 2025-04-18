using UnityEngine;
using UnityEngine.UI;

public class ChargeBarUIController : MonoBehaviour
{
    [SerializeField] private Image chargeBarImage;
    [SerializeField] private Color startColor = Color.yellow;
    [SerializeField] private Color endColor = Color.red;
    private bool isCharging = false;
    private float chargeStartTime;
    // private float minChargeTime = 0f; // Corresponds to minLaunchVelocity
    private float maxChargeTime = 4f; // Corresponds to maxLaunchVelocity - minLaunchVelocity (5 - 1)

    // Optional: Reference to the player control script for velocity info
    [SerializeField] private playerControlScript playerControl;

    void Start()
    {
        if (chargeBarImage == null)
        {
            Debug.LogError("ChargeBarImage not assigned in the Inspector!");
            enabled = false;
            return;
        }
        chargeBarImage.gameObject.SetActive(true); // Hide the UI initially
    }

    void Update()
    {
        if (isCharging)
        {
            float chargeDuration = Time.time - chargeStartTime;
            float normalizedCharge = Mathf.Clamp01(chargeDuration / maxChargeTime);

            // Update fill amount (top to bottom)
            chargeBarImage.fillAmount = normalizedCharge;

            // Update color based on the normalized charge
            chargeBarImage.color = Color.Lerp(startColor, endColor, normalizedCharge);
        }
    }

    public void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
        chargeBarImage.fillAmount = 0f;
        chargeBarImage.color = startColor;
        chargeBarImage.gameObject.SetActive(true);
    }

    public void StopCharging()
    {
        isCharging = false;
        chargeBarImage.gameObject.SetActive(false);
    }
}