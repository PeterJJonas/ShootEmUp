using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerSriptStart : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private playerScript _score;
    private float _playerScore;

    void Start()
    {
        if (GameObject.Find("Player)") != null)
        {
            _scoreText.text = "Score: 000";
            _score = GameObject.Find("Player").GetComponent<playerScript>();
            _playerScore = _score.GetPlayerScore();
            _scoreText.text = "Score: " + _playerScore;
        }
    }
}
