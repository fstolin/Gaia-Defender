using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{

    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int ScoreAwarded;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(ScoreAwarded);
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
