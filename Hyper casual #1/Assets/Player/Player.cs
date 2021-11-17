using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Transform leftPoint, rightPoint;
    [SerializeField] private Player _playerPrefab;

    [Header("Stats")]
    [SerializeField] private int _speed;
    [SerializeField] int _money;
    [SerializeField] int _score;

    [Header("UI")]
    [SerializeField] private Text _scoreUI;
    [SerializeField] private LosePanel _losePanel;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _deathParticle;

    private void Start()
    {
        _money = PlayerPrefs.GetInt("Money");

        GetComponent<SpriteRenderer>().sprite = _playerPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touchPosition.x > 0)
            {
                MovePlayer(rightPoint.position);
            }
            else
            {
                MovePlayer(leftPoint.position);
            }
        }
    }

    private void MovePlayer(Vector2 point)
    {
        transform.position = Vector2.MoveTowards(transform.position, point, _speed * Time.deltaTime);
    }

    public void UpdateScore()
    {
        _score++;
        _scoreUI.text = $"Score: {_score}";
    }

    public void Die()
    {
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            spawner.StopAllCoroutines();
        }

        _speed = 0;
        var particle = Instantiate(_deathParticle);
        particle.transform.position = transform.position;

        PlayerPrefs.SetInt("Money", _money += _score);
        PlayerPrefs.Save();

        FindObjectOfType<AudioManager>().Play("Sound of the death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _losePanel.OpenPanel(_score, PlayerPrefs.GetInt("Money", _money));
    }
}