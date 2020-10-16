using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string label = "Room";
    public bool isLightOn = false;
    public CameraBounds cameraBounds;
    public float floor;

    void OnEnable()
    {
        LevelManager.onInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onInit -= OnLoad;
    }

    void OnLoad()
    {
        LevelManager.Instance.RegisterRoom(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(cameraBounds.minimumX, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(cameraBounds.maximumX, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, cameraBounds.minimumY));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, cameraBounds.maximumY));
    }
}
