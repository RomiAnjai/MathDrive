using UnityEngine;

public class TruckScript2 : MonoBehaviour
{
    public float turnSpeed = 20.0f;
    public float autoCenterSpeed = 10f;
    public float centerX = 0f;

    private bool bisaDikontrol = true;

    void Update()
    {
        if (bisaDikontrol)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * turnSpeed * Time.deltaTime);
        }
        else
        {
            float newX = Mathf.MoveTowards(
                transform.position.x,
                centerX,
                autoCenterSpeed * Time.deltaTime
            );

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
    }
    public void SetControl(bool value)
    {
        bisaDikontrol = value;
    }
}
