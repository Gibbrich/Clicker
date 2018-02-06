using System;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        #region Private fields

        [Inject] private GameSettings gameSettings;
        private Item[,] field;

        #endregion
        
        #region Unity callbacks

        private void Start()
        {
            field = new Item[gameSettings.FieldSettings.Width, gameSettings.FieldSettings.Height];
        }

        #endregion
        
        #region Public methods

//        public ValuePair<int, int> GetFreeField()
//        {
//            /* todo    - implement
//             * @author - Артур
//             * @date   - 07.02.2018
//             * @time   - 0:08
//            */
//        }
        
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