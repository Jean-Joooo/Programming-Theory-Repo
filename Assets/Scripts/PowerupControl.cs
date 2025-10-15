using UnityEngine;

public class PowerupControl : MonoBehaviour
{
    
    public float destroyDistance = 150.0f;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        if (transform.position.z < player.transform.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }

}