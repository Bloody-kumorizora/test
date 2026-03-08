using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float animationDuration = 1f;
    
    private bool isGameOver = false;

    private Vector2 startPos;
    private Vector2 targetPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        startPos = panelTransform.anchoredPosition + new Vector2(0, -800);
        targetPos = panelTransform.anchoredPosition;

        panelTransform.anchoredPosition = startPos;
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        StartCoroutine(AnimatePanel());
    }

    private IEnumerator AnimatePanel()
    {
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.unscaledDeltaTime;

            float t = time / animationDuration;

            // Плавность (ease out)
            t = 1f - Mathf.Pow(1f - t, 3f);

            panelTransform.anchoredPosition =
                Vector2.Lerp(startPos, targetPos, t);

            canvasGroup.alpha = t;

            yield return null;
        }

        panelTransform.anchoredPosition = targetPos;
        canvasGroup.alpha = 1f;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}