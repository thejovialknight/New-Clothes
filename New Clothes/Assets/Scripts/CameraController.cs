using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public CameraBounds bounds;
    public float speed = 4f;
    public Vector3 target;

    public bool isYOverride;
    public float yOverride;
    public bool isXOverride;
    public float xOverride;

    public static CameraController Instance;

    public void SetBounds(CameraBounds newBounds, bool isInstantaneous)
    {
        bounds = newBounds;

        if(isInstantaneous)
        {
            SetTarget();
            transform.position = target;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        cam = GetComponent<Camera>();
    }

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
        LevelManager.Instance.RegisterCamera(this);
    }

    void Update()
    {
        bounds = LevelManager.Instance.playerLocation.cameraBounds;

        SetTarget();
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);

        if(isYOverride)
        {
            transform.position = new Vector3(transform.position.x, yOverride, transform.position.z);
        }

        if (isXOverride)
        {
            transform.position = new Vector3(xOverride, transform.position.y, transform.position.z);
        }
    }

    void SetTarget()
    {
        float xPos = Mathf.Clamp(player.position.x, bounds.minimumX, bounds.maximumX);
        float yPos = Mathf.Clamp(player.position.y, bounds.minimumY, bounds.maximumY);
        target = new Vector3(xPos, yPos, transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(bounds.minimumX, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(bounds.maximumX, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, bounds.minimumY));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, bounds.maximumY));
    }
}
