using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTile : MonoBehaviour
{
    public GameObject tile;
    private void Start()
    {
        StartCoroutine(DeleteWaitTime());
    }
    
    private IEnumerator DeleteWaitTime()
    {
        yield return new WaitForSeconds(100f);
        if (gameObject.name != "Tile_1" && gameObject.name != "Tile_2" && gameObject.name != "Tile_3" && gameObject.name != "Tile_4" && gameObject.name != "Tile_5" && gameObject.name != "Tile_6")
        {
            GameObject.Destroy(tile);
        }
    }
}