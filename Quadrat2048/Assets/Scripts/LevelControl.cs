using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private GameObject[] arTile;
    private int[] pole;
    private int indMaxZn = 1;
    private int[] arZn = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048};

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
            Vector3 pos = new Vector3(-3 + 2 * (n % 4), 3, -3 + 2 * (n / 4));
            Color col = Color.cyan;
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.GetComponent<TileControl>().SetNumber(zn, col);
            pole[n] = zn;
            arTile[n] = tile;
            pos.y = 0.5f;
            tile.GetComponent<TileControl>().SetTarget(pos);
        }
        else
        {   //  Game over

        }
    }
    private void UpdateTile(int num_pos, int tileZn)
    {
        //int i, n, zn = 1;
        int zn = tileZn * 2;
        if (zn > arZn[indMaxZn]) indMaxZn++;
        if (indMaxZn >= arZn.Length)
        {   //  Win

        }
        else
        {
            Vector3 pos = new Vector3(-3 + 2 * (num_pos % 4), 0.5f, -3 + 2 * (num_pos / 4));
            Color col = Color.cyan;
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.GetComponent<TileControl>().SetNumber(zn, col);
            pole[num_pos] = zn;
            arTile[num_pos] = tile;
        }
    }

    public void OnClickDirection(int n)
    {
        int i;
        if (n == 2)
        {
            for (i = 4; i < 16; i++)
            {
                if (pole[i] > 0)
                {
                    TileControl tc = arTile[i].GetComponent<TileControl>();
                    if (i / 4 == 1)
                    {
                        if (pole[i - 4] == 0) MoveTile(i, i - 4); //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, -3));
                        else
                        {
                            if (pole[i - 4] == pole[i])
                            {
                                pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 4]);
                                UpdateTile(i - 4, pole[i - 4]);
                            }
                        }
                    }
                    if (i / 4 == 2)
                    {
                        if (pole[i - 4] == 0)
                        {
                            if (pole[i - 8] == 0) MoveTile(i, i - 8); //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, -3));
                            else
                            {
                                if (pole[i - 8] == pole[i])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 8]);
                                    UpdateTile(i - 8, pole[i - 8]);
                                }
                                else
                                {
                                    MoveTile(i, i - 4);
                                    //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, -1));
                                }
                            }
                        }
                        else
                        {
                            if (pole[i - 4] == pole[i])
                            {
                                pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 4]);
                                UpdateTile(i - 4, pole[i - 4]);
                            }
                        }
                    }
                    if (i / 4 == 3)
                    {
                        if (pole[i - 4] == 0)
                        {
                            if (pole[i - 8] == 0)
                            {
                                if (pole[i - 12] == 0) MoveTile(i, i - 12); //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, -3));
                                else
                                {
                                    if (pole[i - 12] == pole[i])
                                    {
                                        pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 12]);
                                        UpdateTile(i - 12, pole[i - 12]);
                                    }
                                    else
                                    {
                                        MoveTile(i, i - 8);
                                        //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, 1));
                                    }
                                }
                            }
                            else
                            {
                                if (pole[i - 8] == pole[i])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 8]);
                                    UpdateTile(i - 8, pole[i - 8]);
                                }
                                else
                                {
                                    MoveTile(i, i - 4);
                                    //tc.SetTarget(new Vector3(-3 + 2 * (i % 4), 0, -1));
                                }
                            }
                        }
                        else
                        {
                            if (pole[i - 4] == pole[i])
                            {
                                pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 4]);
                                UpdateTile(i - 4, pole[i - 4]);
                            }
                        }
                    }
                }
            }
        }
        Povtor(n);
        GenerateTile();
    }

    private void Povtor(int direction)
    {
        for(int i = 0; i < 16; i++)
        {
            if (pole[i] > 0)
            {
                if (((i % 4) < 3) && (pole[i] == pole[i + 1]))
                {
                    if (direction == 3)
                    {
                        pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i + 1]);
                        UpdateTile(i + 1, pole[i + 1]);
                    }
                    else
                    {
                        pole[i + 1] = 0; Destroy(arTile[i]); Destroy(arTile[i + 1]);
                        UpdateTile(i, pole[i]);
                    }
                }
                if (((i / 4) < 3) && (pole[i] == pole[i + 4]))
                {
                    if (direction == 0)
                    {
                        pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i + 4]);
                        UpdateTile(i + 4, pole[i + 4]);
                    }
                    else
                    {
                        pole[i + 4] = 0; Destroy(arTile[i]); Destroy(arTile[i + 4]);
                        UpdateTile(i, pole[i]);
                    }
                }
            }
        }
    }

    private void MoveTile(int src, int dst)
    {
        TileControl tc = arTile[src].GetComponent<TileControl>();
        tc.SetTarget(new Vector3(-3 + 2 * (dst % 4), 0.5f, -3 + 2 * (dst / 4)));
        pole[dst] = pole[src];pole[src] = 0;
        arTile[dst] = arTile[src];arTile[src] = null;
    }
}
