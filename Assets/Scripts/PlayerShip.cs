using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Unit
{
    private Rigidbody playerRb;
    private float timer = 0f;
    private float fireRate = 0.5f;
    public bool hasPowerup;
    public int powerUpDuration = 10;
    public AudioClip shootSound;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    private MeshRenderer playerMesh; 
    private Color originalColor;
    
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerMesh = GetComponent<MeshRenderer>();
        if (playerMesh != null)
        {
            originalColor = playerMesh.material.color;
        }
    }
    void Update()
    {
        if (!GameManager.Instance.m_Started)
        {
            return;
        }
        
        timer += Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * Speed * verticalInput);
        playerRb.AddForce(Vector3.right * Speed * horizontalInput);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -450.0f, 450.0f);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -4000.0f, 9500.0f);
        transform.position = clampedPosition;
        if (Input.GetKeyDown(KeyCode.Space) && (timer > fireRate || hasPowerup))
        {
            Debug.Log("SPACE BAR CLICKED");
            playerAudio.PlayOneShot(shootSound, 1.0f);
            GameObject pooledProjectile = ObjectPooler.Instance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = transform.position;
            }
            if (!hasPowerup)
            {
                timer = 0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            playerMesh.material.color = Color.yellow;

            StartCoroutine(PowerupCountdownRoutine());
        }
        else if (other.gameObject.CompareTag("Alien"))
        {
            if (explosionParticle != null)
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            }

            EndorWin();
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;

        if (playerMesh != null)
        {
            playerMesh.material.color = originalColor;
        }

    }
    public override void EndorWin()
    {
        Debug.Log("Game Over!");
        GameManager.Instance.GameOver();
        base.EndorWin();
    }
}