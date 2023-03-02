using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hive.Drops
{
    public class NectarDrop : ClickableEntity
    {
        protected DropManager dropManager;
        protected Counter nectarCounter;
        private int nectarValue;
        private float fallSpeed = 40;


        public NectarDrop(Counter nectarCounter, int nectarValue, Texture2D texture, Vector2 position, DropManager dropManager) : base(texture, position)
        {
            this.nectarCounter = nectarCounter;
            this.nectarValue = nectarValue;
            this.dropManager = dropManager;
            this.Click += OnClick;
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Claim);
        }

        public virtual async void Claim()
        {
            await nectarCounter.AddCount(nectarValue);
            dropManager.RemoveDrop(this);
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += fallSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if(position.Y > HiveGame.screenSizeY) 
            { 
                dropManager.RemoveDrop(this);
            }

            base.Update(gameTime);
        }

        

    }
}