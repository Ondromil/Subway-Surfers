using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    private Vector3 newTilePosition = new Vector3(0, -1.4f, 294);
    private bool numeratorOn = false;
    public Transform parent;
    private GameObject clone;

    public List<GameObject> tiles = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < 3; i++)                                                       // Na začátku scény se vytvoří 4 tily, aby měl hráč hned nějakej prostor
        {
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
        numeratorOn = true;                                                                        // Vybere náhodny tile z listu a položí ho na pozici za předchozim tilem
        clone = Instantiate(tiles[Random.Range(0, 6)], parent);
        clone.transform.position = new Vector3(newTilePosition.x, newTilePosition.y, newTilePosition.z);
        yield return new WaitForSeconds(6f);
        newTilePosition += new Vector3(0f, 0f, 200f);
        numeratorOn = false;
    }
}
