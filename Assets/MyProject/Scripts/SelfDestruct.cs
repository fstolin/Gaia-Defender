using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    void Update()
    {
        if (!this.GetComponent<ParticleSystem>().isPlaying) Destroy(this.gameObject);
    }

}
