using UnityEngine;

public class SpawnerBad : MonoBehaviour
{
    public GameObject prefab;
    void Update()
    {
        var go = Instantiate(prefab, Random.insideUnitSphere * 10f, Quaternion.identity);
        Destroy(go, 0.05f); // نابودی سریع = کلی GC و کار اضافه
    }
}
