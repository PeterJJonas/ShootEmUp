using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerSript : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _gameOverText;
        /* Other option:
        [SerializeField] private Text _gameOverText;
        Second part: see down
        */
    [SerializeField] private Text _yourScoreText;
    [SerializeField] private GameObject _backToMenu;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _livesSprites;

    private int _playerLives;
    private playerScript _player;
    private float _playerScore;

    void Start()
    {
        _scoreText.text = "Score: 000";
        if (GameObject.Find("Player") != null)
        {
            _player = GameObject.Find("Player").GetComponent<playerScript>();
            _playerLives = _player.GetPlayerLives();
        }
    }

    void Update()
    {
        if (_playerLives > 0)
        {
            _playerScore = _player.GetPlayerScore();
            _scoreText.text = "Score: " + _playerScore.ToString();
            _playerLives = _player.GetPlayerLives();
            _livesImage.sprite = _livesSprites[_playerLives];
        }

        else if (_playerLives == 0)
        {
            _playerLives--;
            _scoreText.gameObject.SetActive(false);
            _gameOverText.SetActive(true);
                //_gameOverText.gameObject.SetActive(true);
            _backToMenu.SetActive(true);
            _yourScoreText.text = "Your Score: \n" + _playerScore.ToString();
            _yourScoreText.gameObject.SetActive(true);
            StartCoroutine(GameOver(0.4f));
        }
    }

    IEnumerator GameOver(float Flick)
    {
        while (true)
        {
            _gameOverText.SetActive(true);
                //if (_gameOverText.activeSelf)
            yield return new WaitForSeconds(Flick);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(Flick);
        }
    }
}
