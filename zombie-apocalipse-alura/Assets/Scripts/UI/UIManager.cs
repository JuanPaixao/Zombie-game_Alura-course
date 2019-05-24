using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    private Player _playerScript;
    public Slider sliderHP;
    public GameObject gameOverPanel;
    public Text gameOverText, maxGameOverText, zombieScore, bossSpawnText;
    private int _minutes, _seconds, _zombiesKilled;
    private float _maxScore;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        if (sceneName != "menu")
        {
            _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (sliderHP != null)
        {
            sliderHP.maxValue = _playerScript.hp;
        }
        _maxScore = PlayerPrefs.GetFloat("MaxTime", _maxScore);
        Debug.Log(_maxScore);
    }
    public void GameOver()
    {
        _minutes = (int)(Time.timeSinceLevelLoad / 60);
        _seconds = (int)Time.timeSinceLevelLoad % 60;
        gameOverPanel.SetActive(true);
        gameOverText.text = " You survived for: \n" + _minutes + " min and " + _seconds + " seconds.";
        MaxScore(_minutes, _seconds);
    }
    public void SetHP()
    {
        sliderHP.value = _playerScript.hp;
    }
    public void MaxScore(int min, int sec)
    {
        if (Time.timeSinceLevelLoad > _maxScore)
        {
            _maxScore = Time.timeSinceLevelLoad;
            maxGameOverText.text = string.Format("Your best time is: {0} min and {1} seconds", min, sec);
            PlayerPrefs.SetFloat("MaxTime", _maxScore);
        }
        if (maxGameOverText.text == "")
        {
            min = (int)_maxScore / 60;
            sec = (int)_maxScore % 60;
            maxGameOverText.text = string.Format("Your best time is:\n{0} min and {1} seconds", min, sec);
        }
    }
    public void UpdateZombieCount()
    {
        _zombiesKilled++;
        zombieScore.text = string.Format("x {0}", _zombiesKilled);
    }
    public void BossSpawnText()
    {
        bossSpawnText.gameObject.SetActive(true);
        StartCoroutine(BossSpawnTextOff());
    }
    private IEnumerator BossSpawnTextOff()
    {
        Color fadeText = bossSpawnText.color;
        fadeText.a = 1;
        bossSpawnText.color = fadeText;
        float count = 0f;
        while (bossSpawnText.color.a > 0)
        {
            count += Time.deltaTime / 3; //time to fade);
            fadeText.a = Mathf.Lerp(1, 0, count);
            yield return null;
            bossSpawnText.color = fadeText;
            if (fadeText.a <= 0)
            {
                bossSpawnText.gameObject.SetActive(false);
            }
        }
    }
}
