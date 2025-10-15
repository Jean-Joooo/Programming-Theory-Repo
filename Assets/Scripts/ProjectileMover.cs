using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 400.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > 9500 || transform.position.z < -3500)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("Alien"))

        {

            Alien alienScript = other.GetComponent<Alien>();


            if (alienScript != null)

            {
                alienScript.EndorWin(); 
            }

            gameObject.SetActive(false);

        }

    }
}