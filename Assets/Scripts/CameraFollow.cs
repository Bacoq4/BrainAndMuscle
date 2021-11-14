using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    public Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        diff = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + diff;
    }
}