using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Vector3 topPosition;
    public Vector3 bottomPosition;

    public Room topRoom;
    public Room bottomRoom;

    public float topCameraY;
    public float bottomCameraY;

    public Vector3 Top()
    {
        return topPosition;
    }

    public Vector3 Bottom()
    {
        return bottomPosition;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Top(), 0.25f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Bottom(), 0.25f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, topCameraY), 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, bottomCameraY), 0.1f);
    }
}
