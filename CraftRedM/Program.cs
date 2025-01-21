using System;

namespace CraftRedM
{
    class Program
    {
        static void Main(string[] args)
        {
            Material canaDeAcucar = new Material("Cana de Açúcar", 0.02m);
            Material pano = new Material("Pano", 0.6m);
            Material adubo = new Material("Adubo", 0.30m);
            Material garrafaDeVidro = new Material("Garrafa de Vidro", 0.10m);

            // Produto Açúcar
            ItemCraft acucar = new ItemCraft("Açúcar");
            acucar.AdicionarMaterial(canaDeAcucar, 1); // 1 Cana de Açúcar por unidade de Açúcar
            acucar.AdicionarMaterial(pano, 0.1m); // 0.1 Pano por unidade de Açúcar --- 1 pano = 10 pano
            acucar.AdicionarMaterial(adubo, 0.1m); // 0.1 Adubo por unidade de Açúcar --- 1 adubo = 10 açucar

            // Produto Álcool
            ItemCraft alcool = new ItemCraft("Álcool");
            alcool.AdicionarMaterial(canaDeAcucar, 6); // 6 Cana de Açúcar por 10 unidades de Álcool
            alcool.AdicionarMaterial(garrafaDeVidro, 10); // 10 Garrafas de Vidro por 10 unidades de Álcool

            decimal precoDeVendaAçucar = 0.30m;  //preço de Venda do Açucar
            decimal precoDeVendaAlcool = 0.50m;  //preço de Venda do Álcool

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Exibir Custo de Produção");
                Console.WriteLine("2. Alterar custo de materiais");
                Console.WriteLine("3. Fazer encomenda de Açúcar");
                Console.WriteLine("4. Fazer encomenda de Álcool");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ExibirCustoDeProducao(acucar, alcool);
                        break;

                    case "2":
                        AlterarCustoDeMateriais(canaDeAcucar, pano, adubo, garrafaDeVidro);
                        break;

                    case "3":
                        FazerEncomenda(acucar, precoDeVendaAçucar);
                        break;

                    case "4":
                        FazerEncomendaAlcool(alcool, precoDeVendaAlcool);
                        break;

                    case "5":
                        continuar = false;
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                if (continuar)
                {
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                }
            }
        }

        static void ExibirCustoDeProducao(ItemCraft acucar, ItemCraft alcool)
        {
            Console.Clear();
            Console.WriteLine("Gastos para criar 1 unidade de Açúcar:");

            // Exibir custos do açúcar
            foreach (var material in acucar.MateriaisNecessarios)
            {
                Console.WriteLine($"- {material.Value} de {material.Key.Nome}: R$ {material.Key.CustoPorUnidade * material.Value:F2}");
            }

            Console.WriteLine($"Custo total para criar 1 Açúcar: R$ {acucar.CalcularCustoTotal():F2}");
            Console.WriteLine("\nGastos para criar 1 unidade de Álcool:");

            // Exibir custos do álcool
            foreach (var material in alcool.MateriaisNecessarios)
            {
                Console.WriteLine($"- {material.Value} de {material.Key.Nome}: R$ {material.Key.CustoPorUnidade * material.Value:F2}");
            }
            Console.WriteLine($"Custo total para criar 1 Álcool: R$ {alcool.CalcularCustoTotal():F2}");
        }

        static void AlterarCustoDeMateriais(Material canaDeAcucar, Material pano, Material adubo, Material garrafaDeVidro)
        {
            Console.Clear();
            Console.WriteLine("Escolha o material que deseja alterar o custo:");
            Console.WriteLine($"1. {canaDeAcucar.Nome} - Preço atual: R$ {canaDeAcucar.CustoPorUnidade:F2}");
            Console.WriteLine($"2. {pano.Nome} - Preço atual: R$ {pano.CustoPorUnidade:F2}");
            Console.WriteLine($"3. {adubo.Nome} - Preço atual: R$ {adubo.CustoPorUnidade:F2}");
            Console.WriteLine($"4. {garrafaDeVidro.Nome} - Preço atual: R$ {garrafaDeVidro.CustoPorUnidade:F2}");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AlterarCustoMaterial(canaDeAcucar);
                    break;

                case "2":
                    AlterarCustoMaterial(pano);
                    break;

                case "3":
                    AlterarCustoMaterial(adubo);
                    break;

                case "4":
                    AlterarCustoMaterial(garrafaDeVidro);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void AlterarCustoMaterial(Material material)
        {
            Console.Clear();
            Console.Write($"Digite o novo custo para {material.Nome}: R$ ");
            if (decimal.TryParse(Console.ReadLine(), out decimal novoCusto))
            {
                material.CustoPorUnidade = novoCusto;
                Console.WriteLine($"Custo de {material.Nome} alterado para: R$ {material.CustoPorUnidade:F2}");
            }
            else
            {
                Console.WriteLine("Valor inválido. Custo não alterado.");
            }
        }

        static void FazerEncomenda(ItemCraft acucar, decimal precoDeVenda)
        {
            Console.Clear();
            Console.Write("Quantos Açúcares deseja encomendar? ");
            if (int.TryParse(Console.ReadLine(), out int quantidadeEncomendada))
            {
                decimal custoTotal = acucar.CalcularCustoTotal() * quantidadeEncomendada;
                decimal valorVendaTotal = precoDeVenda * quantidadeEncomendada;  // Calculo do preço de venda

                Console.WriteLine(" \nMateriais necessários para produzir a encomenda: \n");
                foreach (var material in acucar.MateriaisNecessarios)
                {
                    Console.WriteLine($"{material.Value * quantidadeEncomendada} de {material.Key.Nome}");
                }
                decimal lucro = valorVendaTotal - custoTotal;    // Calcular o Lucro da encomenda 
                Console.WriteLine(" \n----------------------------------------------------------");
                Console.WriteLine($"\nCusto Total da Encomenda: R$ {custoTotal:F2} \nValor Total da Venda: R$ {valorVendaTotal:F2} \nLucro obtido de: R${lucro:F2} \n ");
            }
            else
            {
                Console.WriteLine("Valor inválido. Encomenda não realizada.");
            }
        }

        static void FazerEncomendaAlcool(ItemCraft alcool, decimal precoDeVenda)
        {
            Console.Clear();
            Console.Write("Quantos Álcool deseja encomendar? ");
            if (int.TryParse(Console.ReadLine(), out int quantidadeEncomendada))
            {
                decimal custoTotal = alcool.CalcularCustoTotal() * (quantidadeEncomendada / 10); // Produz 10 unidades por vez
                decimal valorVendaTotal = precoDeVenda * quantidadeEncomendada;  // Calculo do preço de venda


                Console.WriteLine(" \nMateriais necessários para produzir a encomenda:");
                foreach (var material in alcool.MateriaisNecessarios)
                {
                    Console.WriteLine($"{material.Value * quantidadeEncomendada / 10} de {material.Key.Nome}");
                }
                decimal lucro = valorVendaTotal - custoTotal;   // Calcular o Lucro da encomenda 
                Console.WriteLine(" \n----------------------------------------------------------");
                Console.WriteLine($"\nCusto Total da Encomenda: R$ {custoTotal:F2} \nValor Total da Venda: R$ {valorVendaTotal:F2} \nLucro obtido de: R${lucro:F2} \n");
            }
            else
            {
                Console.WriteLine("Valor inválido. Encomenda não realizada.");
            }
        }
    }
}
