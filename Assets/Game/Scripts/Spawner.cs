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

    private Pool<ItemController> circlePool;
    private Pool<ItemController> squarePool;
    private Pool<ItemController> trianglePool;

    private GameSettings gameSettings;
    private GameField field;

    #endregion
    
    #region Public methods
    
    public void PlaceSquare()
    {
        PlaceItemController(squarePool, gameSettings.SquareSettings.ItemMaxCount);
    }

    public void PlaceCircle()
    {
        PlaceItemController(circlePool, gameSettings.CircleSettings.ItemMaxCount);
    }
    
    public void PlaceTriangle()
    {
        PlaceItemController(trianglePool, gameSettings.TriangleSettings.ItemMaxCount);
    }

    public void ReleaseBulk(List<ItemController> items)
    {
        ItemType type = items[0].GetItemType();
        Pool<ItemController> pool;
        switch (type)
        {
            case ItemType.SQUARE:
                pool = squarePool;
                break;
            case ItemType.CIRCLE:
                pool = circlePool;
                break;
            case ItemType.TRIANGLE:
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
    private void Init([Inject(Id = ItemType.SQUARE)] ItemController.SquareFactory squareFactory,
                      [Inject(Id = ItemType.CIRCLE)] ItemController.CircleFactory circleFactory,
                      [Inject(Id = ItemType.TRIANGLE)] ItemController.TriangleFactory triangleFactory,
                      GameSettings gameSettings,
                      GameField field)
    {
        // first init pools, as during pool initiation calls pool.SetToSleep() callback.
        // This call back also add free field to Game firld and we don't want populate field excess fields
        squarePool = InitPool(squareFactory, ItemType.SQUARE);
        circlePool = InitPool(circleFactory, ItemType.CIRCLE);
        trianglePool = InitPool(triangleFactory, ItemType.TRIANGLE);

        this.gameSettings = gameSettings;
        this.field = field;
    }

    private Pool<ItemController> InitPool(Factory<ItemType, ItemController> factory, ItemType type)
    {
        return new Pool<ItemController>(100,
                              () => factory.Create(type),
                              item => Destroy(item.gameObject),
                              ItemWakeUp,
                              ItemSetToSleep);
    }

    private void ItemWakeUp(ItemController itemController)
    {
        var freeField = field.GetRandomFreeField();
        itemController.SetPosition(freeField.First, freeField.Second);
        itemController.SetColor(gameSettings.GetColor());
        itemController.gameObject.SetActive(true);
    }

    private void ItemSetToSleep(ItemController itemController)
    {
        itemController.gameObject.SetActive(false);
    }

    private void PlaceItemController(Pool<ItemController> pool, int maxCapacity)
    {
        if (!pool.IsObjectAvailable)
        {
            if (pool.Capacity <= maxCapacity)
            {
                pool.IncCapacity(100);
                pool.GetNewObject();
            }
        }
        else
        {
            pool.GetNewObject();
        }
    }

    #endregion
}