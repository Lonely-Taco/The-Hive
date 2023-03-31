using Hive.Common;
using Hive.Drops.DropBehaviour;
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
        private float fallSpeed = 100;
        private IDropBehaviour dropBehaviour;
        private Color nectarEventTextColor;

        public NectarDrop(Counter nectarCounter, Texture2D texture, Vector2 position, DropManager dropManager, IDropBehaviour dropBehaviour) : base(texture, position, 1f)
        {
            this.nectarCounter = nectarCounter;
            this.dropManager = dropManager;
            this.Click += OnClick;
            this.dropBehaviour = dropBehaviour;
        }

        protected void OnClick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Claim);
        }

        public virtual async void Claim()
        {
            int addedNectar = await dropBehaviour.Claim(nectarCounter);
            dropManager.AddEventText(position, scale * 1.5f, $"+{addedNectar.ToString()}", Color.Orange, 0.5f);
            dropManager.RemoveDrop(this);
        }


        public override void Update(GameTime gameTime)
        {
            var currentPos = Position;

            currentPos.Y += fallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = currentPos;

            if (currentPos.Y > HiveGame.screenSizeY)
            {
                dropManager.RemoveDrop(this);
            }

            base.Update(gameTime);
        }
    }
}