using UnityEngine;

public class FreeCamController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 3f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.unscaledDeltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.unscaledDeltaTime;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            move += transform.forward;

        if (Input.GetKey(KeyCode.S))
            move -= transform.forward;

        if (Input.GetKey(KeyCode.D))
            move += transform.right;

        if (Input.GetKey(KeyCode.A))
            move -= transform.right;

        transform.position += move * moveSpeed * Time.unscaledDeltaTime;
    }
}
