using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VerivoxTask.Domain.Entities
{
    public class Product:EntitytBase<long>
    {

        public int Consumption { get; set; }
        public string TarrifName { get; set; }
        public decimal AnnualCost { get; set; }
    }
}
