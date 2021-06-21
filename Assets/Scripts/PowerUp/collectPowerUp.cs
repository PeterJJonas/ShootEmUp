using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectPowerUp : MonoBehaviour
{
    [SerializeField] private AudioClip _powerUpAudio;
    // Start is called before the first frame update
    void Start()
    {
        //CollectPowerSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectPowerSound()
    {
        AudioSource.PlayClipAtPoint(_powerUpAudio, transform.position);
    }
}
