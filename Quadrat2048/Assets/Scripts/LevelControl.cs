using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private GameObject[] arTile;
    private int[] pole;
    private int indMaxZn = 1;
    private int[] arZn = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2096};

    // Start is called before the first frame update
    void Start()
    {
        arTile = new GameObject[16];
        pole = new int[16];
        GenerateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateTile()
    {
        List<int> freeCeils = new List<int>();
        int i, n, zn = 1;
        for (i = 0; i < 16; i++)
        {
            if (pole[i] == 0) freeCeils.Add(i);
        }
        if (freeCeils.Count > 0)
        {
            n = freeCeils[Random.Range(0, freeCeils.Count)];
            zn = arZn[Random.Range(0, indMaxZn)];
            Vector3 pos = new Vector3(-3 + 2 * (n % 4), 0, -3 + 2 * (n / 4));
            Color col = Color.cyan;
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.GetComponent<TileControl>().SetNumber(zn, col);
        }
        else
        {   //  Game over

        }
    }

    public void OnClickDirection(int n)
    {

    }
}
