using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour
{
    [Header("Spawn Position")]
    [SerializeField] private Transform player;
    [SerializeField] public GameObject entityGameObject;
    public LayerMask ground;

    [Header("Spawn Conditions")]
    private bool asSpawned = false;

    private void Start()
    {
        asSpawned = false;
        StartCoroutine(SpawnEntity(70, 120)); //Will spawn betwen 70 & 120 seconds
    }

    private IEnumerator SpawnEntity(int minTime, int maxTime) 
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        float x = RandomPosition(player.transform.localPosition.x + 10f, player.transform.localPosition.x + 20f);
        float z = RandomPosition(player.transform.localPosition.z + 10f, player.transform.localPosition.z + 20f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 0f, z), Vector3.down, out hit, ground))
        {
            Debug.Log("Teste");
            if (asSpawned == false)
            {
                Instantiate(entityGameObject, new Vector3(hit.point.x, player.transform.localPosition.y, hit.point.z), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                asSpawned = true;
            }
        }

    }

    public float RandomPosition(float min, float max) //Calculation of the position of the object
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }

    public void DespawnEntity()
    {
            Destroy(GameObject.FindGameObjectWithTag("Entity"));
            Debug.Log("Destroyed");
            asSpawned = false;
            StopAllCoroutines();
            StartCoroutine(SpawnEntity(70, 120)); 
    }    
}




