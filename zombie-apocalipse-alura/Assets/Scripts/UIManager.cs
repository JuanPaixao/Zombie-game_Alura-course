using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    private Player _playerScript;
    public Slider sliderHP;
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sliderHP.maxValue = _playerScript.hp;
    }
    void Update()
    {

    }
    public void SetHP()
    {
        sliderHP.value = _playerScript.hp;
    }
}
