using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour
{
    [SerializeField] private float _randomRotate;
    [SerializeField] private GameObject _explodeAsteroid;
    [SerializeField] private float _asteroidSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 100f) > 50)
        {
            _randomRotate = Random.Range(-13f, -3f);
        }
        else
        {
            _randomRotate = Random.Range(3f, 13f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _asteroidSpeed * Time.deltaTime);
        transform.Rotate(0, 0, _randomRotate * Time.deltaTime);
    }

    public void ExplodeAsteroid()
    {
        Instantiate(_explodeAsteroid, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.2f);
    }
}
