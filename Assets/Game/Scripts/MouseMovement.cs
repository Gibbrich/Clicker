using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zenject;

public class MouseMovement : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private int boundary = 1;

    #endregion

    #region Private fields

    [Inject] private GameSettings gameSettings;
    [Inject] private GameController gameController;

    private int width;
    private int height;

    #endregion

    #region Unity callbacks

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }


    void Update()
    {
        if (gameController.State == GameState.PLAY)
        {
            if (Input.mousePosition.x > width - boundary)
            {
                Move(new Vector3(-1 * Time.deltaTime * speed, 0.0f, 0.0f));
            }

            if (Input.mousePosition.x < 0 + boundary)
            {
                Move(new Vector3(1 * Time.deltaTime * speed, 0.0f, 0.0f));
            }

            if (Input.mousePosition.y > height - boundary)
            {
                Move(new Vector3(0.0f, -1 * Time.deltaTime * speed, 0.0f));
            }

            if (Input.mousePosition.y < 0 + boundary)
            {
                Move(new Vector3(0.0f, 1 * Time.deltaTime * speed, 0.0f));
            }
        }
    }

    #endregion

    #region Private methods

    private void Move(Vector3 offset)
    {
        Vector3 newPos = transform.position - offset;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, 0, gameSettings.FieldSettings.Width),
                                         Mathf.Clamp(newPos.y, 0, gameSettings.FieldSettings.Height),
                                         newPos.z);
    }

    #endregion
}