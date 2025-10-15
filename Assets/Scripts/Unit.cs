using UnityEngine;

public class Unit : MonoBehaviour
{
    private float speed = 10.0f;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public virtual void EndorWin()
    {
        Destroy(gameObject);
    }
}