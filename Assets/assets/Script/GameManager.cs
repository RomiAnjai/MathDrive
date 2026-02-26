using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("Tutorial")]
    public TMP_Text TutorialText;
    public GameObject TutorialBox;
    public SoalScript ss;
    public FadeImage blackScreen;

    [Header("Mboh opo iki")]
    public bool isTutorial = true;
    public bool sedangTutorialBatu;
    public bool sedangTutorialBola;
    public bool sedangTutorialPoin;
    public bool sedangTutorialPersimpangan;
    public bool sedangTutorialSoal;
    public bool sudahTriggerBatu = false;
    public bool sudahTriggerBola = false;
    public bool sudahTriggerPersimpangan;
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
        if(isTutorial == true)
        {
            Time.timeScale = 0f;
            WelcomeText();
        }
        Instantiate(platform, transform.position + new Vector3(14.05f, 13.84f, 20f), Quaternion.Euler(0f, 0f, 0f));
        Instantiate(batuRintangan, posisiBatu[3].position, posisiBatu[3].rotation);
        spawnRate += 1;

        SoalScript ss = FindObjectOfType<SoalScript>();
        TruckScript2 ts2 = FindObjectOfType<TruckScript2>();
        PlayerPoint ppoint = FindObjectOfType<PlayerPoint>();
        PersimpanganScript ps = FindObjectOfType<PersimpanganScript>();
        BatuScript bs = FindObjectOfType<BatuScript>();

        if(SceneManager.GetActiveScene().name != "LevelTutorial")
        {
            isTutorial = false;
            ss.yesTutorial = false;
            ts2.sedangTutorial = false;
            ppoint.yesTutorial = false;
            TriggerSmoothTimeScale();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Running)
        {
            PlatformSpawner();
            BatuSpawner();
            BolaPoinSpawner();
        }

        // Bagian tutorial pokok e

        if (isTutorial == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TutorialBox.SetActive(false);
                TriggerSmoothTimeScale();
                TutorialText.fontSize = 49.5f;
            }
        }

        if(sedangTutorialBatu == true)
        {
            BatuTutorial();
        }

        if(sedangTutorialBola == true)
        {
            BolaTutorial();
        }
        
        if(sedangTutorialPoin == true)
        {
            PoinTutorial();
        }

        if(sedangTutorialPersimpangan == true)
        {
            PersimpanganTutorial();
        }

        if(sedangTutorialSoal == true)
        {
            SoalTutorial();
        }

        // Akhir dari bagian tutorial

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
        if(isTutorial == false)
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

        if(SceneManager.GetActiveScene().name != "LevelTutorial")
        {
            StartCoroutine(MunculkanTombol());
        } else
        {
            yield return new WaitForSecondsRealtime(1f);
            blackScreen.FadeIn();
            yield return new WaitForSecondsRealtime(2f);
            TutorialBox.SetActive(true);
            TutorialText.color = Color.white;
            TutorialText.text = "YOU WIN // KAMU MENANG Hebat! Semua soal berhasil diselesaikan.";
            yield return new WaitForSecondsRealtime(1.5f);
            TutorialText.text = "Setelah ini, kamu akan memulai perjalanan sungguhan. Terapkan semua yang sudah kamu pelajari dan terus melaju hingga sampai tujuanmu!";
            yield return new WaitForSecondsRealtime(3.5f);
            TutorialText.text = "";
            StartCoroutine(MunculkanTombol());
        }
        
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

    public void BatuTutorial()
    {
        sedangTutorialBatu = true;
        TruckScript2 ts2 = FindObjectOfType<TruckScript2>();
        TutorialBox.SetActive(true);
        TutorialText.text = "Awas! Batu di jalan harus dihindari. Menabrak batu akan mengurangi nyawa. Jika nyawa habis, perjalanan harus diulang. Tekan tombol A / D untuk melanjutkan perjalanan";

        if (Input.GetKeyDown(KeyCode.A))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            ts2.sedangTutorial = false;
            sedangTutorialBatu = false;
            TutorialText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            ts2.sedangTutorial = false;
            sedangTutorialBatu = false;
            TutorialText.text = "";
        }
    }

    public void BolaTutorial()
    {
        sedangTutorialBola = true;
        TruckScript2 ts2 = FindObjectOfType<TruckScript2>();
        TutorialBox.SetActive(true);
        TutorialText.text = "Itu adalah bola simbol operasi. Ambil bola-bola tersebut untuk menambah skor. Semakin banyak simbol yang dikumpulkan, semakin tinggi skor yang kamu dapatkan. Tekan tombol 'Enter' untuk lanjut";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            ts2.sedangTutorial = false;
            sedangTutorialBola = false;
            TutorialText.text = "";
        }
    }

    public void PoinTutorial()
    {
        sedangTutorialPoin = true;
        TutorialBox.SetActive(true);
        TutorialText.text = "Selamat! kamu mendapatkan 100 poin! setiap 1 bola Operator bernilai 100 poin. Mari kita coba kumpulkan bola operator tersebut hingga mencapai 1000 poin!";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            sedangTutorialPoin = false;
            TutorialText.text = "";
        }
    }

    public void PersimpanganTutorial()
    {
        sedangTutorialPersimpangan = true;
        TutorialBox.SetActive(true);
        TutorialText.text = "Di persimpangan, perhatikan soal dan pilihan jawaban dengan cermat. Sederhanakan soal di atas dengan mengerjakan dahulu operasi yang benar menurut hierarki matematika. Tekan tombol 1, 2, atau 3 untuk memilih jawabanmu agar perjalanan dapat berlanjut. Tekan tombol 'Enter' untuk lanjut.";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            sedangTutorialPersimpangan = false;
            TutorialText.text = "";
        }
    }

    public void SoalTutorial()
    {
        sedangTutorialSoal = true;
        TutorialBox.SetActive(true);
        TutorialText.text = "Keren, tepat sekali! Hati-hati di perjalanan selanjutnya, ya!";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TutorialBox.SetActive(false);
            Time.timeScale = 1f;
            sedangTutorialSoal = false;
            TutorialText.text = "";
        }
    }

    public void WelcomeText()
    {
        StartCoroutine(SequenceWelcomeText());
    }

    IEnumerator SequenceWelcomeText()
    {
        yield return new WaitForSecondsRealtime(0f);
        TutorialBox.SetActive(true);
        TutorialText.fontSize = 45;
        TutorialText.text = "Halo, aku Budi! Aku sedang dalam perjalanan menuju lomba matematika. Sepanjang perjalanan, aku harus memilih rute yang tepat. Selain itu, aku harus berlatih menjawab soal matematika terkait hierarki urutan operasi. Ayo bantu aku menentukan jalan yang benar dan belajar bersama untuk meraih kemenangan! Tekan 'Enter' untuk lanjut!";
    }

    public void TriggerSmoothTimeScale()
    {
        StartCoroutine(SmoothTimeScale(1f, 1f));
    }

    IEnumerator SmoothTimeScale(float targetScale, float duration)
    {
        float startScale = Time.timeScale;
        float time = 0f;

        while (time < duration)
        {
            Time.timeScale = Mathf.Lerp(startScale, targetScale, time / duration);

            time += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = targetScale;
    }
}
