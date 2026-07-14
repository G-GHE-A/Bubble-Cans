using UnityEngine;

public class ManagerLocalizare : MonoBehaviour
{
    public static ManagerLocalizare instanta;

    private int indexLimba = 0; // 0 = Română, 1 = Engleză

    void Awake()
    {
        if (instanta == null) instanta = this;
    }

    public void SchimbaLimbaUrmatoare()
    {
        indexLimba++;
        if (indexLimba > 1) indexLimba = 0;
    }

    public string ObtineTextSunet(bool sunetPornit)
    {
        if (indexLimba == 0) return sunetPornit ? "Sunet: ON" : "Sunet: OFF";
        return sunetPornit ? "Sound: ON" : "Sound: OFF";
    }

    public string ObtineTextButonLimba()
    {
        if (indexLimba == 0) return "Limbă: Română";
        return "Language: English";
    }

    public string ObtineTextDescriereJoc()
    {
        if (indexLimba == 0)
        {
            return "CUM SE JOACĂ:\n\nMută dozele pe masă până când fiecare ajunge la locul ei corect.\nFii atent la indicii pentru a obține potrivirea PERFECTĂ!";
        }
        else
        {
            return "HOW TO PLAY:\n\nMove the cans on the desk until each one reaches its correct spot.\nPay attention to the clues to get the PERFECT match!";
        }
    }
    public string ObtineTextVictorie()
    {
        if (indexLimba == 0) return "PERFECT!";
        if (indexLimba == 1) return "WELL DONE!";
        return "PERFECT!";
    }
    public string ObtineTextUrmatorulNivel()
    {
        if (indexLimba == 0) return "URMĂTORUL NIVEL";
        if (indexLimba == 1) return "NEXT LEVEL";
        return "URMĂTORUL NIVEL";
    }
    public string ObtineTextIndiciuTradus(int numarCorecte, int numarTotal)
    {
        if (indexLimba == 0) return "Doze corecte: " + numarCorecte + " / " + numarTotal;
        if (indexLimba == 1) return "Correct cans: " + numarCorecte + " / " + numarTotal;
        return "";
    }
}