using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    private Player _playerScript;
    public Slider sliderHP;
    public GameObject gameOverPanel;
    public Text gameOverText, maxGameOverText, zombieScore;
    private int _minutes, _seconds, _zombiesKilled;
    private float _maxScore;
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sliderHP.maxValue = _playerScript.hp;
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
}
