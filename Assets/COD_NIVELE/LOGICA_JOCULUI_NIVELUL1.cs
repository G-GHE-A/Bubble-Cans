using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGICA_JOCULUI_NIVELUL1 : MonoBehaviour
{
    [Header("Generare Dinamică")]
    [SerializeField] private GameObject[] dozePrefabs;
    [SerializeField] private Transform containerDoze;

    [Header("Coordonate UI (Ajustează din Inspector)")]
    [SerializeField] private float xStanga = -200f;
    [SerializeField] private float xMijloc = 0f;
    [SerializeField] private float xDreapta = 200f;
    [SerializeField] private float pozitieY = -150f;

    [Header("Elemente UI Externe")]
    public TextMeshProUGUI textIndiciu;
    public TextMeshProUGUI textCastig;
    public TextMeshProUGUI textUrmatorulNivel;
    public GameObject panouVictorie;

    private int pozitie_corecta1;
    private int pozitie_corecta2;
    private int pozitie_corecta3;

    private Button primaDozaSelectata = null;
    private List<Button> dozeInstantiate = new List<Button>();

    void Start()
    {
        SeteazaPozitiiRandom();
        GenereazaDoze();

        panouVictorie.SetActive(false);
        ActualizeazaIndiciu();
    }

    void GenereazaDoze()
    {
        float[] pozitiiX = { xStanga, xMijloc, xDreapta };

        for (int i = 0; i < dozePrefabs.Length; i++)
        {
            GameObject dozaNoua = Instantiate(dozePrefabs[i], containerDoze);

            // Forțează scara la 1 pentru a evita ca elementele UI să devină invizibile
            dozaNoua.transform.localScale = Vector3.one;

            RectTransform rt = dozaNoua.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(pozitiiX[i], pozitieY);

            Button butonDoza = dozaNoua.GetComponent<Button>();
            butonDoza.onClick.AddListener(() => DozaClick(butonDoza));

            dozeInstantiate.Add(butonDoza);
        }
    }

    void SeteazaPozitiiRandom()
    {
        do
        {
            List<int> numereDisponibile = new List<int> { 1, 2, 3 };

            int index = Random.Range(0, numereDisponibile.Count);
            pozitie_corecta1 = numereDisponibile[index];
            numereDisponibile.RemoveAt(index);

            index = Random.Range(0, numereDisponibile.Count);
            pozitie_corecta2 = numereDisponibile[index];
            numereDisponibile.RemoveAt(index);

            pozitie_corecta3 = numereDisponibile[0];
        }
        while (pozitie_corecta1 == 1 || pozitie_corecta2 == 2 || pozitie_corecta3 == 3);
    }

    void DozaClick(Button dozaApasata)
    {
        RectTransform rtApasat = dozaApasata.GetComponent<RectTransform>();

        if (primaDozaSelectata == null)
        {
            primaDozaSelectata = dozaApasata;
            rtApasat.anchoredPosition = new Vector2(rtApasat.anchoredPosition.x, rtApasat.anchoredPosition.y + 30);
        }
        else if (primaDozaSelectata == dozaApasata)
        {
            rtApasat.anchoredPosition = new Vector2(rtApasat.anchoredPosition.x, rtApasat.anchoredPosition.y - 30);
            primaDozaSelectata = null;
        }
        else
        {
            RectTransform rtPrimaDoza = primaDozaSelectata.GetComponent<RectTransform>();
            rtPrimaDoza.anchoredPosition = new Vector2(rtPrimaDoza.anchoredPosition.x, rtPrimaDoza.anchoredPosition.y - 30);

            Vector2 pozitieTemporara = rtPrimaDoza.anchoredPosition;
            rtPrimaDoza.anchoredPosition = rtApasat.anchoredPosition;
            rtApasat.anchoredPosition = pozitieTemporara;

            primaDozaSelectata = null;

            ActualizeazaIndiciu();

            if (VerificaVictorie() == true)
            {
                panouVictorie.SetActive(true);
                textCastig.text = ManagerLocalizare.instanta.ObtineTextVictorie();
                textUrmatorulNivel.text = ManagerLocalizare.instanta.ObtineTextUrmatorulNivel();

                // Ascunde dozele după ce nivelul este completat
                foreach (Button doza in dozeInstantiate)
                {
                    doza.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ActualizeazaIndiciu()
    {
        if (dozeInstantiate.Count < 3) return;

        int numarCorecte = 0;
        int numarTotal = 3;

        if (dozeInstantiate[0].GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta1)) numarCorecte++;
        if (dozeInstantiate[1].GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta2)) numarCorecte++;
        if (dozeInstantiate[2].GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta3)) numarCorecte++;

        textIndiciu.text = ManagerLocalizare.instanta.ObtineTextIndiciuTradus(numarCorecte, numarTotal);
    }

    bool VerificaVictorie()
    {
        if (dozeInstantiate.Count < 3) return false;

        if (dozeInstantiate[0].GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta1)) return false;
        if (dozeInstantiate[1].GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta2)) return false;
        if (dozeInstantiate[2].GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta3)) return false;

        return true;
    }

    float ObtineCoordonataXSlot(int pozitieCeruta)
    {
        if (pozitieCeruta == 1) return xStanga;
        if (pozitieCeruta == 2) return xMijloc;
        if (pozitieCeruta == 3) return xDreapta;
        return 0f;
    }
}