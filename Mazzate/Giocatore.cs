using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mazzate
{

    public enum Colore
    {
        rosso,
        blu,
        giallo,
        verde
    }

    public class Giocatore
    {

        public Giocatore (Colore id, List<Guerriero> listGuer) {
            colore = id;
            listaGuerrieri = listGuer;
        }
        
        public void creaGuerrieri(int nGuerrieri)
        {
            for (int j = 0; j < nGuerrieri; j++)
            {
                listaGuerrieri.Add(new Guerriero(100*j, 100*j));
            }
            return;
        }


        public Colore colore { get; set; }
        public List<Guerriero> listaGuerrieri { get; set; }
    }



}
/*
If I understand your problem properly, you should just have a direction Vector2 representing the direction you want to move in inside your sprite class. Like this:

public Vector2 Direction { get; set; }
This is the normalized vector(which means it has a length of 1) showing where you want to go.

Then, add a Speed float property, which says how fast the sprite should go.

public float Speed { get; set; }
You also need to add a UpdateSprite function, so why not put it inside your Sprite class?

public Update(GameTime gameTime)
{
   Position += Direction * Speed * gameTime.ElapsedGameTime.TotalSeconds;
}
This will update the sprite's position to make it move(you multiply by the delta seconds so that the sprite moves properly on slow computers too).

Finally, you just set your direction property like this:

sprite.Direction = location - sprite.Position;
sprite.Direction.Normalize();


*/
