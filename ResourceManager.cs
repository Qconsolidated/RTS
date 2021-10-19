using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public static class ResourceManager
    {
        private static GUISkin selectBoxSkin;
        private static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
        private static GameObjectList gameObjectList;
        private static float buttonHeight = 40;
        private static float headerHeight = 32, headerWidth = 256;
        private static float textHeight = 25, padding = 10;
       
        public static float PauseMenuHeight { get { return headerHeight + 2 * buttonHeight + 4 * padding; } }
        public static float MenuWidth { get { return headerWidth + 2 * padding; } }
        public static float ButtonHeight { get { return buttonHeight; } }
        public static float ButtonWidth { get { return (MenuWidth - 3 * padding) / 2; } }
        public static float HeaderHeight { get { return headerHeight; } }
        public static float HeaderWidth { get { return headerWidth; } }
        public static float TextHeight { get { return textHeight; } }
        public static float Padding { get { return padding; } }
        public static int BuildSpeed { get { return 2; } }

        public static bool MenuOpen { get; set; }

        public static GUISkin SelectBoxSkin { get { return selectBoxSkin; } }

        public static void StoreSelectBoxItems(GUISkin skin)
        {
            selectBoxSkin = skin;
        }

        public static Vector3 InvalidPosition { get { return invalidPosition; } }

        public static Bounds InvalidBounds { get { return invalidBounds; } }

        public static void SetGameObjectList(GameObjectList objList)
        {
            gameObjectList = objList;
        }

        public static GameObject GetBuilding(string name)
        {
            return gameObjectList.GetBuilding(name);
        }

        public static GameObject GetUnit(string name)
        {
            return gameObjectList.GetUnit(name);
        }

        public static GameObject GetWorldObject(string name)
        {
            return gameObjectList.GetWorldObject(name);
        }

        public static GameObject GetPlayerObject()
        {
            return gameObjectList.GetPlayerObject();
        }

        //public static Texture2D GetBuildImage(string name)
        //{
        //    return gameObjectList.GetBuildImage(name);
        //}
    }
}