using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] private UILabel playerNameLabel;
    [SerializeField] private UILabel timerLabel;
    [SerializeField] private UILabel scoreLabel;
    [SerializeField] private UIItem correctItemUIController;
    [SerializeField] private GameObject pausePanel;

    #endregion
    
    #region Public methods

    public void UpdateTimerDisplay(int time)
    {
        timerLabel.text = time.ToString();
    }

    public void UpdateDisplayedItem(ItemType type, Color color)
    {
        correctItemUIController.ChangeDisplay(type, color);
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdatePlayerNameDisplay()
    {
        /* todo    - implement
         * @author - Артур
         * @date   - 08.02.2018
         * @time   - 21:43
        */        
    }

    public void OpenPauseMenu()
    {
        correctItemUIController.gameObject.SetActive(false);
        pausePanel.SetActive(true);      
    }

    public void ClosePauseMenu()
    {
        correctItemUIController.gameObject.SetActive(true);
        pausePanel.SetActive(false);
    }
    
    #endregion
}