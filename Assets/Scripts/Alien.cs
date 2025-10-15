using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : Unit // INHERITANCE
{
    private Rigidbody alienRb; // ENCAPSULATION 
    private GameObject player;
    private SpawnManager spawnManager;
    public ParticleSystem alienExplosionParticle;
    private float xBound = 600.0f;

    void Start()
    {
        alienRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = FindFirstObjectByType<SpawnManager>();
        Speed = spawnManager.alienSpeed;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        Speed = spawnManager.alienSpeed;

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        alienRb.AddForce(lookDirection * Speed * Time.deltaTime); 

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xBound, xBound); 
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -4200.0f, 9600.0f);
        transform.position = clampedPosition;

        float destroyDistance = 150.0f;
        if (player != null && transform.position.z < player.transform.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }

    }

    public override void EndorWin()
    {
        if (alienExplosionParticle != null)
        {
            Instantiate(alienExplosionParticle, transform.position, alienExplosionParticle.transform.rotation);
        }

        Debug.Log("Score +1");
        GameManager.Instance.AddPoint(1);

        base.EndorWin(); // POLYMORPHISM


    }

}