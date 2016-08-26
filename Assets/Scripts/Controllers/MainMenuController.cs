using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MainMenuController : MonoBehaviour
{
    public GameObject _newGameDialog;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowNewGameDialog()
    {
        _newGameDialog.SetActive(true);
    }

    public void ConfirmNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Game");
    }

    public void DenyNewGame()
    {
        _newGameDialog.SetActive(false);
    }
}
