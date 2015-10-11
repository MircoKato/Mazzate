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

        public Giocatore (Colore id) {
            colore = id;
            //listaGuerrieri = listGuer;
        }
        
        public Colore colore { get; set; }
    }



}