using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Platform Adalah")]
    public GameObject platform;
    private float platTimer;
    private float spawnRatePlat = 3.2f;
    [Header("Batu Adalah")]
    public Transform[] posisiBatu;
    public GameObject batuRintangan;
    public float spawnRate = 0;
    [Header("Bola Operator Wes Pokoke")]
    public GameObject bolaOperator;
    public GameObject[] jenisOperator;
    public float spawnRatePoin;
    public float poinTimer;
    [Header("Persimpangan Meow Meow")]
    public GameObject jalanPersimpangan;
    public float spawnZPersimpangan = 40f;
    private int jumlahPersimpangan;
    [Header("wave")]
    public int wave;
    public int waveNow = 1;

    [Header("Sistem Bintang")]
    public FadeImage bintang1;
    public FadeImage bintang2;
    public FadeImage bintang3;
    private float showJumlahBintang;

    [Header("Tombol Tombol an")]
    public FadeImage tombolLanjut;
    public FadeImage tombolRestart;
    public FadeImage tombolRestartHolder;
    public GameObject tombolLanjutObject;
    public GameObject tombolRestartObject;

    [Header("Mboh opo iki")]
    public GameObject gameOver;
    private float timer;
    private float timerGameManager;
    public int playTime;
    private int randomBatu;
    private int randomPoin;
    private bool kondisiDecision = false;
    public bool sudahMenang = false;

    [SerializeField] AudioSource awokawok;
    [SerializeField] AudioSource awokawok2;
    private int jos = 0;
    void Start()
    {
        Instantiate(platform, transform.position + new Vector3(14.05f, 13.84f, 20f), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(batuRintangan, posisiBatu[1].position, posisiBatu[1].rotation);
        spawnRate += 1;
    }

    // Update is called once per frame
    void Update()
    {
        SoalScript ss = FindObjectOfType<SoalScript>();
        if (currentState == GameState.Running)
        {
            PlatformSpawner();
            BatuSpawner();
            BolaPoinSpawner();
        }

        if (kondisiDecision == true && jumlahPersimpangan == 0)
        {
            SpawnPersimpangan();
        }

        if(currentState == GameState.Running)
        {
            if(timerGameManager <= playTime)
                {
                    timerGameManager += Time.deltaTime;
                    Debug.Log(timerGameManager);
                } else
                {
                    EnterDecisionMode();
                    timerGameManager = 0;
                }
        }

        if(ss.whatIsThisVariableForQuestionMark >= 4 && jos == 0)
        {
            awokawok2.Stop();
            awokawok.Play();
            jos++;
        } else if (jos > 0)
        {
            IkiPensCak();
            ss.whatIsThisVariableForQuestionMark = 0;
            jos = 0;
        }
    }


    public enum GameState
    {
        Running,
        Decision
    }

    public GameState currentState = GameState.Running;

    public void BatuSpawner()
    {
        if (timer <= spawnRate)
        {
            timer = (timer + Time.deltaTime) * spawnRate;
        } else 
        {
            randomBatu = Random.Range(0, posisiBatu.Length);

            Instantiate(batuRintangan, posisiBatu[randomBatu].position, posisiBatu[randomBatu].rotation);
            timer = 0f;
            Debug.Log("Good");
        }
    }

    public void PlatformSpawner()
    {
        if(platTimer < spawnRatePlat)
        {
            platTimer += Time.deltaTime;
        }
        else
        {
           Instantiate(platform, transform.position + new Vector3(14.05f, 13.84f, 40f), Quaternion.Euler(0f, 0f, 0f));
           platTimer = 0f;
        }
    }

    public void BolaPoinSpawner()
    {
        if (poinTimer < spawnRatePoin)
        {
            poinTimer += Time.deltaTime;
        } else {
        do
        {
            randomPoin = Random.Range(0, posisiBatu.Length);
        }
        while (randomPoin == randomBatu);

        int jenisOperasi = Random.Range(0, jenisOperator.Length);

        Instantiate(jenisOperator[jenisOperasi], posisiBatu[randomPoin].position, posisiBatu[randomPoin].rotation);

        poinTimer = 0f;
        }
    }

    public void EnterDecisionMode()
    {
        if (currentState == GameState.Decision) return;

        currentState = GameState.Decision;
        kondisiDecision = true;

        TruckScript2 truck = FindObjectOfType<TruckScript2>();

        if (truck != null)
        {
            truck.SetControl(false);
        }

        Debug.Log("Persimpangan");

        SpawnPersimpangan();
    }

    public void SpawnPersimpangan()
    {
        PlatformScript ps = FindObjectOfType<PlatformScript>();

        if(ps.posisiZ <= -40)
        {
            jumlahPersimpangan++;
            Vector3 spawnPos = transform.position + new Vector3(
            -7.38f,
            -2.4f,
            spawnZPersimpangan + -265f
        );

        Instantiate(jalanPersimpangan, spawnPos, Quaternion.Euler(0f, 90f, 0f));
        }
    }

    public void KembaliKeRunning()
    {
        currentState = GameState.Running;
        PersimpanganScript pss =  FindObjectOfType<PersimpanganScript>();
        KabutMundurScript kms = FindObjectOfType<KabutMundurScript>();
        PlayerPoint ppoint = FindObjectOfType<PlayerPoint>();
        TruckScript2 truck = FindObjectOfType<TruckScript2>();
        SoalScript ss = FindObjectOfType<SoalScript>();

        if (truck != null)
        {
            truck.SetControl(true);
        }

        if(waveNow <= wave && ss.jawabanBenar == ss.hehe)
        {
            ppoint.targetPoint += 1000;
            waveNow++;
        }

        kms.isDecision = false;
        ppoint.rekamanPoin = 0;
        timerGameManager = 0;
        pss.stopZ = -510;
        pss.currentSpeed = 30f;
        jumlahPersimpangan = 0;
        currentState = GameState.Running;
        Debug.Log("GAME LANJUT");
    }

    public void Kemenangan()
    {
        StartCoroutine(SequenceKemenangan());
    }

    IEnumerator SequenceKemenangan()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        sudahMenang = true;
        WinPopupAnimation popup = FindObjectOfType<WinPopupAnimation>();
        PlayerPoint ppoint = FindObjectOfType<PlayerPoint>();
        popup.PlayAnimation();
        showJumlahBintang = (float)ppoint.totalPoint / ppoint.maxPoint;
        Debug.Log("Total: " + ppoint.totalPoint);
        Debug.Log("Max Point: " + ppoint.maxPoint);
        Debug.Log("Rasio: " + showJumlahBintang);
        Time.timeScale = 0f;
        ShowBintang();
    }

    void IkiPensCak()
    {
        StartCoroutine(Pensjos());
    }

    IEnumerator Pensjos()
    {
        yield return new WaitUntil (() => !awokawok.isPlaying);
        awokawok2.Play();
    }

    void ShowBintang()
    {
        StartCoroutine(SequenceBintang());
    }

    IEnumerator SequenceBintang()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if(showJumlahBintang >= 0.33f)
        {
            bintang1.FadeIn();
        }
        yield return new WaitForSecondsRealtime(1f);
        if(showJumlahBintang >= 0.66f)
        {
            bintang3.FadeIn();
        }
        yield return new WaitForSecondsRealtime(1.75f);
        if(showJumlahBintang >= 0.92f)
        {
            bintang2.FadeIn();
        }

        StartCoroutine(MunculkanTombol());
    }

    IEnumerator MunculkanTombol()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        tombolLanjutObject.SetActive(true);
        tombolRestartObject.SetActive(true);
        tombolLanjut.FadeIn();
        tombolRestart.FadeIn();
        yield return new WaitForSecondsRealtime(0.1f);
        tombolRestartHolder.FadeIn();
    }
}
