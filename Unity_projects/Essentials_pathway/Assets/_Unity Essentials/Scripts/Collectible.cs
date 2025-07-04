using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;

    void Awake()
    {
    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            Destroy(gameObject);

            //instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }


    }

}
