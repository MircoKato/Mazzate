using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mazzate
{
    public class ManagerGuerrieri : GameComponent
    {
        static Random rand = new Random();
        public List<Guerriero>[] arrayGuerrieri {get; set;}

        public ManagerGuerrieri(int nGioc, Game game) : base (game)
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
                int tipoGuerriero = rand.Next(3);
                switch (tipoGuerriero) {
                    case 0: listaGuerrieri.Add(new Guerriero(100, 100, 100, 100, new Arma(categoria.manoSingola, tipoDanno.impatto, 10, 10, 10))); break;
                    case 1: listaGuerrieri.Add(new Guerriero(100, 100, 100, 100, new Arma(categoria.manoDoppia, tipoDanno.taglio, 30, 30, 30))); break;
                    case 2: listaGuerrieri.Add(new Guerriero(100, 100, 100, 100, new Arma(categoria.lunga, tipoDanno.perforazione, 20, 20, 20))); break;
                }
                    
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
                //ySpawn = coloreGioc == Colore.rosso ? 576/2 : 576 / 2 + 100;
                //int gnu = i * sezione + 32;
                //int gni = (i + 1) * sezione - 32;
                //int rnd = rand.Next(gnu,gni);
                arrayGuerrieri[(int)coloreGioc].ElementAt(i).posizione = new Vector2(rand.Next(i * sezione + 32, (i +1) * sezione - 32), ySpawn);
                //arrayGuerrieri[(int)coloreGioc].ElementAt(i).posizione = new Vector2(300, ySpawn);
            }
        }

        public override void Update(GameTime gameTime)
        {
            /*
            foreach (Guerriero guerriero in arrayGuerrieri[0])
            {
                guerriero.nemicoPiuVicino(arrayGuerrieri[1]);
                guerriero.muoviVersoNemico(guerriero.obiettivo);
            }
            foreach (Guerriero guerriero in arrayGuerrieri[1])
            {
                guerriero.nemicoPiuVicino(arrayGuerrieri[0]);
                guerriero.muoviVersoNemico(guerriero.obiettivo);
            }

            controllaIngaggio(arrayGuerrieri);*/

            foreach (Guerriero guerriero in arrayGuerrieri[0])
            {
                //if (guerriero.ingaggia) controllaCollisioni(guerriero, arrayGuerrieri);
                guerriero.nemicoPiuVicino(arrayGuerrieri[1]);
                if (guerriero.obiettivo != null)
                {
                    guerriero.muoviVersoNemico(guerriero.obiettivo);
                    guerriero.attacca(guerriero.obiettivo);
                }
            }
            foreach (Guerriero guerriero in arrayGuerrieri[1])
            {
                //if (guerriero.ingaggia) controllaCollisioni(guerriero, arrayGuerrieri);
                guerriero.nemicoPiuVicino(arrayGuerrieri[0]);
                if (guerriero.obiettivo != null)
                {
                    guerriero.muoviVersoNemico(guerriero.obiettivo);
                    guerriero.attacca(guerriero.obiettivo);
                }
            }

            for (int i = 0; i < arrayGuerrieri.Length; i++)
            {
                arrayGuerrieri[i].RemoveAll(g => g.puntiVita <= 0);
                ricaricaStat(i);
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

        /*
        public void controllaIngaggio(List<Guerriero>[] arrayGuer)
        {
            List<Guerriero> lisTemp = new List<Guerriero>();
            lisTemp.AddRange(arrayGuer[0]);
            lisTemp.AddRange(arrayGuer[1]);

            foreach (Guerriero gUno in lisTemp)
            {
                Rectangle rectUno = new Rectangle((int)gUno.nuovaPosizione.X - 32, (int)gUno.nuovaPosizione.Y - 32, 64, 64);
                if (gUno.ingaggia) continue;
                foreach (Guerriero gDue in lisTemp)
                {
                    if (gUno.Equals(gDue)) continue;
                    Rectangle rectDue = new Rectangle((int)gDue.nuovaPosizione.X - 32, (int)gDue.nuovaPosizione.Y - 32, 64, 64);
                    if (rectUno.Intersects(rectDue))
                    {
                        gUno.ingaggia = true;
                        gUno.velMovimento = 1;
                        gUno.nuovaPosizione = gUno.posizione;
                        break;
                    }
                }
            }
        }

        public void controllaCollisioni(Guerriero gUno, List<Guerriero>[] arrayGuer)
        {
            List<Guerriero> lisTemp = new List<Guerriero>();
            lisTemp.AddRange(arrayGuer[0]);
            lisTemp.AddRange(arrayGuer[1]);

            Rectangle rectUno = new Rectangle((int)gUno.nuovaPosizione.X - 32, (int)gUno.nuovaPosizione.Y - 32, 64, 64);
            foreach (Guerriero gDue in lisTemp)
            {
                if (gUno.Equals(gDue)) continue;
                Rectangle rectDue = new Rectangle((int)gDue.nuovaPosizione.X - 32, (int)gDue.nuovaPosizione.Y - 32, 64, 64);
                if (rectUno.Intersects(rectDue))
                {
                    gUno.collide = true;
                    gUno.velMovimento = 0;
                    gUno.nuovaPosizione = gUno.posizione;
                    break;
                }
            }
        }

        public void resettaCollide(List<Guerriero>[] arrayGuer)
        {
            for (int i = 0; i < arrayGuer.Length; i++)
            {
                foreach (Guerriero guer in arrayGuer[i]) { guer.collide = false; }
            }
        }

        public void resettaIngaggia(Guerriero guer)
        {
            guer.ingaggia = false;
            guer.velMovimento = 10;
        }
        */

        public void ricaricaStat(int gioc)
        {
            foreach (Guerriero g in arrayGuerrieri[gioc])
            {
                if (g.puntiVelocità < 100) g.puntiVelocità += 1;
                if (g.puntiEnergia < 100) g.puntiEnergia += 1;
            }
        }

        /// <summary>Non funziona bene il margine destro :(</summary>
        public void impedisciUscitaSchermo(List<Guerriero>[] listGuer, Rectangle schermo)
        {
            for (int i=0; i < listGuer.Count(); i++) {
                foreach (Guerriero guerDaCheck in listGuer[i])
                {
                    if (guerDaCheck.nuovaPosizione.Y > schermo.Bottom - 64)
                    {
                        guerDaCheck.nuovaPosizione -= new Vector2(0, guerDaCheck.nuovaPosizione.Y - guerDaCheck.posizione.Y);
                    }
                    if (guerDaCheck.nuovaPosizione.Y < schermo.Top)
                    {
                        guerDaCheck.nuovaPosizione -= new Vector2(0, guerDaCheck.nuovaPosizione.Y - guerDaCheck.posizione.Y);
                    }
                    if (guerDaCheck.nuovaPosizione.X > schermo.Right -64)
                    {
                        guerDaCheck.nuovaPosizione -= new Vector2(guerDaCheck.nuovaPosizione.X - guerDaCheck.posizione.X, 0);
                    }
                    if (guerDaCheck.nuovaPosizione.X < schermo.Left)
                    {
                        guerDaCheck.nuovaPosizione -= new Vector2(guerDaCheck.nuovaPosizione.X - guerDaCheck.posizione.X, 0);
                    }
                }
            }
        }


    }
}
