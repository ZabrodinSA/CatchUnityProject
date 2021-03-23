using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int life;
    public int score;
    public string name;
    public double spawnPeriod = 0.5;
    public float speedIncrease = 0.01f;
    public GameObject finishPanel;
    public GameObject startPanel;
    public GameObject gameWindow;
    public GameObject goodTarget;
    public GameObject badTarget;
    public GameObject client;
    private APIClient apiClient;
    private float spawnCouter = 0;
    private int top;
    private int bot;
    private bool gameIsOn = false;
    
    void Start()
    {
        if (finishPanel == null)
        {
            Debug.Log("Finish Panel has been not added");
        }
        if (startPanel == null)
        {
            Debug.Log("Start Panel has been not added");
        }
        apiClient = client.GetComponent<APIClient>();
        startPanel.SetActive(true);
        bot = -Camera.main.pixelHeight/2+15;
        top = Camera.main.pixelHeight/2-15;
        badTarget.transform.localScale = new Vector3(Camera.main.pixelHeight / 8, Camera.main.pixelHeight / 8);
        goodTarget.transform.localScale = new Vector3(Camera.main.pixelHeight / 8, Camera.main.pixelHeight / 8);
    }

    void Update()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < target.Length; i++)
        {
            var moveController = target[i].GetComponent<MoveController>();
            moveController.speed = moveController.speed+speedIncrease;
        }
        if (gameIsOn && spawnCouter>spawnPeriod && target.Length<50)
        {
            GeneratingTarget();
            spawnPeriod /= 1.05;
            spawnCouter = 0;
        }
        spawnCouter += Time.deltaTime;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameIsOn = true;
        GeneratingTarget();
    }

    private void EndGame()
    {
        gameIsOn = false;
        GameObject[] target = GameObject.FindGameObjectsWithTag("Target");
        for (int i =0; i < target.Length; i++)
        {
            Destroy(target[i]);
        }
        finishPanel.SetActive(true);
        apiClient.SendPlayerInServer();
    }

    public void UpScore()
    {
        score++;
    }

    public void DownLife()
    {
        life--;
        if (life <= 0)
        {
            EndGame();
        }
    }

    private void GeneratingTarget()
    {
        var random = new System.Random();
        var n = random.Next(bot, top);
        var m = random.Next(bot, top);
        Vector3 vector3forBadTarget = new Vector3(-Camera.main.pixelWidth / 2, n, 0);
        Vector3 vector3forGoodTarget = new Vector3(-Camera.main.pixelWidth / 2, m, 0);
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        if (n * m > 0)
        {
            Instantiate(goodTarget, vector3forGoodTarget, quaternion);
        }
        else
        {
            Instantiate(badTarget, vector3forBadTarget, quaternion);
        }
    }
}
