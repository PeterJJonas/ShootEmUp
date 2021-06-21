using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4.0f;
    [SerializeField] private AudioClip _soundOnDeath;
    [SerializeField] [Range(0, 1)] private float _audioVolume = 1f;

    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -11f)
        {
            float randomX = Random.Range(-10.63f, 10.9f);
            transform.position = new Vector3(randomX, 15f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerScript player = other.transform.GetComponent<playerScript>();
            if (player != null)
            {
                player.PlayerDamage();
            }
            ExplosionEnemy();
        }
    }

    public void ExplosionEnemy()
    {
        _animator.SetTrigger("OnEnemyDeath");
        AudioSource.PlayClipAtPoint(_soundOnDeath, transform.position, _audioVolume);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(this.gameObject, 2.35f);
    }
}
