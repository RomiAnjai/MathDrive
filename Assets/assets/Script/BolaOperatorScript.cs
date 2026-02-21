using UnityEngine;

public class BolaOperatorScript : MonoBehaviour
{
    public float speed = -20f;
    private float timer = 0f;
    public float timerTimer;

    
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

    private void OnTriggerEnter(Collider other) 
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm.sudahTriggerBola == true) return;

        if (other.CompareTag("TutorialLine"))
        {
            gm.sudahTriggerBola = true;
            Time.timeScale = 0f;
            gm.BolaTutorial();
        }
    }
}
