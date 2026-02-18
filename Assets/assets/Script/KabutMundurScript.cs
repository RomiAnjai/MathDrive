using UnityEngine;

public class KabutMundurScript : MonoBehaviour
{
    public bool isDecision = false;

    private void Awake()
    {
        Debug.Log("Posisi awal kabut: " + transform.position.z);

        if(transform.position.z <= 14.88f)
        {
            Vector3 pos = transform.position;
            pos.z = 14.88f;
            transform.position = pos;
        }

        Debug.Log("Posisi setelah koreksi: " + transform.position.z);
    }

    void Update()
    {
        SoalScript ss = FindObjectOfType<SoalScript>();
        if(isDecision == true && transform.position.z <= 110 && ss.soalAktif == true)
        {
            transform.Translate(Vector3.forward * 30 * Time.deltaTime);
        }
        if(isDecision == false && transform.position.z >= 14.88)
        {
            transform.Translate(Vector3.forward * -300 * Time.deltaTime);

            if(transform.position.z < 14.88f)
            {
                Vector3 pos = transform.position;
                pos.z = 14.88f;
                transform.position = pos;
            }
        }
    }
}
