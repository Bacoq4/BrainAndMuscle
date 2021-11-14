using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;
    public Vector3 rotateVector;

    void Update()
    {
        transform.Rotate(rotateVector * speed * Time.deltaTime);
    }
}
