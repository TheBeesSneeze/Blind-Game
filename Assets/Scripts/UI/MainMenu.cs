using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] MainMenuOpacity menuOpacity;
    [Scene]
    [SerializeField] int sceneToLoad;
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button howToPlayButton;

    [Header("Credits Menu")]
    [SerializeField] RectTransform creditsPanel;
    [SerializeField] Button credits_BackButton;

    [Header("How to play Menu")]
    [SerializeField] RectTransform howToPlayPanel;
    [SerializeField] Button howToPlay_BackButton;

    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);

        // main menu
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        creditsButton.onClick.AddListener(OpenCreditsMenu);
        howToPlayButton.onClick.AddListener(OpenHowToPlayMenu);

        // credits
        credits_BackButton.onClick.AddListener(OnAnyBackButtonClicked);

        // htp
        howToPlay_BackButton.onClick.AddListener(OnAnyBackButtonClicked);
    }

    void OnStartButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    void OnAnyBackButtonClicked()
    {
        menuOpacity.OnClick();
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }

    void OpenCreditsMenu()
    {
        menuOpacity.HideAll();
        creditsPanel.gameObject.SetActive(true);
        howToPlayPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(false);
    }

    void OpenHowToPlayMenu()
    {
        menuOpacity.HideAll();
        creditsPanel.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }
}
