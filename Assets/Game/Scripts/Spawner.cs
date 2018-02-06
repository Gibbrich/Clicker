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

    #endregion
}