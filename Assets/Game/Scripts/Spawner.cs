using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Gamelogic.Extensions;
using ModestTree.Util;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    #region Private fields

    private Pool<Item> circlePool;
    private Pool<Item> squarePool;
    private Pool<Item> trianglePool;

    private GameSettings gameSettings;
    private GameField field;

    #endregion
    
    #region Public methods
    
    public void PlaceSquare()
    {
        squarePool.GetNewObject();
    }

    public void PlaceCircle()
    {
        circlePool.GetNewObject();
    }
    
    public void PlaceTriangle()
    {
        trianglePool.GetNewObject();
    }

    public void ReleaseBulk(List<Item> items)
    {
        Item.ItemType type = items[0].Type;
        Pool<Item> pool;
        switch (type)
        {
            case Item.ItemType.SQUARE:
                pool = squarePool;
                break;
            case Item.ItemType.CIRCLE:
                pool = circlePool;
                break;
            case Item.ItemType.TRIANGLE:
                pool = trianglePool;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        items.ForEach(item => pool.Release(item));
    }
    
    #endregion

    #region Private methods

    [Inject]
    private void Init([Inject(Id = Item.ItemType.SQUARE)] Item.SquareFactory squareFactory,
                      [Inject(Id = Item.ItemType.CIRCLE)] Item.CircleFactory circleFactory,
                      [Inject(Id = Item.ItemType.TRIANGLE)] Item.TriangleFactory triangleFactory,
                      GameSettings gameSettings,
                      GameField field)
    {
        // first init pools, as during pool initiation calls pool.SetToSleep() callback.
        // This call back also add free field to Game firld and we don't want populate field excess fields
        squarePool = InitPool(squareFactory, Item.ItemType.SQUARE);
        circlePool = InitPool(circleFactory, Item.ItemType.CIRCLE);
        trianglePool = InitPool(triangleFactory, Item.ItemType.TRIANGLE);

        this.gameSettings = gameSettings;
        this.field = field;
    }

    private Pool<Item> InitPool(Factory<Item.ItemType, Item> factory, Item.ItemType type)
    {
        return new Pool<Item>(100,
                              () => factory.Create(type),
                              item => Destroy(item.gameObject),
                              ItemWakeUp,
                              ItemSetToSleep);
    }

    private void ItemWakeUp(Item item)
    {
        var freeField = field.GetRandomFreeField();
        item.PosX = freeField.First;
        item.PosY = freeField.Second;
        item.Color = gameSettings.GetColor();
        item.gameObject.SetActive(true);
    }

    private void ItemSetToSleep(Item item)
    {
        if (field)
        {      
            field.AddFreeField(item.PosX, item.PosY);
        }
        item.gameObject.SetActive(false);
    }

    #endregion
}