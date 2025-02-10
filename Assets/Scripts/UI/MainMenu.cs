using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Scene]
    [SerializeField] int sceneToLoad;
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
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
}
