using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AppearItems : MonoBehaviour
{
    public GameObject bulletPrefab;
    
 
    void Start()
    {
        /*StartCoroutine(AppearItem());*/
    }
    /*IEnumerator AppearItem()
    {

        yield return new WaitForSeconds(1.0f);
        float x = Random.Range(-50.0f, 50.0f);
        float z = Random.Range(-47.0f, 45.0f);
        Vector3 randomPoint = new Vector3(x, 3.0f, z);
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit navMeshHit, 10.0f, NavMesh.AllAreas)) {
            Instantiate(bulletPrefab, navMeshHit.position, Quaternion.identity); 
        }
    }*/
    // Update is called once per frame

    void AppearItem()
    {
        float x = Random.Range(-50.0f, 50.0f);
        float z = Random.Range(-47.0f, 45.0f);
        Vector3 randomPoint = new Vector3(x, 3.0f, z);
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit navMeshHit, 10.0f, NavMesh.AllAreas))
        {
            Instantiate(bulletPrefab, navMeshHit.position, Quaternion.identity);

        }
     }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AppearItem();
        }
    }
}
