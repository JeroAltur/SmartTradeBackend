﻿using SmartTradeBackend.Services;
using Dapper;

namespace SmartTradeBackend.Models
{
    public class Electronica
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Electronica() { }
        public Electronica(int idprod)
        {
            this.id_Prod = idprod;
        }
    }
}
