using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditPopup;
    public void AbrirGame()
    {
        SceneManager.LoadScene("Abertura");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }


    public  void OpenCredits()
    {
        creditPopup.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPopup.SetActive(false);
    }
}
