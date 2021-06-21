using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 8.0f;

    private playerScript _scoreUp;
    //private asteroidScript _explosionOnAsteroid;

    void Start()
    {
        //.Log(_scoreUp);
        _scoreUp = GameObject.Find("Player").GetComponent<playerScript>();
        //Debug.Log(_explosionOnAsteroid);
    }

    void Update()
    {
            //Why the laser change direction with the following code?
            //transform.Translate(new Vector3(0, Mathf.Abs(transform.position.x) * _laserSpeed * Time.deltaTime, 0));
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
            if (_scoreUp != null)
            {
                _scoreUp.PlayerScore(Random.Range (5, 11));
            }
            other.GetComponent<enemyScript>().ExplosionEnemy();
            Destroy(this.gameObject);
        }

        if (other.tag == "Asteroid")
        {
            other.GetComponent<asteroidScript>().ExplodeAsteroid();
            Destroy(this.gameObject);
            //Debug.Log("hit asteroid");
            //Debug.Log(_explosionOnAsteroid);
            //_explosionOnAsteroid = GameObject.Find("Asteroid").GetComponent<asteroidScript>();
            //_explosionOnAsteroid.ExplodeAsteroid();
        }
    }
}
