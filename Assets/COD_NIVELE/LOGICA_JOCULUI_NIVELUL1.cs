using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGICA_JOCULUI_NIVELUL1 : MonoBehaviour
{
    public Button doza1;
    public Button doza2;
    public Button doza3;

    public TextMeshProUGUI textIndiciu;
    public GameObject panouVictorie;

    private int pozitie_corecta1;
    private int pozitie_corecta2;
    private int pozitie_corecta3;

    private Button primaDozaSelectata = null;
    private float xStanga, xMijloc, xDreapta;

    void Start()
    {
        SeteazaPozitiiRandom();

        xStanga = doza1.GetComponent<RectTransform>().anchoredPosition.x;
        xMijloc = doza2.GetComponent<RectTransform>().anchoredPosition.x;
        xDreapta = doza3.GetComponent<RectTransform>().anchoredPosition.x;

        doza1.onClick.AddListener(() => DozaClick(doza1));
        doza2.onClick.AddListener(() => DozaClick(doza2));
        doza3.onClick.AddListener(() => DozaClick(doza3));

        panouVictorie.SetActive(false);

        ActualizeazaIndiciu();
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
                textIndiciu.text = ManagerLocalizare.instanta.ObtineTextVictorie();
            }
        }
    }

    public void ActualizeazaIndiciu()
    {
        int numarCorecte = 0;
        int numarTotal = 3;

        if (doza1.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta1)) numarCorecte++;
        if (doza2.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta2)) numarCorecte++;
        if (doza3.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta3)) numarCorecte++;

        textIndiciu.text = ManagerLocalizare.instanta.ObtineTextIndiciuTradus(numarCorecte, numarTotal);
    }

    bool VerificaVictorie()
    {
        if (doza1.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta1)) return false;
        if (doza2.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta2)) return false;
        if (doza3.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta3)) return false;
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