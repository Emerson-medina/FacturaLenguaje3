﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 

namespace SynFacturaLenguaje3.Modelos.DAO
{
    public class Conexion
    {
        protected SqlConnection MiConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["FacturaConexion"].ConnectionString); 
    }
}
