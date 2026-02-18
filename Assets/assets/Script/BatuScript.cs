using UnityEngine;

public class BatuScript : MonoBehaviour
{
    public float speed = -20f;
    private float timer = 0f;
    public float timerTimer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (timer <= timerTimer)
        {
            timer += Time.deltaTime;
        } else if (timer >= timerTimer)
        {
            Destroy(gameObject);
        }
    }
}
