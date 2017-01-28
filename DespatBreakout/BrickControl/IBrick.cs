/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This interface is implemented by every brick.
 * 
 * Design patterns: Decorator
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    public interface IBrick
    {

         void Destroy(IBrick brick);
         void Hit(IBrick brick);
         void Subscribe(IBrickObserver observer);
         void Unsubscribe(IBrickObserver observer);
         void LoadContent();
         void Initialize(String textureName, int x, int y);
         void Update(GameTime gameTime);
         void Draw(GameTime gameTime);
         Rectangle GetDestinationRectangle();

    }
}
