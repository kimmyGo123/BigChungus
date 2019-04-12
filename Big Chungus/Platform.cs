﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_Chungus
{
    class Platform : GameObject
    {
        //Fields
        private Texture2D platformTexture;
        private Rectangle platformBox;
        private bool isMoveable = true;

        #region Inventory
        bool inInventory;
        public bool InInventory
        {
            get { return inInventory; }
            set { inInventory = value; }
        }
        #endregion

        //Properties
        public int Width { get => platformBox.Width; set => platformBox.Width = value; }
        public int Height { get => platformBox.Height; set => platformBox.Height = value; }
        public int YPos { get => platformBox.Y; set => platformBox.Y = value; }
        public int XPos { get => platformBox.X; set => platformBox.X = value; }
        public bool IsMoveable { get => isMoveable; set => isMoveable = value; }
        public Rectangle Box { get => platformBox; set => platformBox = value; }
        public Texture2D Texture { get => platformTexture; set => platformTexture = value; }

        public Platform(Texture2D texture, int x, int y, int width, int height)
        {
            platformTexture = texture;
            platformBox = new Rectangle(x, y, width, height);
        }
        /*public Platform(Texture2D texture, int width, int height)
        {
            platformTexture = texture;
            platformBox = new Rectangle();
            platformBox.Width = width;
            platformBox.Height = height;
        }*/

        //set platform center to cursor position when dragging
        public void Drag()
        {
            MouseState mouseState = Mouse.GetState();
            XPos = mouseState.X - Width / 2;
            YPos = mouseState.Y - Height / 2;
        }

    }
}
