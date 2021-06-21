using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 3f;

    /*[SerializeField]
    private int powerUpID;*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }
}
