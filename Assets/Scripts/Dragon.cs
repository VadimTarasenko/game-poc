using UnityEngine;

public class Dragon : MonoBehaviour
{
    public GameObject dragon;
    public Terrain terrain;

    void Start()
    {
        Vector3 dragonPosition = dragon.transform.position;
        float terrainHeight = terrain.SampleHeight(dragonPosition);
        dragon.transform.position = new Vector3(dragonPosition.x, terrainHeight + 1.0f, dragonPosition.z);
    }
}
