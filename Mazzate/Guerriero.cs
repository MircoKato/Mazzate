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
        public Guerriero(int mor, int hp, int pVel, int pEne, Arma ar)
        {
            posizione = new Vector2(-1);
            nuovaPosizione = new Vector2(-10);
            //orientamento = 0;
            collide = false;
            ingaggia = false;

            velMovimento = 3;
            morale = mor;
            puntiVita = hp;

            puntiVelocità = pVel;
            puntiEnergia = pEne;

            arma = ar;
            abilitaTipoDanno = new int[3];
            abilitaTipoArma = new int[6];

            Array.Clear(abilitaTipoArma,0,abilitaTipoArma.Length);
            Array.Clear(abilitaTipoDanno, 0, abilitaTipoDanno.Length);
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
            if (idx >= 0) obiettivo = listGuerNem[idx];
            else obiettivo = null;
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

            //float distanza = Vector2.Distance(this.posizione, guerNem.posizione);
            float distanza = dist(this, guerNem);

            //Console.WriteLine("Distanza " + distanza);

            int velo = velMovimento;

            if (distanza < this.arma.portata)
            {
                velo = 0;
            }
            else
            { 
                if (distanza < this.obiettivo.arma.portata)
                {
                    velo = 1;
                    if (distanza <= 65) velo = 0;
                }
            }
            this.nuovaPosizione = this.posizione + direzione * velo;
        }

        public void attacca (Guerriero gNem) //, List<Guerriero>[] arrayG)
        {
            if (dist(this, gNem) < this.arma.portata && puntiEnergia > this.arma.fatica && puntiVelocità > arma.velocita)
            {
                puntiEnergia -= this.arma.fatica; if (puntiEnergia < 0) puntiEnergia = 0;
                puntiVelocità = 0;
                gNem.puntiVita -= this.arma.danno; if (puntiVita < 0) puntiVita = 0;
                //if (gNem.puntiVita <= 0) gNem.muore(gNem, arrayG);
            }
        }

        public void muore (Guerriero gNem, List<Guerriero>[] arrayG)
        {
            Console.WriteLine("Guerriero " + this + " è morto.");
        }

        public float dist (Guerriero gUno, Guerriero gDue)
        {
            return Vector2.Distance(gUno.posizione, gDue.posizione);
        }

        public Vector2 posizione { get; set; }
        public Vector2 nuovaPosizione { get; set; }
        public bool collide { get; set; }
        public bool ingaggia { get; set; }
        //public float orientamento { get; set; }

        public Guerriero obiettivo { get; set; }

        public int puntiVelocità { get; set; }
        public int puntiEnergia { get; set; }

        public int morale { get; set; }
        public int puntiVita { get; set; }
        public int velMovimento { get; set; }
        public Arma arma { get; set; }

        public int[] abilitaTipoDanno; // Taglio Perforazione Impatto
        public int[] abilitaTipoArma; // 1mano 2mani Lunghe Lancio Tiro Scudi

        static Random rand = new Random();
    }
}
