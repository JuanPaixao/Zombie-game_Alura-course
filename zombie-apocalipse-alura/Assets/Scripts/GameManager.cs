using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameOver;
    private Animator _cameraAnimator;

    private void Start()
    {
        _cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("fase_01");
    }
    public void ShakeCamera()
    {
        _cameraAnimator.SetBool("Shake", true);
		StartCoroutine(StopShake());
    }
    private IEnumerator StopShake()
    {
		yield return new WaitForSeconds(0.2f);
		 _cameraAnimator.SetBool("Shake", false);
    }
}
