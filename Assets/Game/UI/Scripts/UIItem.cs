using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    /* todo    - think on better decision how to change sprite
     * @author - Артур
     * @date   - 08.02.2018
     * @time   - 20:12
    */    
    private const string circleSpriteName = "Circle";
    private const string squareSpriteName = "Square";
    private const string triangleSpriteName = "Triangle";
    
    #region Private fields

    private UISprite sprite;

    #endregion
    
    #region Unity callbacks

    private void Awake()
    {
        sprite = GetComponent<UISprite>();
    }

    #endregion
    
    #region Public methods

    public void ChangeDisplay(ItemType type, Color color)
    {
        string spriteName;
        switch (type)
        {
            case ItemType.SQUARE:
                spriteName = squareSpriteName;
                break;
            case ItemType.CIRCLE:
                spriteName = circleSpriteName;
                break;
            case ItemType.TRIANGLE:
                spriteName = triangleSpriteName;
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }

        sprite.spriteName = spriteName;
        sprite.color = color;
    }
    
    #endregion
}