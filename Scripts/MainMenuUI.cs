using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

    public void PlayGame()
    {
        Debug.Log("кнопка не нажата");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Выход из игры");
    }
}