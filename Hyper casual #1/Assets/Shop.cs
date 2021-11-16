using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _playerPrefab;

    [Header("Skins")]
    [SerializeField] private Skin[] _skins;
    [SerializeField] private int _currentSkinIndex;
    [SerializeField] private Image _skinImage;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("UI")]
    [SerializeField] private Button _buyBtn;
    [SerializeField] private Button _setSkinBtn;
    [SerializeField] private TextMeshProUGUI _priceUI;

    private void Start()
    {
        _priceUI.text = _skins[_currentSkinIndex].price.ToString();
        _skinImage.sprite = _skins[_currentSkinIndex].sprite;
    }

    public void SelectSkin(int nextIndex)
    {
        if (_currentSkinIndex + nextIndex < _skins.Length && _currentSkinIndex + nextIndex >= 0)
        {
            if (_skins[_currentSkinIndex].bougth)
            {
                _priceUI.text = "Bought";
            }
            else
            {
                _priceUI.text = _skins[_currentSkinIndex].price.ToString();
            }

            _currentSkinIndex += nextIndex;
            ChangeSkinImage();
        }
    }

    public void ChangeSkinImage()
    {
        _animator.Play("SelectSkin");
        _skinImage.sprite = _skins[_currentSkinIndex].sprite;
    }

    public void SetSkin()
    {
        if (_skins[_currentSkinIndex].bougth)
        {
            _playerPrefab.GetComponent<SpriteRenderer>().sprite = _skinImage.sprite;
        }
        else
        {
            int money = PlayerPrefs.GetInt("Money");

            if (money > _skins[_currentSkinIndex].price)
            {
                _playerPrefab.GetComponent<SpriteRenderer>().sprite = _skinImage.sprite;
                PlayerPrefs.SetInt("Money", money -= _skins[_currentSkinIndex].price);
                _skins[_currentSkinIndex].bougth = true;
                _buyBtn = _setSkinBtn;
            }
        }

    }
}