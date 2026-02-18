using UnityEngine;

public class PersimpanganScript : MonoBehaviour
{
    public float platSpeed = 30f;         
    public float stopDeceleration = 40f;   
    public float stopZ = -237f;

    public float currentSpeed;
    public bool prefabSpawned = false;

    void Start()
    {
        currentSpeed = platSpeed;
        prefabSpawned = true;
    }

    void Update()
    {
        if (transform.position.z > stopZ)
        {
            currentSpeed = platSpeed;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                stopDeceleration * Time.deltaTime
            );
        }

        GameManager gm = FindObjectOfType<GameManager>();
        if(currentSpeed == 0 && gm.currentState == GameManager.GameState.Decision)
        {
            KabutMundurScript kms = FindObjectOfType<KabutMundurScript>();
            kms.isDecision = true;
        }

        transform.position += Vector3.back * currentSpeed * Time.deltaTime;

        if(transform.position.z <= -500)
        {
            Destroy(gameObject);
        }
    }
}
