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
            int nGuerDaCheck = 0;
            int nGuerLista = 0;
            foreach (Guerriero guerDaCheck in listGuer)
            {
                nGuerDaCheck++;
                Rectangle rectDaCheck = new Rectangle((int)guerDaCheck.nuovaPosizione.X - 32, (int)guerDaCheck.nuovaPosizione.Y - 32, 64, 64);
                guerDaCheck.checkCollisione = true;
                foreach (Guerriero guerLista in listGuer)
                {
                    if (!guerLista.checkCollisione)
                    {
                        nGuerLista++;
                        Rectangle rectGuerLista = new Rectangle((int)guerLista.nuovaPosizione.X - 32, (int)guerLista.nuovaPosizione.Y - 32, 64, 64);
                        if (rectDaCheck.Intersects(rectGuerLista)) { guerDaCheck.nuovaPosizione = guerDaCheck.posizione; guerLista.nuovaPosizione = guerLista.posizione; }
                    }
                }

            }
            Console.WriteLine("guerdacheck " + nGuerDaCheck + " guerLista " + nGuerLista);
            resettaCheckCollisione(listGuer);
        }

        public void resettaCheckCollisione(List<Guerriero> listGuer)
        {
            foreach (Guerriero guerriero in listGuer)
            {
                guerriero.checkCollisione = false;
            }
        }

        /// <summary>Non funziona :(</summary>
        public void impedisciUscitaSchermo(List<Guerriero> listGuer, Game1 game)
        {
            foreach (Guerriero guerDaCheck in listGuer)
            {
                if (guerDaCheck.nuovaPosizione.Y > game.Window.ClientBounds.Bottom - 32 || guerDaCheck.nuovaPosizione.Y < game.Window.ClientBounds.Top + 32)
                {
                    guerDaCheck.nuovaPosizione = guerDaCheck.nuovaPosizione - new Vector2(0, guerDaCheck.velMovimento);
                }
                if (guerDaCheck.nuovaPosizione.X > game.Window.ClientBounds.Right - 32 || guerDaCheck.nuovaPosizione.X < game.Window.ClientBounds.Left + 32)
                {
                    guerDaCheck.nuovaPosizione = guerDaCheck.posizione - new Vector2(guerDaCheck.velMovimento,0);
                }

            }
        }


    }
}
