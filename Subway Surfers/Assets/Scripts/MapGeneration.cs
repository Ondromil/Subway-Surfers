using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    private Vector3 newTilePosition = new Vector3(0, -1.4f, 294);
    private bool numeratorOn = false;
    public Transform parent;

    public List<GameObject> tiles = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject clone;
            clone = Instantiate(tiles[Random.Range(0, 6)], parent);
            clone.transform.position = new Vector3(newTilePosition.x, newTilePosition.y, newTilePosition.z);
            newTilePosition += new Vector3(0f, 0f, 200f);
        }
    }
    
    private void Update()
    {
        if (!numeratorOn)
        {
            StartCoroutine(MakeTile());
        }
    }

    private IEnumerator MakeTile()
    {
        numeratorOn = true;
        GameObject clone;
        clone = Instantiate(tiles[Random.Range(0, 6)], parent);
        clone.transform.position = new Vector3(newTilePosition.x, newTilePosition.y, newTilePosition.z);
        yield return new WaitForSeconds(6f);
        newTilePosition += new Vector3(0f, 0f, 200f);
        numeratorOn = false;
    }
}
