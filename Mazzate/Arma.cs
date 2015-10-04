﻿using System;
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
                case categoria.manoSingola: lunghezza = 1; break;
                case categoria.manoDoppia: lunghezza = 2; break;
                case categoria.lunga: lunghezza = 3; break;
                case categoria.daLancio: lunghezza = 4; break;
                case categoria.daTiro: lunghezza = 5; break;
                case categoria.scudo: lunghezza = 0; break;
            }
        }


        public int velocita { get; set; }
        public int fatica { get; set; }
        public danno tipoDanno { get; set; }
        public categoria tipoArma { get; set; }
        public int lunghezza { get; set; }
    }

}
