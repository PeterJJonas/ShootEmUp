using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 2.4f);
    }
}
