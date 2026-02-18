using UnityEngine;

public class BatuDamage : MonoBehaviour
{
    public int damage = 1;
    public AudioClip soundNubruk;
    private bool isNubruk = false;

    void Update()
    {
        if(isNubruk == true)
        {
            BuangBatu();
        }

        GameManager gm = FindObjectOfType<GameManager>();
        if(gm.currentState == GameManager.GameState.Decision)
        {
            BatuNoHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        GameManager gm = FindObjectOfType<GameManager>();
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            if(gm.currentState == GameManager.GameState.Decision)
            {
                damage = 0;
            } else
            {
                health.TakeDamage(damage);
                isNubruk = true;

            if (soundNubruk != null)
            {
                AudioSource.PlayClipAtPoint(soundNubruk, transform.position);
            }
            }
        }
    }

    void BuangBatu()
    {
        BatuScript bs = FindObjectOfType<BatuScript>();
        transform.Translate(new Vector3(0, 0.5f, 0) * bs.speed * Time.deltaTime);
    }

    void BatuNoHit()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        BatuScript bs = FindObjectOfType<BatuScript>();
        
        damage = 0;
        transform.Translate(new Vector3(0, 0.5f, 0) * bs.speed * Time.deltaTime);
    }
}
