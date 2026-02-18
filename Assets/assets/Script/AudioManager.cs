using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource environmentSFX;

    private void Awake() 
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        backgroundMusic.Play();
        environmentSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
