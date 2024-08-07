﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Working_title.MapGenerator;
using Working_title.UI;

namespace Working_title.Utillity
{
    public delegate void LoadingBarDoneCallback();

    public class LoadingBar : Bar
    {
        private const float LoadingSpeed = 0.1f;

        private int NumberOfLoadingPoints;
        private int CurrentLoadingPoint;
        private float WantedLoadingBarProcent;
        private LoadingBarDoneCallback LoadingBarDoneCallback;

        private Size FullSize;

        public LoadingBar(Vector2 position,Size fullSize,int numberOfLoadingPoints, LoadingBarDoneCallback loadingBarDoneCallback) :
            base(position, fullSize)
        {
            FullSize = fullSize;
            NumberOfLoadingPoints = numberOfLoadingPoints;
            LoadingBarDoneCallback = loadingBarDoneCallback;
            TextureSize = new Size(1, FullSize.Height);
            TextureName = "Green";
            LayerDepth = 0.9f;
        }

        public void ReachedLoadingPoint()
        {
            CurrentLoadingPoint++;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SetWantedLoadingBarProcent(CurrentLoadingPoint / (float)NumberOfLoadingPoints);
            if (CurrentLoadingBarProcent() < WantedLoadingBarProcent)
            {
                TextureSize += new Size((int)(LoadingSpeed * DeltaTime),0);
            }
            ShouldCallback();
        }

        private void ShouldCallback()
        {
            if (CurrentLoadingBarProcent() >= 1)
            {
                LoadingBarDoneCallback();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Game1.SpriteFonts["StandardFont"], (int)(CurrentLoadingBarProcent() * 100) + "%",Position, Color.White,
                0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
}