using System.Collections;
using System.Collections.Generic;
using Game;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    #region Private fields

    [Inject] private GameSettings gameSettings;
    [Inject] private GameField gameField;
    private Clock clock;

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        clock = new Clock();
        clock.AddTime(gameSettings.TimingSettings.GameDuration);
        clock.OnSecondsChanged += OnSecondsChanged;
    }

    private void Update()
    {
        clock.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        clock.OnSecondsChanged -= OnSecondsChanged;
    }

    #endregion
    
    #region Private methods

    private void OnSecondsChanged()
    {
        /* todo    - implement
         * @author - Артур
         * @date   - 07.02.2018
         * @time   - 0:08
        */        
    }
    
    #endregion
}