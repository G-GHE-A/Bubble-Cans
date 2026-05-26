using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerSetari : MonoBehaviour
{
    [Header("Butoane UI")]
    public Button butonSunet;
    public Button butonLimba;
    public Button butonDescriereAlb;

    [Header("Texte UI (TextMeshPro)")]
    public TextMeshProUGUI textButonSunet;
    public TextMeshProUGUI textButonLimba;
    public TextMeshProUGUI textExplicatieJoc;

    private bool sunetPornit = true;
    private bool arataExplicatia = false;

    void Start()
    {
        sunetPornit = AudioListener.volume > 0;
        if (textExplicatieJoc != null) textExplicatieJoc.gameObject.SetActive(false);

        if (butonSunet != null) butonSunet.onClick.AddListener(SchimbaSunet);
        if (butonLimba != null) butonLimba.onClick.AddListener(SchimbaLimba);
        if (butonDescriereAlb != null) butonDescriereAlb.onClick.AddListener(AfiseazaAscundeRegulile);

        ActualizeazaInterfata();
    }

    void SchimbaSunet()
    {
        sunetPornit = !sunetPornit;
        AudioListener.volume = sunetPornit ? 1f : 0f;
        ActualizeazaInterfata();
    }

    void SchimbaLimba()
    {
        ManagerLocalizare.instanta.SchimbaLimbaUrmatoare();
        ActualizeazaInterfata();
    }

    void AfiseazaAscundeRegulile()
    {
        arataExplicatia = !arataExplicatia;
        if (textExplicatieJoc != null)
        {
            textExplicatieJoc.gameObject.SetActive(arataExplicatia);
        }
        ActualizeazaInterfata();
    }

    void ActualizeazaInterfata()
    {
        if (ManagerLocalizare.instanta == null) return;

        if (textButonSunet != null) textButonSunet.text = ManagerLocalizare.instanta.ObtineTextSunet(sunetPornit);
        if (textButonLimba != null) textButonLimba.text = ManagerLocalizare.instanta.ObtineTextButonLimba();
        if (textExplicatieJoc != null) textExplicatieJoc.text = ManagerLocalizare.instanta.ObtineTextDescriereJoc();
    }
}