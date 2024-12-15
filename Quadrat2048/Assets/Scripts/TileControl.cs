using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControl : MonoBehaviour
{
    [SerializeField] private Text txtNumber;
    [SerializeField] private Image imgTile;
    [SerializeField] private float speed = 3f;

    private bool isMove = false;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNumber(int zn, Color col)
    {
        txtNumber.text = zn.ToString();
        txtNumber.color = col;
    }

    public void SetFoneColor(Color col)
    {
        imgTile.color = col;
    }

    public void SetImg(Sprite spr)
    {
        imgTile.color = Color.white;
        imgTile.sprite = spr;
    }

    public void SetTarget(Vector3 tg)
    {
        target = tg;
        isMove = true;
    }
}
