using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Space_Shooter.Sciprts.Heritage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter.Sciprts.Gui
{
    internal class Health : GuiObject
    {
        public Health(Vector2 startPostion)
        {
            postion = startPostion;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
