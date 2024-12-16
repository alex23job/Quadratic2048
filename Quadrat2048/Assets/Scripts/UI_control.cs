using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_control : MonoBehaviour
{
    [SerializeField] private Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
        ViewScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewScore(int sc)
    {
        txtScore.text = $"Очки : {sc:D05}";
    }

    public void LoadMenu()
    {

    }
}
