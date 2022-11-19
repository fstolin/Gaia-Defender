using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        Destroy(this.gameObject);
    }

}
