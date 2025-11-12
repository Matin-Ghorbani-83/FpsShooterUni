using System.Collections.Generic;
using UnityEngine;
public class SpawnerGood : MonoBehaviour
{
    public GameObject prefab;
    Queue<GameObject> pool = new Queue<GameObject>();
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            var go = Instantiate(prefab);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }
    void Update()
    {
        var go = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);
        go.transform.SetPositionAndRotation(Random.insideUnitSphere * 10f, Quaternion.identity);
        go.SetActive(true);
        StartCoroutine(Return(go));
    }
    System.Collections.IEnumerator Return(GameObject go)
    {
        yield return new WaitForSeconds(0.05f);
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
