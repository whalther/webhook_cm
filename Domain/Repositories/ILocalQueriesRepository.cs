﻿using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ILocalQueriesRepository
    {
        List<TipoDocumento> GetTiposDocumento();
    }
}
