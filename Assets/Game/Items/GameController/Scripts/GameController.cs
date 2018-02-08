using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Inject] private UIController uiController;
    
    private Clock clock;

    private Item correctItem;
    private float correctItemShowTimeStamp = 0;

    private int score;

    #endregion
    
    #region Properties
    

    public int Score
    {
        get { return score; }
        set
        {
            score = Mathf.Clamp(value, 0, gameSettings.PointsSettings.PointsTarget);
            uiController.UpdateScoreDisplay(score);
        }
    }

    public Item CorrectItem
    {
        get { return correctItem; }
    }

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        clock = new Clock();
        clock.AddTime(gameSettings.TimingSettings.GameDuration);
        clock.OnSecondsChanged += OnSecondsChanged;
        clock.Unpause();
        
        ChangeCorrectItem();
        uiController.UpdateTimerDisplay(clock.TimeInSeconds);
        uiController.UpdateScoreDisplay(score);
        uiController.UpdatePlayerNameDisplay();
    }

    private void Update()
    {
        clock.Update(Time.deltaTime);

        if (Time.time - correctItemShowTimeStamp >= gameSettings.TimingSettings.ItemDisplayTime)
        {
            ChangeCorrectItem();
            Score -= gameSettings.PointsSettings.TimeOutPenalty;
            correctItemShowTimeStamp = Time.time;
        }
    }

    private void OnDestroy()
    {
        clock.OnSecondsChanged -= OnSecondsChanged;
    }

    #endregion
    
    #region Private methods

    private void OnSecondsChanged()
    {
        uiController.UpdateTimerDisplay(clock.TimeInSeconds);
        
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

    private void ChangeCorrectItem()
    {
        var itemTypes = Utils.GetEnumValues<ItemType>();
        ItemType itemType = Utils.GetRandomItem(itemTypes);

        correctItem = new Item(gameSettings.GetColor(), itemType);
        uiController.UpdateDisplayedItem(correctItem.Type, correctItem.Color);
    }
    
    #endregion
}