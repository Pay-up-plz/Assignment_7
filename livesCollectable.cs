using UnityEngine;
using System.Collections;

public class livesCollectable : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip collectSound;

    // Use this for initialization
    void Start()
    {
        audioSource = GameObject.Find("collectibleAS").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Animal"))
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
                StatsManager.lives++;
                Destroy(gameObject);
            }

        }

    }
}

