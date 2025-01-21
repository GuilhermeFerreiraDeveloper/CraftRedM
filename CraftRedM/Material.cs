namespace CraftRedM
{
    public class Material
    {
        public string Nome { get; set; }
        public decimal CustoPorUnidade { get; set; }

        public Material(string nome, decimal custoPorUnidade)
        {
            Nome = nome;
            CustoPorUnidade = custoPorUnidade;
        }
    }
}
