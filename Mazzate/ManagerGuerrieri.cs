using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mazzate
{
    public class ManagerGuerrieri
    {
        public ManagerGuerrieri()
        {


        }

        public void sistemaCollisioni(List<Guerriero> listGuer)
        {
            foreach (Guerriero guerDaCheck in listGuer)
            {
                Rectangle rectDaCheck = new Rectangle((int)guerDaCheck.nuovaPosizione.X - 32, (int)guerDaCheck.nuovaPosizione.Y - 32, 64, 64);
                guerDaCheck.checkCollisione = true;
                foreach (Guerriero guerLista in listGuer)
                {
                    if (!guerLista.checkCollisione)
                    {
                        Rectangle rectGuerLista = new Rectangle((int)guerLista.nuovaPosizione.X - 32, (int)guerLista.nuovaPosizione.Y - 32, 64, 64);
                        if (rectDaCheck.Intersects(rectGuerLista)) { guerDaCheck.nuovaPosizione = guerDaCheck.posizione; guerLista.nuovaPosizione = guerLista.posizione; }
                    }
                }

            }
            resettaCheckCollisione(listGuer);
        }

        public void resettaCheckCollisione(List<Guerriero> listGuer)
        {
            foreach (Guerriero guerriero in listGuer)
            {
                guerriero.checkCollisione = false;
            }
        }
        public void impedisciUscitaSchermo(List<Guerriero> listGuer, Game1 game)
        {
            foreach (Guerriero guerDaCheck in listGuer)
            {
                if (guerDaCheck.nuovaPosizione.Y > game.Window.ClientBounds.Bottom - 64 || guerDaCheck.nuovaPosizione.Y < game.Window.ClientBounds.Top)
                {
                    Console.WriteLine("Uscito asse Y!");
                    guerDaCheck.nuovaPosizione = guerDaCheck.posizione - new Vector2(0,guerDaCheck.velMovimento);
                }
                if (guerDaCheck.nuovaPosizione.X > game.Window.ClientBounds.Right - 64 || guerDaCheck.nuovaPosizione.X < game.Window.ClientBounds.Left - 64)
                {
                    Console.WriteLine("Uscito asse X!");
                    guerDaCheck.nuovaPosizione = guerDaCheck.posizione - new Vector2(guerDaCheck.velMovimento,0);
                }

            }
        }


    }
}
