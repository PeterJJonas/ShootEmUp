using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] private float _speedHidden = 6.0f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _ShieldPrefab;
    [SerializeField] private float _fireRate = 1.2f;
    [SerializeField] private float _canFire = 0;
    [SerializeField] private int _maxShoot = 5;
    [SerializeField] private Material _defaultMat;
    [SerializeField] private Material _overheatedMat;
        /* [SerializeField] private Material _getHitMat;*/
    [SerializeField] private int _playerLives = 3;
    [SerializeField] private bool _isTripleShotAvtive = false;
    [SerializeField] private bool _isSpeedUpActive = false;
    [SerializeField] private bool _isShieldUpActive = false;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private int _playerScore = 0;
    [SerializeField] private GameObject[] _damageFire;
    [SerializeField] private GameObject _playerExplosion;
    //[SerializeField] private GameObject _playerTruster;
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private AudioClip _powerUpSound;

    private enemyHiveScript _stopSpawnEnemy;
    private powerUpHiveSript _stopSpawnPowerUp;
    //private asteroidScript _explosionOnAsteroid;
    private AudioSource _audioToPlay;
    private collectPowerUp _collectPowerSound;

    void Start()
    {
        _collectPowerSound = GameObject.Find("CollectPowerUp").GetComponent<collectPowerUp>();
        _audioToPlay = GetComponent<AudioSource>();
        if (_audioToPlay == null)
        {
            Debug.LogError("No audio source on player");
        }
        transform.position = new Vector3(0, 0, 0);

        _stopSpawnEnemy = GameObject.Find("EnemyHive").GetComponent<enemyHiveScript>();
        if (_stopSpawnEnemy == null)
        {
            Debug.LogError("No enemy Hive Script found!");
        }

        _stopSpawnPowerUp = GameObject.Find("PowerUpHive").GetComponent<powerUpHiveSript>();
        if (_stopSpawnPowerUp == null)
        {
            Debug.LogError("No Power Up Hive Scrift Found");
        }
    }

    void Update()
    {
        PlayerControll();
        FireLaser();
        SpeedUp();
    }

    private void PlayerControll()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speedHidden * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.66f), 0);

        if (transform.position.x <= -10.63f)
        {
            transform.position = new Vector3(10.9f, transform.position.y, 0);
        }

        else if (transform.position.x >= 10.9f)
        {
            transform.position = new Vector3(-10.63f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        if (Time.time > _canFire)

            if (_isTripleShotAvtive == false)
            {
                {
                    GetComponent<Renderer>().material = _defaultMat;
                    if (Input.GetKeyDown(KeyCode.Space) && _maxShoot > 0)
                    {
                        _maxShoot = _maxShoot - 1;
                        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                        _audioToPlay.clip = _laserSound;
                        _audioToPlay.Play();
                        //_audioToPlay.clip = null;
                    }
                    else if (_maxShoot <= 0)
                    {
                        _canFire = Time.time + _fireRate;
                        _maxShoot = 5;
                        GetComponent<Renderer>().material = _overheatedMat;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    _audioToPlay.clip = _laserSound;
                    _audioToPlay.Play();
                    //_audioToPlay.clip = null;
                }
            }
    }

    private void SpeedUp()
    {
        if (_isSpeedUpActive == true)
        {
            _speedHidden = 25f;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "PowerUp":
                _collectPowerSound.CollectPowerSound();
                _isTripleShotAvtive = true;
                StartCoroutine(StopTripleShot(5.0f));
                Destroy(other.gameObject);
                break;


            case "SpeedUp":
                _collectPowerSound.CollectPowerSound();
                _isSpeedUpActive = true;
                StartCoroutine(StopSpeedUp(3.0f));
                Destroy(other.gameObject);
                break;

            case "ShieldUp":
                _collectPowerSound.CollectPowerSound();
                _isShieldUpActive = true;
                StartCoroutine(StopShieldUp(5.0f));
                Destroy(other.gameObject);
                break;

            case "Asteroid":

                Debug.Log("Hit by asteroid!");
                if (_isShieldUpActive == true)
                {
                    _isShieldUpActive = false;
                    _shieldVisualizer.SetActive(false);
                }
                else
                {
                    PlayerDamage();
                    Debug.Log("Asteroid Asteroid");
                    other.GetComponent<asteroidScript>().ExplodeAsteroid();
                    /*
                     * _explosionOnAsteroid = GameObject.Find("Asteroid").GetComponent<asteroidScript>();
                    if (_explosionOnAsteroid == null)
                    {
                        Debug.LogError("No Asteroid Script Found");
                    }
                    else
                    {
                        _explosionOnAsteroid.ExplodeAsteroid();
                    }
                    */
                }
                break;
        }
    }

    IEnumerator StopTripleShot(float TripleShotTime)
    {
        while (_isTripleShotAvtive)
        {
            _audioToPlay.clip = _powerUpSound;
            _audioToPlay.Play();
            _isTripleShotAvtive = true;
            Debug.Log("_isTripleShotAvtive = true");
            GetComponent<Renderer>().material = _defaultMat;
            PlayerScore(5);
            yield return new WaitForSeconds(TripleShotTime);
            _isTripleShotAvtive = false;
        }
    }

    IEnumerator StopSpeedUp(float SpeedUpTime)
    {
        while (_isSpeedUpActive)
        {
            PlayerScore(8);
            yield return new WaitForSeconds(SpeedUpTime);
            _isSpeedUpActive = false;
            _speedHidden = 6.0f;
        }
    }

    IEnumerator StopShieldUp(float ShieldUpTime)
    {
        while (_isShieldUpActive)
        {
            PlayerScore(3);
                //Instantiate(_ShieldPrefab, transform.position, Quaternion.identity);
                // Switch shield on instead
            _shieldVisualizer.SetActive(true);
            yield return new WaitForSeconds(ShieldUpTime);
            _isShieldUpActive = false;
            _shieldVisualizer.SetActive(false);
        }
    }

    public void PlayerScore(int Score)
    {
        _playerScore += Score;
    }

    public float GetPlayerScore()
    {
        return _playerScore;
    }
    public int GetPlayerLives()
    {
        return _playerLives;
    }

    public void PlayerDamage()
    {
        if (_isShieldUpActive)
        {
            PlayerScore(2);
            return;
        }

        _playerLives--;

        if (_playerLives == 2)
        {
            _damageFire[0].SetActive(true);
        }
        else if (_playerLives == 1)
        {
            _damageFire[1].SetActive(true);
        }
        else if (_playerLives < 1)
        {
            _stopSpawnPowerUp.OnPlayerDeath();
            _stopSpawnEnemy.OnPlayerDeath();
            Instantiate(_playerExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1f);
        }
    }
}
