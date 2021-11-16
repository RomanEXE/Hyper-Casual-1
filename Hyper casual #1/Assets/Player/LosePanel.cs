using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyUI;
    //[SerializeField] private TextMeshProUGUI _scoreUI;

    public void OpenPanel(int score, int money)
    {
        _moneyUI.text = ($"Money: {money}");
        //_scoreUI.text = ($"Score: {score}");

        gameObject.SetActive(true);
    }
}