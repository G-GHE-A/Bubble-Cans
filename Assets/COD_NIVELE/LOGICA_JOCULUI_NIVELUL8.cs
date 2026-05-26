using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGICA_JOCULUI_NIVELUL8 : MonoBehaviour
{
    public Button doza1;
    public Button doza2;
    public Button doza3;
    public Button doza4;
    public Button doza5;
    public Button doza6;
    public Button doza7;
    public Button doza8;
    public Button doza9;
    public Button doza10;

    public TextMeshProUGUI textIndiciu;
    public GameObject panouVictorie;

    private int pozitie_corecta1;
    private int pozitie_corecta2;
    private int pozitie_corecta3;
    private int pozitie_corecta4;
    private int pozitie_corecta5;
    private int pozitie_corecta6;
    private int pozitie_corecta7;
    private int pozitie_corecta8;
    private int pozitie_corecta9;
    private int pozitie_corecta10;

    private Button primaDozaSelectata = null;
    private float xSlot1, xSlot2, xSlot3, xSlot4, xSlot5, xSlot6, xSlot7, xSlot8, xSlot9, xSlot10;

    void Start()
    {
        SeteazaPozitiiRandom();

        xSlot1 = doza1.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot2 = doza2.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot3 = doza3.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot4 = doza4.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot5 = doza5.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot6 = doza6.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot7 = doza7.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot8 = doza8.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot9 = doza9.GetComponent<RectTransform>().anchoredPosition.x;
        xSlot10 = doza10.GetComponent<RectTransform>().anchoredPosition.x;

        doza1.onClick.AddListener(() => DozaClick(doza1));
        doza2.onClick.AddListener(() => DozaClick(doza2));
        doza3.onClick.AddListener(() => DozaClick(doza3));
        doza4.onClick.AddListener(() => DozaClick(doza4));
        doza5.onClick.AddListener(() => DozaClick(doza5));
        doza6.onClick.AddListener(() => DozaClick(doza6));
        doza7.onClick.AddListener(() => DozaClick(doza7));
        doza8.onClick.AddListener(() => DozaClick(doza8));
        doza9.onClick.AddListener(() => DozaClick(doza9));
        doza10.onClick.AddListener(() => DozaClick(doza10));

        panouVictorie.SetActive(false);
        ActualizeazaIndiciu();
    }

    void SeteazaPozitiiRandom()
    {
        do { 
        List<int> numereDisponibile = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta1 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta2 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta3 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta4 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta5 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta6 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta7 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta8 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        index = Random.Range(0, numereDisponibile.Count);
        pozitie_corecta9 = numereDisponibile[index];
        numereDisponibile.RemoveAt(index);

        pozitie_corecta10 = numereDisponibile[0];
        } while (pozitie_corecta1 == 1 || pozitie_corecta2 == 2 || pozitie_corecta3 == 3 || pozitie_corecta4 == 4 || pozitie_corecta5 == 5 || pozitie_corecta6 == 6 || pozitie_corecta7 == 7 || pozitie_corecta8 == 8 || pozitie_corecta9 == 9 || pozitie_corecta10 == 10);
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
        if (doza5.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta5)) numarCorecte++;
        if (doza6.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta6)) numarCorecte++;
        if (doza7.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta7)) numarCorecte++;
        if (doza8.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta8)) numarCorecte++;
        if (doza9.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta9)) numarCorecte++;
        if (doza10.GetComponent<RectTransform>().anchoredPosition.x == ObtineCoordonataXSlot(pozitie_corecta10)) numarCorecte++;

        textIndiciu.text = "Doze corecte: " + numarCorecte + " / 10";
    }

    bool VerificaVictorie()
    {
        if (doza1.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta1)) return false;
        if (doza2.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta2)) return false;
        if (doza3.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta3)) return false;
        if (doza4.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta4)) return false;
        if (doza5.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta5)) return false;
        if (doza6.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta6)) return false;
        if (doza7.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta7)) return false;
        if (doza8.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta8)) return false;
        if (doza9.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta9)) return false;
        if (doza10.GetComponent<RectTransform>().anchoredPosition.x != ObtineCoordonataXSlot(pozitie_corecta10)) return false;

        return true;
    }

    float ObtineCoordonataXSlot(int pozitieCeruta)
    {
        if (pozitieCeruta == 1) return xSlot1;
        if (pozitieCeruta == 2) return xSlot2;
        if (pozitieCeruta == 3) return xSlot3;
        if (pozitieCeruta == 4) return xSlot4;
        if (pozitieCeruta == 5) return xSlot5;
        if (pozitieCeruta == 6) return xSlot6;
        if (pozitieCeruta == 7) return xSlot7;
        if (pozitieCeruta == 8) return xSlot8;
        if (pozitieCeruta == 9) return xSlot9;
        if (pozitieCeruta == 10) return xSlot10;

        return 0f;
    }
}