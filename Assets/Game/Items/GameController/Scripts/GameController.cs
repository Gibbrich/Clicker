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
    private Clock clock;

    private Item correctItem;
    private float correctItemShowTimeStamp = 0;

    private int score;

    private Dictionary<Item.ItemType, Factory<Item.ItemType, Item>> factories;

    #endregion
    
    #region Properties
    
    public Item CorrectItem
    {
        get { return correctItem; }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = Mathf.Clamp(value, 0, gameSettings.PointsSettings.PointsTarget);
        }
    }

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        clock = new Clock();
        clock.AddTime(gameSettings.TimingSettings.GameDuration);
        clock.OnSecondsChanged += OnSecondsChanged;
        clock.Unpause();
        
        ChangeDisplayedItem();
    }

    private void Update()
    {
        clock.Update(Time.deltaTime);

        if (Time.time - correctItemShowTimeStamp >= gameSettings.TimingSettings.ItemDisplayTime)
        {
            ChangeDisplayedItem();
            correctItemShowTimeStamp = Time.time;
        }
    }

    private void OnDestroy()
    {
        clock.OnSecondsChanged -= OnSecondsChanged;
    }

    #endregion
    
    #region Private methods
    
    [Inject]
    private void Init([Inject(Id = Item.ItemType.SQUARE)] Item.SquareFactory squareFactory,
                      [Inject(Id = Item.ItemType.CIRCLE)] Item.CircleFactory circleFactory,
                      [Inject(Id = Item.ItemType.TRIANGLE)] Item.TriangleFactory triangleFactory)
    {
        factories = new Dictionary<Item.ItemType, Factory<Item.ItemType, Item>>
        {
            {Item.ItemType.SQUARE, squareFactory},
            {Item.ItemType.CIRCLE, circleFactory},
            {Item.ItemType.TRIANGLE, triangleFactory}
        };
    }

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

    private void ChangeDisplayedItem()
    {
        var pair = Utils.GetRandomItem(factories);
        
        correctItem = pair.Value.Create(pair.Key);
        correctItem.Color = gameSettings.GetColor();
        correctItem.PosX = -100;
        correctItem.PosY = -100;

        /* todo    - consider set correctItem in UI
         * @author - Dvurechenskiyi
         * @date   - 08.02.2018
         * @time   - 12:25
        */
        print(string.Format("Item type: {0}, color: {1}", correctItem.Type, correctItem.Color));
    }
    
    #endregion
}