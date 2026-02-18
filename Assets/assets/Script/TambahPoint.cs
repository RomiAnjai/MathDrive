using UnityEngine;

public class TambahPoint : MonoBehaviour
{
    public int plusPoint;
    public AudioClip pickupSound;
    private bool isHit = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isHit == true)
        {
            BuangBola();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHit = true;
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }


            PlayerPoint point = other.GetComponent<PlayerPoint>();
            if (point != null)
            {
                point.AddPoint(plusPoint);
            }

            Destroy(gameObject, 0.5f);
        }
    }

    void BuangBola()
    {
        BolaOperatorScript bos = FindObjectOfType<BolaOperatorScript>();
        transform.Translate(new Vector3(0, 0.5f, 0) * bos.speed * Time.deltaTime);
    }
}
