using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mazzate
{
    public class Guerriero
    {
        public Guerriero(int mor, int hp)
        {
            posizione = new Vector2(-1);
            nuovaPosizione = new Vector2(-10);
            checkCollisione = false;
            orientamento = 0;

            velMovimento = 1;
            morale = mor;
            puntiVita = hp;
            abilitaTipoDanno = new int[3];
            abilitaTipoArma = new int[6];

            Array.Clear(abilitaTipoArma,0,abilitaTipoArma.Length);
            Array.Clear(abilitaTipoDanno, 0, abilitaTipoDanno.Length);
        }

        /// <summary>Crea le coordinate dove spawnerà il guerriero e le setta in .posizione</summary>
        public void coordSpawnGuerriero(Colore coloreGiocatore, Game1 game)
        {
            int rnd = -1;
            while (rnd < 64 || rnd > game.Window.ClientBounds.Right - 64)
            {
                rnd = rand.Next(0, (game.Window.ClientBounds.Right));
            }
            int xSpawn = rnd;
            int ySpawn;

            if (coloreGiocatore == Colore.rosso) ySpawn = 32;
            else ySpawn = game.Window.ClientBounds.Bottom - 32;

            this.posizione = new Vector2(xSpawn, ySpawn);
        }

        /// <summary>Restituisce il nemico più vicino</summary>
        public void nemicoPiuVicino(List<Guerriero> listGuerNem)
        {
            int distPiuVicino = 999999999;
            int dist = -1;
            int idx = -1;

            for (int i = 0; i < listGuerNem.Count; i++)
            {
                dist = (int) ( Math.Pow((this.posizione.X - listGuerNem[i].posizione.X),2) + Math.Pow((this.posizione.Y - listGuerNem[i].posizione.Y),2) );
                if (dist < distPiuVicino && dist > 0) {
                    distPiuVicino = dist;
                    idx = i;
                    //Console.WriteLine("distPiuVicino " + distPiuVicino);
                }
            }
            this.obiettivo = listGuerNem[idx];
        }

        /// <summary>Restituisce l'angolo (in radianti) tra guerriero e nemico</summary>
        public float guardaNemico(Guerriero guerNem)
        {
            double diffx = MathHelper.Distance(this.posizione.X, guerNem.posizione.X);
            double diffy = MathHelper.Distance(this.posizione.Y, guerNem.posizione.Y);

            return (float)Math.Atan(diffy / diffx);
        }

        public void muoviVersoNemico(Guerriero guerNem)
        {
            Vector2 direzione = guerNem.posizione - this.posizione;
            direzione.Normalize();

            this.nuovaPosizione = this.posizione + direzione * this.velMovimento;
        }


        public Vector2 posizione { get; set; }
        public Vector2 nuovaPosizione { get; set; }
        public bool checkCollisione { get; set; }
        public float orientamento { get; set; }

        public Guerriero obiettivo { get; set; }

        public int morale { get; set; }
        public int puntiVita { get; set; }
        public int velMovimento { get; set; }

        public int[] abilitaTipoDanno; // Taglio Perforazione Impatto
        public int[] abilitaTipoArma; // 1mano 2mani Lunghe Lancio Tiro Scudi

        static Random rand = new Random();
    }
}
