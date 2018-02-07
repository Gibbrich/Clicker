using System.Collections;
using System.Collections.Generic;
using Game;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    #region Private fields

    private Pool<Item> circlePool;
    private Pool<Item> squarePool;
    private Pool<Item> trianglePool;

    private GameSettings gameSettings;

    #endregion
    
    #region Public methods

    
    public Item PlaceSquare(int width, int height)
    {
        return PlaceObject(width, height, squarePool);
    }

    public Item PlaceCircle(int width, int height)
    {
        return PlaceObject(width, height, circlePool);
    }
    
    public Item PlaceTriangle(int width, int height)
    {
        return PlaceObject(width, height, trianglePool);
    }
    
    #endregion

    #region Private methods

    [Inject]
    private void Init([Inject(Id = Item.ObjectType.SQUARE)] Item.SquareFactory squareFactory,
                      [Inject(Id = Item.ObjectType.CIRCLE)] Item.CircleFactory circleFactory,
                      [Inject(Id = Item.ObjectType.TRIANGLE)] Item.TriangleFactory triangleFactory,
                      GameSettings gameSettings)
    {
        squarePool = InitPool(squareFactory);
        circlePool = InitPool(circleFactory);
        trianglePool = InitPool(triangleFactory);

        this.gameSettings = gameSettings;
    }

    private Pool<Item> InitPool(Factory<Item> factory)
    {
        return new Pool<Item>(100,
                              factory.Create,
                              item => Destroy(item.gameObject),
                              item => { item.gameObject.SetActive(true); },
                              item => { item.gameObject.SetActive(false); });
    }

    /// <summary>
    /// Places game item on game field.
    /// </summary>
    /// <param name="width">Position X</param>
    /// <param name="height">Position Y</param>
    /// <param name="pool">Pool, which spawns items</param>
    private Item PlaceObject(int width, int height, Pool<Item> pool)
    {
        Item item = pool.GetNewObject();
        item.Width = width;
        item.Height = height;
        return item;
    }

    #endregion
}