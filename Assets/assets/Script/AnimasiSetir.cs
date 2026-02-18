using UnityEngine;

public class AnimasiSetir : MonoBehaviour
{
    public float maxRotation = 90f;     // derajat maksimal kiri/kanan
    public float smoothSpeed = 8f;

    private float currentRotation = 0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float targetRotation = horizontalInput * maxRotation;

        currentRotation = Mathf.Lerp(
            currentRotation,
            targetRotation,
            Time.deltaTime * smoothSpeed
        );

        transform.localRotation = Quaternion.Euler(-24.509f, 88.46f, currentRotation);
    }
}
