﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfServicio.Entidad
{
    public class mSucursal
    {
        public int idBanco { get; set; }
        public int idSucursal { get; set; }
        public string NombreBanco { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha_registro { get; set; }
    }
}