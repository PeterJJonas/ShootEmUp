using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _shieldUpSpeed = 4f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _shieldUpSpeed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }
}
