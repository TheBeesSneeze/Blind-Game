using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] RectTransform pauseGroup;
    [SerializeField] Button continueButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    
    [ReadOnly][SerializeField] private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;

        InputEvents.PauseCanceled.AddListener(TogglePaused);
        InputEvents.PauseStarted.AddListener(OnEscPressed);

        continueButton.onClick.AddListener(OnContinueClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        Unpause();
    }

    void OnContinueClicked()
    {
        Unpause();
    }

    void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnQuitClicked()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    void OnEscPressed()
    {
        Time.timeScale = 0.5f;
    }

    void OnEscReleased()
    {
        TogglePaused();
    }

    public void TogglePaused()
    {
        paused = !paused;
        if (paused)
            Pause();
        else
            Unpause();
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        paused = true;
        pauseGroup.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        pauseGroup.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
