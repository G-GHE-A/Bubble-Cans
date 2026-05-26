using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGICA_JOCULUI_NIVELUL2 : MonoBehaviour
{
    public Button doza1;
    public Button doza2;
    public Button doza3;
    public Button doza4;

    public TextMeshProUGUI textIndiciu;
    public GameObject panouVictorie;

    private int pozitie_corecta1;
    private int pozitie_corecta2;
    private int pozitie_corecta3;
    private int pozitie_corecta4;

    private Button primaDozaSelectata = null;
    private float xSlot1, xSlot2, xSlot3, xSlot4;

    void Start()
    {
        SeteazaPozitiiRandom();

        xSlot1 = doza1.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot2 = doza2.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot3 = doza3.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot4 = doza4.GetComponent<RectTransform>().anchoredPosition.x;

        doza1.onClick.AddListener(() => DozaClick(doza1));
        doza2.onClick.AddListener(() => DozaClick(doza2));
        doza3.onClick.AddListener(() => DozaClick(doza3));
        doza4.onClick.AddListener(() => DozaClick(doza4));

        panouVictorie.SetActive(false);
        ActualizeazaIndiciu();
    }

    void SeteazaPozitiiRandom()
    {
        do
        {
            List<int> numereDisponibile = new List<int> { 1, 2, 3, 4 };

            int index = Random.Range(0, numereDisponibile.Count);
            pozitie_corecta1 = numereDisponibile[index];
            numereDisponibile.RemoveAt(index);

            index = Random.Range(0, numereDisponibile.Count);
            pozitie_corecta2 = numereDisponibile[index];
            numereDisponibile.RemoveAt(index);

            index = Random.Range(0, numereDisponibile.Count);
            pozitie_corecta3 = numereDisponibile[index];
            numereDisponibile.RemoveAt(index);

            pozitie_corecta4 = numereDisponibile[0];
        } while (pozitie_corecta1 == 1 || pozitie_corecta2 == 2 || pozitie_corecta3 == 3 || pozitie_corecta4 == 4);
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
                textIndiciu.text = "PERFECT!";
            }
        }
    }

    void ActualizeazaIndiciu()
    {
        int numarCorecte = 0;

        if (doza1.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta1)) numarCorecte++;
        if (doza2.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta2)) numarCorecte++;
        if (doza3.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta3)) numarCorecte++;
        if (doza4.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta4)) numarCorecte++;

        textIndiciu.text = "Doze corecte: " + numarCorecte + " / 4";
    }

    bool VerificaVictorie()
    {
        if (doza1.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta1)) return false;
        if (doza2.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta2)) return false;
        if (doza3.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta3)) return false;
        if (doza4.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta4)) return false;

        return true;
    }

    float ObtineCoordonataXSlot(int pozitieCeruta)
    {
        if (pozitieCeruta == 1) return xSlot1;
        if (pozitieCeruta == 2) return xSlot2;
        if (pozitieCeruta == 3) return xSlot3;
        if (pozitieCeruta == 4) return xSlot4;

        return 0f;
    }
}