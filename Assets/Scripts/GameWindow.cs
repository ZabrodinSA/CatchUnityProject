using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameWindow : MonoBehaviour
{
    public Text life;
    public Text score;
    public Text playerName;
    GameObject controller;
    GameController gameController;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
    }

    void Update()
    {
        SetLife();
        SetScore();
        SetName();
    }
    public void SetName()
    {
        playerName.text = $"Palyer: {gameController.name}";
    }

    public void SetScore()
    {
        score.text = $"Score: {gameController.score}";
    }

    public void SetLife ()
    {
        life.text = $"Life: {gameController.life}";
    }
}
