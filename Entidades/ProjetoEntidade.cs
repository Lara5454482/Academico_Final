﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico_Final.Entidades
{
    public class ProjetoEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Turno { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }

        public object[] Linha()
        {
            return new object[] { Id, Nome, Turno, Sigla, Ativo };
        }
    }
}
