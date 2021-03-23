using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    public bool isGood = false;
    private GameObject controller;
    private GameController gameController;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (controller == null)
        {
            Debug.Log("Game Controller not found");
        }
        gameController = controller.GetComponent<GameController>();
        if (gameController == null)
        {
            Debug.Log("Script Game Controller not found");
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.x > Camera.main.pixelWidth/2)
        {
            if (isGood)
            {
                gameController.DownLife();
            }
            Destroy(gameObject);
        }
    }

    public void ClickHandler ()
    {
        if (isGood)
        {
            gameController.UpScore();
        }
        else
        {
            gameController.DownLife();
        }
        Destroy(gameObject);
     }
}
