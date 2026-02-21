using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class TahapanSoal
{
    public string pertanyaan;
    public string jawabanBenar;
    public string[] jawabanSalah;
}

[System.Serializable]
public class SoalBesar
{
    public string namaSoal;
    public TahapanSoal[] tahapan;
}

public class SoalScript : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelSoal;
    public GameObject jawaban1;
    public GameObject jawaban2;
    public GameObject jawaban3;

    public TMP_Text soalPertanyaan;
    public TMP_Text jawabanKe1;
    public TMP_Text jawabanKe2;
    public TMP_Text jawabanKe3;

    public TMP_Text displayPoin;

    [Header("DATA SOAL")]
    public SoalBesar[] semuaSoal;

    [Header("State")]
    public bool soalAktif = false;
    
    [Header("Reveal Jawaban Soal")]
    public GameObject Green1;
    public GameObject Red1;
    public GameObject Green2;
    public GameObject Red2;
    public GameObject Green3;
    public GameObject Red3;

    private int indexSoalBesar = 0;
    private int indexTahapan = 0;

    [HideInInspector] public int jawabanBenar;
    [HideInInspector] public int hehe;
    [HideInInspector] public int whatIsThisVariableForQuestionMark = 0;
    private int randomPos;

    private AudioSource audioSource;
    public AudioClip sFXSalah;
    public AudioClip sFXBenar;
    public bool yesTutorial;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panelSoal.SetActive(false);
    }

    void Update()
    {
        PlayerPoint ppoin = FindObjectOfType<PlayerPoint>();
        PersimpanganScript pss = FindObjectOfType<PersimpanganScript>();
        PlatformScript[] platforms = FindObjectsOfType<PlatformScript>();

        if (ppoin != null)
            displayPoin.text = "" + ppoin.totalPoint;

        if (!soalAktif && platforms.Length == 0 && pss.currentSpeed == 0)
        {
            MulaiSoal();
        }

        if (soalAktif)
        {
            CekInputJawaban();
        }
    }

    void MulaiSoal()
    {
        if (indexSoalBesar >= semuaSoal.Length)
            return;

        soalAktif = true;
        panelSoal.SetActive(true);
        jawaban1.SetActive(true);
        jawaban2.SetActive(true);
        jawaban3.SetActive(true);
        Green1.SetActive(false);
        Green2.SetActive(false);
        Green3.SetActive(false);
        Red1.SetActive(false);
        Red2.SetActive(false);
        Red3.SetActive(false);

        TahapanSoal tahap = semuaSoal[indexSoalBesar].tahapan[indexTahapan];

        soalPertanyaan.text = FormatHighlight(tahap.pertanyaan);

        randomPos = Random.Range(1, 4);
        jawabanBenar = randomPos;

        string benar = FormatHighlight(tahap.jawabanBenar);
        string salah1 = FormatHighlight(tahap.jawabanSalah[0]);
        string salah2 = FormatHighlight(tahap.jawabanSalah[1]);

        if (randomPos == 1)
        {
            jawabanKe1.text = benar;
            jawabanKe2.text = salah1;
            jawabanKe3.text = salah2;
        }
        else if (randomPos == 2)
        {
            jawabanKe1.text = salah1;
            jawabanKe2.text = benar;
            jawabanKe3.text = salah2;
        }
        else
        {
            jawabanKe1.text = salah1;
            jawabanKe2.text = salah2;
            jawabanKe3.text = benar;
        }
    }

    void CekInputJawaban()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ProsesJawaban(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ProsesJawaban(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ProsesJawaban(3);
    }

    void ProsesJawaban(int inputPlayer)
    {
        soalAktif = false;

        hehe = inputPlayer;

        if (inputPlayer == jawabanBenar)
        {
            audioSource.PlayOneShot(sFXBenar);

            // RESET variable aneh
            whatIsThisVariableForQuestionMark = 0;

            GameManager gm = FindObjectOfType<GameManager>();
            if(yesTutorial == true)
            {
                yesTutorial = false;
                Time.timeScale = 0f;
                gm.SoalTutorial();
            }

            ShowJawab();
            LanjutTahapan();
        }
        else
        {
            audioSource.PlayOneShot(sFXSalah);
            panelSoal.SetActive(false);
            jawaban1.SetActive(false);
            jawaban2.SetActive(false);
            jawaban3.SetActive(false);

            PlayerPoint ppoint = FindObjectOfType<PlayerPoint>();
            if (ppoint != null)
                ppoint.totalPoint -= ppoint.rekamanPoin;

            whatIsThisVariableForQuestionMark++;

            FindObjectOfType<GameManager>().KembaliKeRunning();
        }
    }

    void LanjutTahapan()
    {
        indexTahapan++;

        if (indexTahapan >= semuaSoal[indexSoalBesar].tahapan.Length)
        {
            // Soal besar selesai
            indexSoalBesar++;
            indexTahapan = 0;

            if (indexSoalBesar >= semuaSoal.Length)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                gm.Kemenangan();
                return;
            }
        }

        FindObjectOfType<GameManager>().KembaliKeRunning();
    }

    void ShowJawab()
    {
        StartCoroutine(SequenceShowJawab());
    }
    IEnumerator SequenceShowJawab()
    {
        yield return new WaitForSecondsRealtime(0f);
        
        if(randomPos == 1)
            {
                Green1.SetActive(true);
                Red2.SetActive(true);
                Red3.SetActive(true);
            } else if (randomPos == 2)
            {
                Red1.SetActive(true);
                Green2.SetActive(true);
                Red3.SetActive(true);
            } else if (randomPos == 3)
            {
                Red1.SetActive(true);
                Red2.SetActive(true);
                Green3.SetActive(true);
            }
        
        yield return new WaitForSecondsRealtime(1f);
        panelSoal.SetActive(false);
        jawaban1.SetActive(false);
        jawaban2.SetActive(false);
        jawaban3.SetActive(false);
    }

    string FormatHighlight(string input)
    {
        while (input.Contains("*"))
        {
            int start = input.IndexOf("*");
            int end = input.IndexOf("*", start + 1);

            if (end > start)
            {
                string target = input.Substring(start + 1, end - start - 1);
                string highlighted = "<mark=#FF4D4D>" + target + "</mark>";
                input = input.Remove(start, end - start + 1);
                input = input.Insert(start, highlighted);
            }
            else
            {
                break;
            }
        }

        return input;
    }

}
