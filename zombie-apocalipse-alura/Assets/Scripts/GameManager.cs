using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameOver, exitButton;
    private Animator _cameraAnimator;

    private void Start()
    {
        _cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        Time.timeScale = 1;
#if UNITY_STANDALONE || UNITY_EDITOR
        if (exitButton != null)
        {
            exitButton.SetActive(true);
        }
#endif
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("fase_01");
    }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    public void ShakeCamera()
    {
        _cameraAnimator.SetBool("Shake", true);
        StartCoroutine(StopShake());
    }
    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.1f);
        _cameraAnimator.SetBool("Shake", false);
    }
    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(0.1f); //if my game is paused, i can use WaitForSecondsRealTime so the sound will play the sound just fine.
        SceneManager.LoadSceneAsync("fase_01");
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
