using System;
using System.Collections.Generic;

namespace CraftRedM
{
    public class ItemCraft
    {
        public string Nome { get; set; }
        public Dictionary<Material, decimal> MateriaisNecessarios { get; set; }

        public ItemCraft(string nome)
        {
            Nome = nome;
            MateriaisNecessarios = new Dictionary<Material, decimal>();
        }

        public void AdicionarMaterial(Material material, decimal quantidade)
        {
            if (MateriaisNecessarios.ContainsKey(material))
                MateriaisNecessarios[material] += quantidade;
            else
                MateriaisNecessarios[material] = quantidade;
        }

        public decimal CalcularCustoTotal()
        {
            decimal custoTotal = 0;

            foreach (var item in MateriaisNecessarios)
            {
                custoTotal += item.Key.CustoPorUnidade * item.Value;
            }

            return custoTotal;
        }
    }
}
