using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{

    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scoreAwarded = 15;
    [SerializeField] int enemyHitpoints = 40;
    [SerializeField] int damageOnHit = 5;

    GameObject parent;
    GameObject explosionPlayer;
    ScoreBoard scoreBoard;
    //bool IsScoreIncreased;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        // Adding rigidbody because of collisions
        AddRigidBody();
        parent = GameObject.FindWithTag("Respawn");
    }

    private void AddRigidBody()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        DrawHitVFX(other);
        // Kill enemy if hitpoints are below or equals zero
        if (enemyHitpoints <= 0) KillEnemy();
    }

    private void ProcessHit()
    {
        TakeDamage();
    }

    private void DrawHitVFX(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(gameObject, collisionEvents);
        Vector3 collisionSpot = collisionEvents[0].intersection;

        GameObject hitVFXgo = Instantiate(hitVFX, collisionSpot, Quaternion.identity);
        hitVFXgo.transform.parent = parent.transform;
    }

    private void IncreaseScore()
    {
        // If score wasn't increased already for this enemy, increase it
        scoreBoard.IncreaseScore(scoreAwarded);
    }

    private void KillEnemy()
    {
        Debug.Log("explosion at: " + this.transform.position);
        GameObject vfx = Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(this.gameObject);
        IncreaseScore();
    }

    private void TakeDamage()
    {
        // Reducing hitpoints on hit
        enemyHitpoints -= damageOnHit;
    }

    private void OnDestroy()
    {
        explosionPlayer = GameObject.FindGameObjectWithTag("ExplosionPlayer");
        explosionPlayer.GetComponent<AudioSource>().Play();
    }
}
