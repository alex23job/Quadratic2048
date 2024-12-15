using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileControl : MonoBehaviour
{
    [SerializeField] private Text txtNumber;
    [SerializeField] private Image imgTile;
    [SerializeField] private float speed = 5f;

    private bool isMove = false;
    private Vector3 target;
    private int number;

    public int Number { get { return number; } }

    // Start is called before the first frame update
    void Start()
    {
        //target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            if (Vector3.Distance(transform.position, target) <= 0.1f)
            {
                isMove = false;
                transform.position = target;
                /*currentPointIndex++;
                if (points.Length > 0) currentPointIndex %= points.Length;
                target = points[currentPointIndex];*/
            }
            else
            {
                Vector3 delta = target - transform.position;
                transform.Translate(delta.normalized * speed * Time.deltaTime, Space.World);
            }
        }
    }

    public void SetNumber(int zn, Color col)
    {
        number = zn;
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
