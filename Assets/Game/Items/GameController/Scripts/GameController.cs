using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Gamelogic.Extensions;
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
        var freeFields = gameField.GetFreeFields();

        for (int i = 0; i < gameSettings.SquareSettings.AppearRateMaxCount; i++)
        {
            SpawnItem(freeFields, spawner.PlaceSquare);
        }

        for (int i = 0; i < gameSettings.CircleSettings.AppearRateMaxCount; i++)
        {
            SpawnItem(freeFields, spawner.PlaceCircle);
        }

        for (int i = 0; i < gameSettings.TriangleSettings.AppearRateMaxCount; i++)
        {
            SpawnItem(freeFields, spawner.PlaceTriangle);
        }
        
        /* todo    - implement
         * @author - Dvurechenskiyi
         * @date   - 07.02.2018
         * @time   - 18:03
        */        
    }

    private void SpawnItem(List<Tuple<int, int>> freeFields, Func<int, int, Item> createFunc)
    {
        int id = Random.Range(0, freeFields.Count);
        var fieldCoordinates = freeFields[id];

        Item item = createFunc(fieldCoordinates.Item1, fieldCoordinates.Item2);
        gameField.SetItem(fieldCoordinates.Item1, fieldCoordinates.Item2, item);
        
        freeFields.RemoveAt(id);
    }
    
    #endregion
}