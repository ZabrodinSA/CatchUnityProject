using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterName : MonoBehaviour
{
    private InputField inputField;
    private GameObject controller;
    private GameController gameController;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        inputField = GetComponent<InputField>();
    }


    public void Enter()
    {
        gameController.name = inputField.text;
        gameController.StartGame();
     }
}
