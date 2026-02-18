using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float platSpeed = -20f;
    private float timer = 0f;
    public float timerTimer = 0f;
    public float posisiZ;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * platSpeed * Time.deltaTime;

        posisiZ = transform.position.z;

        if (timer <= timerTimer)
        {
            timer += Time.deltaTime;
        } else if (timer >= timerTimer)
        {
            Destroy(gameObject);
        }
    }



}
