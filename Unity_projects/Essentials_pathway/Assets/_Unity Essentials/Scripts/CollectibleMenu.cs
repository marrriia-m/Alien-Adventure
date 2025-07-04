using UnityEngine;
using System.Collections;

public class CollectibleMenu : MonoBehaviour
{
    public float rotationSpeed;
    public float respawnDelay;
    public float  spawnRadius;
    public float  viewportMargin = 0.05f;       // keep this % away from the edges
    public int    maxSpawnTries = 5; 
    public GameObject onCollectEffect;

    Vector3 startPos;
    Quaternion startRot;
    Collider col;
    Renderer[]  renderers;
    Camera cam;

    void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        col      = GetComponent<Collider>();
        col.isTrigger = true;
        renderers = GetComponentsInChildren<Renderer>(true);
        cam        = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // tell listeners that the star already exists in the scene
        CollectibleEvents.OnStarRespawned?.Invoke(transform);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the collectible constantly
       transform.Rotate(0,rotationSpeed,0); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // hide object (renderers + collider)
            SetVisible(false);

            //instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // tell everyone the object has been collected
            CollectibleEvents.OnStarCollected?.Invoke();

            // schedule the respawn
            StartCoroutine(RespawnRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        // choose a random visible point
        Vector3 newPos = ChooseVisiblePoint();
        transform.SetPositionAndRotation(newPos, startRot);
        SetVisible(true);

        CollectibleEvents.OnStarRespawned?.Invoke(transform);
    }

    void SetVisible(bool v)
    {
        foreach (var r in renderers) r.enabled = v;
        col.enabled = v;               // keep script alive, just toggle collider
    }

    Vector3 ChooseVisiblePoint()
    {
        // try a few times to find a point that is on-screen
        for (int i = 0; i < maxSpawnTries; i++)
        {
            Vector2 circle = Random.insideUnitCircle * spawnRadius;
            Vector3 candidate = startPos + new Vector3(circle.x, 0f, circle.y);

            Vector3 vp = cam.WorldToViewportPoint(candidate);

            bool inFront = vp.z > 0f;
            bool inside  = vp.x > viewportMargin && vp.x < 1f - viewportMargin &&
                           vp.y > viewportMargin && vp.y < 1f - viewportMargin;

            if (inFront && inside)
                return candidate;
        }

        // fallback: original start position
        return startPos;
    }
}
