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
    
    #endregion

    #region Private methods

    [Inject]
    private void Init([Inject(Id = Item.ObjectType.SQUARE)] Item.SquareFactory squareFactory,
                      [Inject(Id = Item.ObjectType.CIRCLE)] Item.CircleFactory circleFactory,
                      [Inject(Id = Item.ObjectType.TRIANGLE)] Item.TriangleFactory triangleFactory,
                      GameSettings gameSettings,
                      GameField field)
    {
        // first init pools, as during pool initiation calls pool.SetToSleep() callback.
        // This call back also add free field to Game firld and we don't want populate field excess fields
        squarePool = InitPool(squareFactory);
        circlePool = InitPool(circleFactory);
        trianglePool = InitPool(triangleFactory);

        this.gameSettings = gameSettings;
        this.field = field;
    }

    private Pool<Item> InitPool(Factory<Item> factory)
    {
        return new Pool<Item>(100,
                              factory.Create,
                              item => Destroy(item.gameObject),
                              ItemWakeUp,
                              ItemSetToSleep);
    }

    private void ItemWakeUp(Item item)
    {
        var freeField = field.GetRandomFreeField();
        item.SetPosition(freeField.First, freeField.Second);
        item.SetColor(gameSettings.GetColor());
        item.gameObject.SetActive(true);
    }

    private void ItemSetToSleep(Item item)
    {
        if (field)
        {
            /* todo    - check correctness
             * @author - Dvurechenskiyi
             * @date   - 08.02.2018
             * @time   - 11:30
             */        
            field.AddFreeField((int) item.transform.position.x, (int) item.transform.position.y);
        }
        item.gameObject.SetActive(false);
    }

    #endregion
}