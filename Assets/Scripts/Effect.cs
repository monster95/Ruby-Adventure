using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }

}
