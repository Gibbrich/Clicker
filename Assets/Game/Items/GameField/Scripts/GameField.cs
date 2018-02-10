using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        #region Editor tweakable fields

        [SerializeField] private float neighbourRadius = 3f;
        
        #endregion
        
        #region Private fields

        [Inject] private GameSettings gameSettings;
        private List<ValuePair<int, int>> freeFields;

        #endregion

        #region Unity callbacks

        private void Start()
        {
            int capacity = gameSettings.FieldSettings.Width * gameSettings.FieldSettings.Height;
            freeFields = new List<ValuePair<int, int>>(capacity);

            for (int i = 0; i < gameSettings.FieldSettings.Width; i++)
            {
                for (int j = 0; j < gameSettings.FieldSettings.Height; j++)
                {
                    freeFields.Add(new ValuePair<int, int>(i, j));
                }
            }
        }

        #endregion

        #region Public methods

        public ValuePair<int, int> GetRandomFreeField()
        {
            var freeField = Utils.GetRandomItem(freeFields);
            freeFields.Remove(freeField);
            return freeField;
            
        }

        public void AddFreeField(int x, int y)
        {
            freeFields.Add(new ValuePair<int, int>(x, y));
        }

        public bool HasFreeFields()
        {
            return freeFields.Count > 0;
        }

        /// <summary>
        /// Returns neighbours by grid offset 1 in all directions
        /// </summary>
        /// <param name="itemController"></param>
        /// <returns></returns>
        public List<ItemController> GetNeighbours(ItemController itemController)
        {
            Vector2 point = new Vector2(itemController.GetPosX(), itemController.GetPosY());
            return Physics2D
                .OverlapCircleAll(point, neighbourRadius)
                .Select(col => col.GetComponent<ItemController>())
                .Where(controller => controller != null)
                .ToList();
        }

        #endregion
    }

    [Serializable]
    public class GameFieldSettings
    {
        #region Editor tweakable fields

        [SerializeField] private int width = 32;
        [SerializeField] private int height = 32;

        #endregion

        #region Properties

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        #endregion
    }
}