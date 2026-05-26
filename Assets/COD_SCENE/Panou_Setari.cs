using UnityEngine;

public class Panou_Setari : MonoBehaviour
{
    public GameObject panouSetari;

    void Start()
    {
        if (panouSetari != null)
        {
            panouSetari.SetActive(false);
        }
    }
    public void DeschideInchideSetari()
    {
        if (panouSetari != null)
        {
            bool esteActiv = panouSetari.activeSelf;
            panouSetari.SetActive(!esteActiv);
        }
    }
}