using UnityEngine;

public class BolaOperatorScript : MonoBehaviour
{
    public float speed = -20f;
    private float timer = 0f;
    public float timerTimer;

    // Update is called once per frame
    void Update()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (timer <= timerTimer)
        {
            timer += Time.deltaTime;
        } else if (timer >= timerTimer)
        {
            Destroy(gameObject);
        }

        if(gm.currentState == GameManager.GameState.Decision)
        {
            transform.Translate(new Vector3(0, 0.5f, 0) * speed * Time.deltaTime);
        }
    }
}
