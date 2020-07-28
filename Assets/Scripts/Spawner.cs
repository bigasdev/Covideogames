using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spawner : MonoBehaviour
{
    
    public Transform spawnLocation;
    public GameObject[] npcPrefabs;
    public GameObject npcHolder;
    public float spawnTimer = 20f;
    public int spawnLimit = 10;
    public bool isSpawning = false;

    AudioSource audioSource;
    public AudioClip spawnSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = true;
        StartCoroutine(SpawnNPC());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) StopAllCoroutines();
    }

    public IEnumerator SpawnNPC()
    {
        yield return new WaitForSeconds(spawnTimer);
        int i = 0;
        while (i < spawnLimit)
        {
            GameObject newNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], spawnLocation.position, Quaternion.identity, npcHolder.transform);
            audioSource.PlayOneShot(spawnSFX);
            i++;
            spawnTimer *= .95f;
            yield return new WaitForSeconds(spawnTimer);
           
        }
    }
}
