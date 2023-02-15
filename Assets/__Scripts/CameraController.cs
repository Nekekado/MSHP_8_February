using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    [SerializeField] private SpriteRenderer boundsMap;

    private bool playerSeeRight;
    private Vector3 min, max;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        CalculateBounds();
    }

    private void Update()
    {
        Follow();
    }

    void CalculateBounds()
    {
        Bounds bounds = CameraBounds();
        min = bounds.max + boundsMap.bounds.min;
        max = bounds.min + boundsMap.bounds.max;
    }

    Bounds CameraBounds()
    {
        float height = cam.orthographicSize * 2;
        return new Bounds(Vector3.zero, new Vector3(height * cam.aspect, height, 0));
    }

    Vector3 MoveInside(Vector3 current, Vector3 pMin, Vector3 pMax)
    {
        current = Vector3.Max(current, pMin);
        current = Vector3.Min(current, pMax);
        return current;
    }

    void Follow()
    {
        playerSeeRight = player.GetComponent<Player>().IsSeeRight;
        Vector3 position;
        if (playerSeeRight)
        {
            position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
        }
        else
        {
            position = new Vector3(player.transform.position.x - offset.x, player.transform.position.y + offset.y, transform.position.z);
        }
        position = MoveInside(position, new Vector3(min.x, min.y, position.z), new Vector3(max.x, max.y, position.z));
        transform.position = Vector3.Lerp(transform.position, position, damping * Time.deltaTime);
    }
}
