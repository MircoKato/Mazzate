using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mazzate
{

    public enum danno
    {
        taglio,
        perforazione,
        impatto
    }

    public enum categoria
    {
        manoSingola,
        manoDoppia,
        lunga,
        daLancio,
        daTiro,
        scudo
    }

    public class Arma
    {
        public Arma(categoria tArma, danno tDanno, int vel, int fat)
        {
            tipoArma = tArma;
            tipoDanno = tDanno;
            velocita = vel;
            fatica = fat;

            switch (tArma)
            {
                case categoria.manoSingola: portata = 1; break;
                case categoria.manoDoppia: portata = 2; break;
                case categoria.lunga: portata = 3; break;
                case categoria.daLancio: portata = 4; break;
                case categoria.daTiro: portata = 5; break;
                case categoria.scudo: portata = 0.5f; break;
            }
            portata *= 64;
        }


        public int velocita { get; set; }
        public int fatica { get; set; }
        public danno tipoDanno { get; set; }
        public categoria tipoArma { get; set; }
        public float portata { get; set; }
    }

}
