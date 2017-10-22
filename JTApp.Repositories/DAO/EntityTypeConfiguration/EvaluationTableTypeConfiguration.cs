﻿using JTApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Repositories.DAO.EntityTypeConfiguration
{
    public class EvaluationTableTypeConfiguration:TypeConfiguration<EvaluationTable>
    {
        public EvaluationTableTypeConfiguration()
        {
            Ignore(p => p.Score);
        }
    }
}
