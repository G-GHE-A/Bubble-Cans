using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerNivele : MonoBehaviour
{
    public void IncarcaNivel(int numarNivel)
    {
        SceneManager.LoadScene("Nivelul_" + numarNivel);
    }
}