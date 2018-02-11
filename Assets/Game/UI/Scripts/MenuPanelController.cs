using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zenject;

public class MenuPanelController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] private UILabel gameFinishedLabel;
    [SerializeField] private GameObject resumeButton;
    
    #endregion
    
    #region Private fields

    [Inject] private GameController gameController;
    
    #endregion
    
    #region Public methods

    public void Open(bool isPause)
    {
        if (isPause)
        {
            gameFinishedLabel.gameObject.SetActive(false);
            resumeButton.SetActive(true);
        }
        else
        {
            gameFinishedLabel.text = string.Format("Game over! Your score is: {0}", gameController.Score);            
            gameFinishedLabel.gameObject.SetActive(true);
            resumeButton.SetActive(false);
        }
        
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    #endregion
}