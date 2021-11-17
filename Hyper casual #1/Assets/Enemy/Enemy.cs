using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _speed;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(Random.Range(0, 180), Random.Range(0, 180) / 100, Random.Range(0, 180)));
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -10), _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
            _player.UpdateScore();
        }
        else if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().Die();
        }
    }
}