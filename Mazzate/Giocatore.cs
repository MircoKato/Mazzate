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