using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Gamelogic.Extensions;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    #region Private fields

    [Inject] private GameSettings gameSettings;
    [Inject] private GameField gameField;
    [Inject] private Spawner spawner;
    private Clock clock;

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        clock = new Clock();
        clock.AddTime(gameSettings.TimingSettings.GameDuration);
        clock.OnSecondsChanged += OnSecondsChanged;
        clock.Unpause();
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
        for (int i = 0; i < gameSettings.SquareSettings.AppearRateMaxCount; i++)
        {
            spawner.PlaceSquare();
        }

        for (int i = 0; i < gameSettings.CircleSettings.AppearRateMaxCount; i++)
        {
            spawner.PlaceCircle();
        }

        for (int i = 0; i < gameSettings.TriangleSettings.AppearRateMaxCount; i++)
        {
            spawner.PlaceTriangle();
        }
        
        /* todo    - implement
         * @author - Dvurechenskiyi
         * @date   - 07.02.2018
         * @time   - 18:03
        */        
    }
    
    #endregion
}