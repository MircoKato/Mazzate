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
        static Random rand = new Random();
        public List<Guerriero>[] arrayGuerrieri {get; set;}

        public ManagerGuerrieri(int nGioc)
        {
            arrayGuerrieri = new List<Guerriero>[nGioc];
        }

        public void creaGuerrieri(Colore coloreGioc, int nGuerrieri)
        {
            //for (int i = 0; i < arrayGuerrieri.Length; i++)
            //{
                List<Guerriero> listaGuerrieri = new List<Guerriero>();
                for (int j = 0; j < nGuerrieri; j++)
                {
                    listaGuerrieri.Add(new Guerriero(100, 100));
                }
                arrayGuerrieri[(int)coloreGioc] = listaGuerrieri;
            //}
        }

        /// <summary>Crea le coordinate dove spawnerà il guerriero e le setta in .posizione</summary>
        public void posizionaGuerrieri(Colore coloreGioc, int guerPerGioc, Rectangle schermo)
        {
            int sezione = schermo.Width / guerPerGioc;

            for (int i = 0; i < guerPerGioc; i++)
            {
                int ySpawn = coloreGioc == Colore.rosso ? 0 : schermo.Height - 64;
                int gnu = i * sezione + 32;
                int gni = (i + 1) * sezione - 32;
                int rnd = rand.Next(gnu,gni);
                arrayGuerrieri[(int)coloreGioc].ElementAt(i).posizione = new Vector2(rnd, ySpawn);
            }
        }

        /// <summary>Se non sono nel primo ciclo, copio .nuovaPosizione in .posizione, rendendo vecchia quella del Update precedente</summary>
        public void svecchiaPosizione()
        {
            for (int i = 0; i < arrayGuerrieri.Length; i++)
            {
                foreach (Guerriero guer in arrayGuerrieri[i])
                {
                    if (guer.nuovaPosizione.X > -10 && guer.nuovaPosizione.Y > -10)
                    {
                        guer.posizione = guer.nuovaPosizione;
                        guer.nuovaPosizione = new Vector2(-1);
                    }
                }
            }
        }

        public void sistemaCollisioni(List<Guerriero>[] arrayGuer)
        {
            List<Guerriero> lisTempUno = (List<Guerriero>)(arrayGuer[0].Concat(arrayGuer[1]));
            List<Guerriero> lisTempDue = (List<Guerriero>)(arrayGuer[0].Concat(arrayGuer[1]));
            int idx = 0;
            bool collide;
            List<int> listaIndici = new List<int>();
            foreach (Guerriero guerDaCheck in lisTempUno)
            {
                collide = false;
                Rectangle rectUno = new Rectangle((int)guerDaCheck.nuovaPosizione.X - 32, (int)guerDaCheck.nuovaPosizione.Y - 32, 64, 64);
                foreach (Guerriero guerLista in lisTempDue)
                {
                    Rectangle rectDue = new Rectangle((int)guerLista.nuovaPosizione.X - 32, (int)guerLista.nuovaPosizione.Y - 32, 64, 64);
                    if (rectUno.Intersects(rectDue)) { collide = true; listaIndici.Add(idx); }
                }
                if (collide) lisTempDue.RemoveAt(idx);
                idx++;
            }
            //Console.WriteLine("guerdacheck " + nGuerDaCheck + " guerLista " + nGuerLista);
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
