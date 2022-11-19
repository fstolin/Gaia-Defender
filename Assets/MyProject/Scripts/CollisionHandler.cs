using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashRestartTime = 3f;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject fireFX;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReloadScene(crashRestartTime);
        GetComponent<PlayerController>().SetIsCrashed(true);        
    }

    private void OnTriggerEnter(Collider other)
    {
        ReloadScene(crashRestartTime);
        GetComponent<PlayerController>().SetIsCrashed(true);
        StartCrashPhysics();
        PlayExplosionParticles();
    }

    // Calling the reset screen method in $timeToRestart time
    private void ReloadScene(float timeToRestart)
    {
        Invoke("LoadSceneAfterCrash", timeToRestart);
    }

    // Reseting the scene
    private void LoadSceneAfterCrash()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GetComponent<PlayerController>().SetIsCrashed(false);
        ResetCrashPhysics();
        fireFX.SetActive(false);
    }

    // Starting crash physics to behave like in real life.
    private void StartCrashPhysics()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    // Reset physics to railshooter after restart
    private void ResetCrashPhysics()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void PlayExplosionParticles()
    {
        explosion.Play();
        fireFX.SetActive(true);
    }
}
