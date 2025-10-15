using UnityEngine;

public class Unit : MonoBehaviour
{
    private float speed = 8.0f; 

    public float Speed // ENCAPSULATION
    {
        get { return speed; }
        set { speed = value; }
    }
    public virtual void EndorWin() // POLYMORPHISM
    {
        Destroy(gameObject);
    }
}