﻿using SIMS_2021.Model;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    public class DrugsController
    {
        private DrugService _drugService = new DrugService();

        public List<Drug> GetAll()
        {
            return _drugService.GetAll();
        }
        public bool AddDrug(Drug drug)
        {
            return _drugService.Add(drug);
        }
    }
}