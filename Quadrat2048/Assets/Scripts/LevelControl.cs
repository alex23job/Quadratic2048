using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private UI_control ui_Control;

    private GameObject[] arTile;
    private int[] pole;
    private int indMaxZn = 0;
    private int[] arZn = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
    private Color[] arCol = { Color.cyan, Color.red, Color.yellow, Color.green, Color.blue, Color.magenta, new Color(0.7f, 0.6f, 0.3f), new Color(0.1f, 0.8f, 0.7f), new Color(0.9f, 0.7f, 0.8f), new Color(0.8f, 0.4f, 0.9f), new Color(0.5f, 0.8f, 0.3f), new Color(0.4f, 0.6f, 0.9f) };
    private int[] arMaxInd = { 2048, 3072, 3584, 3840, 3968, 4032, 4064, 4080, 4088, 4092, 4094, 4095 };
    private List<int> arRndZn = new();

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        arTile = new GameObject[16];
        pole = new int[16];
        int i, j, lim;
        for(i = 0; i < 12; i++)
        {
            lim = 1; for (j = i; j < 11; j++) lim *= 2;
            //print($"lim = {lim}");
            for (j = 0; j < lim; j++) arRndZn.Add(arZn[i]);
        }
        //print(arRndZn);
        Invoke("GenerateTile", 0.5f);
        //GenerateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateTile()
    {
        List<int> freeCeils = new List<int>();
        int i, n, zn = 1, nzn;
        for (i = 0; i < 16; i++)
        {
            if (pole[i] == 0) freeCeils.Add(i);
        }
        if (freeCeils.Count > 0)
        {
            n = freeCeils[Random.Range(0, freeCeils.Count)];
            //zn = arZn[Random.Range(0, indMaxZn)];
            nzn = Random.Range(0, arMaxInd[indMaxZn]);
            zn = arRndZn[nzn];
            score += zn;
            ui_Control.ViewScore(score);
            Vector3 pos = new Vector3(-3 + 2 * (n % 4), 3, -3 + 2 * (n / 4));
            Color col = GetColor(zn);
            //print($"num_zn => {nzn}    zn => {zn}    col => {col}");
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.GetComponent<TileControl>().SetNumber(zn, col);
            pole[n] = zn;
            arTile[n] = tile;
            pos.y = 0.5f;
            tile.GetComponent<TileControl>().SetTarget(pos);
        }
        else
        {   //  Game over
            ui_Control.ViewLossPanel();
        }
    }

    private Color GetColor(int zn)
    {
        switch(zn)
        {
            case 1: return Color.cyan;
            case 2: return Color.red;
            case 4: return Color.yellow;
            case 8: return Color.green;
            case 16: return Color.blue;
            case 32: return Color.magenta;
            case 64: return new Color(0.7f, 0.6f, 0.3f);
            case 128: return new Color(0.1f, 0.8f, 0.7f);
            case 256: return new Color(0.9f, 0.7f, 0.8f);
            case 512: return new Color(0.8f, 0.4f, 0.9f);
            case 1024: return new Color(0.5f, 0.8f, 0.3f);
            case 2048: return new Color(0.4f, 0.6f, 0.9f);
            default: return Color.cyan;
        }
    }
    private void UpdateTile(int num_pos, int tileZn)
    {
        //int i, n, zn = 1;
        int zn = tileZn * 2;
        if (zn > arZn[indMaxZn]) indMaxZn++;
        if (indMaxZn >= arZn.Length)
        {   //  Win
            ui_Control.ViewWinPanel();
        }
        else
        {
            Vector3 pos = new Vector3(-3 + 2 * (num_pos % 4), 0.5f, -3 + 2 * (num_pos / 4));
            Color col = GetColor(zn);
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
            tile.GetComponent<TileControl>().SetNumber(zn, col);
            pole[num_pos] = zn;
            arTile[num_pos] = tile;
        }
    }

    public void OnClickDirection(int n)
    {
        int i;
        bool isMove = true;
        if (n == 0)
        {
            while (isMove)
            {
                isMove = false;
                for (i = 15; i >= 0; i--)
                {
                    if (pole[i] > 0)
                    {
                        if ((i / 4) < 3)
                        {
                            if (pole[i + 4] == 0)
                            {
                                isMove = true;
                                MoveTile(i, i + 4);
                            }
                            else
                            {
                                if (pole[i] == pole[i + 4])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i + 4]);
                                    UpdateTile(i + 4, pole[i + 4]);
                                    isMove = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (n == 1)
        {
            while (isMove)
            {
                isMove = false;
                for (i = 0; i < 16; i++)
                {
                    if (pole[i] > 0)
                    {
                        if ((i % 4) > 0)
                        {
                            if (pole[i - 1] == 0)
                            {
                                isMove = true;
                                MoveTile(i, i - 1);
                            }
                            else
                            {
                                if (pole[i] == pole[i - 1])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 1]);
                                    UpdateTile(i - 1, pole[i - 1]);
                                    isMove = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (n == 2)
        {
            while (isMove)
            {
                isMove = false;
                for (i = 0; i < 16; i++)
                {
                    if (pole[i] > 0)
                    {
                        if ((i / 4) > 0)
                        {
                            if (pole[i - 4] == 0)
                            {
                                isMove = true;
                                MoveTile(i, i - 4);
                            }
                            else
                            {
                                if (pole[i] == pole[i - 4])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i - 4]);
                                    UpdateTile(i - 4, pole[i - 4]);
                                    isMove = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (n == 3)
        {
            while (isMove)
            {
                isMove = false;
                for (i = 0; i < 16; i++)
                {
                    if (pole[i] > 0)
                    {
                        if ((i % 4) < 3)
                        {
                            if (pole[i + 1] == 0)
                            {
                                isMove = true;
                                MoveTile(i, i + 1);
                            }
                            else
                            {
                                if (pole[i] == pole[i + 1])
                                {
                                    pole[i] = 0; Destroy(arTile[i]); Destroy(arTile[i + 1]);
                                    UpdateTile(i + 1, pole[i + 1]);
                                    isMove = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        Invoke("GenerateTile", 0.5f);
    }

    private void MoveTile(int src, int dst)
    {
        TileControl tc = arTile[src].GetComponent<TileControl>();
        tc.SetTarget(new Vector3(-3 + 2 * (dst % 4), 0.5f, -3 + 2 * (dst / 4)));
        pole[dst] = pole[src];pole[src] = 0;
        arTile[dst] = arTile[src];arTile[src] = null;
    }

    public void OnClickDel_1_2()
    {
        Deleting1_2();
    }

    public void Deleting1_2()
    {
        for (int i = 0; i < 16; i++)
        {
            if (pole[i] == 1 || pole[i] == 2)
            {
                pole[i] = 0;
                Destroy(arTile[i]);
                arTile[i] = null;
            }
        }
    }
}
