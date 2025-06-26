using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;

namespace ProjetoIMC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string caminho = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            "resultado_IMC.txt"
                            );
            bool rodando = true;
            while (rodando)
            {
                Console.Clear();
                Console.WriteLine("-----Menu IMC-----\n1- Calcular IMC\n2- Ver histórico\n3- Limpar histórico\n4- sair");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        CalcularIMC(caminho);
                        break;
                    case "2":
                        Verhistorico(caminho);
                        break;
                    case "3":
                        LimparHistorico(caminho);
                        break;
                    case "4":
                        rodando = false;
                        Console.Write("Saindo do calculador");
                        break;
                    default:
                        Console.Write("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }        
        }

        static void CalcularIMC(string caminho)
        {
            Console.Clear();
            Console.WriteLine("Índice de Massa Corporal (IMC)\n");
            Console.WriteLine("Um IMC dentro da faixa ideal para cada grupo etário\r\né importante para manter a saúde e prevenir doenças.\r\nNo entanto, é importante ressaltar que o IMC é apenas\r\num indicador e não deve ser usado isoladamente para\r\navaliar a saúde de uma pessoa. Outros fatores, como\r\na composição corporal (massa muscular e gordura),\r\nhistórico de saúde e nível de atividade física, também\r\ndevem ser considerados. ");
            Console.WriteLine("\nAdultos:\r\nAbaixo de 18,5: Baixo peso\r\n18,5 a 24,9: Peso normal\r\n25 a 29,9: Sobrepeso\r\n30 ou mais: Obesidade");
            Console.WriteLine("\nIdosos:\r\nAbaixo de 22: Baixo peso\r\n22 a 27: Peso normal/eutrófico\r\nAcima de 27: Sobrepeso ");

            Console.WriteLine("\nVamos calcular o seu IMC!");

            Console.Write("Qual é o seu nome: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Escolha a sua faixa - Aulto (A), Idosos (I)");
            Console.Write("Digite uma sigla (máximo uma letra): ");
            string faixaE = Console.ReadLine().Trim().ToUpper();

            if (faixaE.Length > 1)
            {
                Console.Write("Erro: você digitou mais do que 1 caracter!");
            }
            else
            {
                if (faixaE != "A" && faixaE != "I")
                {
                    Console.Write("\nOpção inválida! Digite 'A' para adultos ou 'I' para Idosos");
                }
                else
                {
                    Console.Write("Digite a sua Altura (em metros): ");
                    float altura = float.Parse(Console.ReadLine());

                    Console.Write("Digite o seu Peso em (KG): ");
                    float peso = float.Parse(Console.ReadLine());

                    float imc = peso / (altura * altura);
                    string classificacao = "";

                    switch (faixaE)
                    {
                        case "I":
                            if (imc >= 0 && imc <= 22)
                            {
                                classificacao = ("Abaixo do peso!!");
                            }
                            else if (imc >= 22 && imc <= 27)
                            {
                                classificacao = ("Peso normal (Eutrófico)!!");
                            }
                            else
                            {
                                classificacao = ("Sobrepeso!");
                            }
                            break;

                        case "A":
                            if (imc >= 0 && imc <= 18.5)
                            {
                                classificacao = ("Abaixo do peso!!");
                            }
                            else if (imc >= 18.5 && imc <= 24.9)
                            {
                                classificacao = ("Peso normal!!");
                            }
                            else if (imc >= 25 && imc <= 29.9)
                            {
                                classificacao = ("Sobrepeso!!");
                            }
                            else if (imc >= 30 && imc <= 34.9)
                            {
                                classificacao = ("Obesidade grau I!!");
                            }
                            else if (imc >= 35 && imc <= 39.9)
                            {
                                classificacao = ("Obesidade grau II!!");
                            }
                            else
                            {
                                classificacao = ("Obesidade grau III!!");
                            }
                            break;
                    }
                    Console.WriteLine($"Seu IMC é {imc:F2}. Classificação: {classificacao}");

                    string faixaDesc = (faixaE == "A") ? "Adulto" : "Idoso";
                    string dados = $"REGISTRO IMC {DateTime.Now: dd/MM/yyyy HH:mm:ss}\n" +
                                   $"Nome: {nome}\n" +
                                   $"Faixa: {faixaDesc}\n" +
                                   $"altura: {altura:F2}m\n" +
                                   $"peso: {peso}kg\n" +
                                   $"IMC: {imc:F2}\n" +
                                   $"Classificação: {classificacao}\n" +
                                   $"__________________________________\n\n";
                    File.AppendAllText(caminho, dados);
                    Console.Write("Dados salvos com sucesso. Pressione ENTER para sair");               
                    Console.ReadKey();
                }
            }
        }

        static void Verhistorico(string caminho)
        {
            Console.Clear();
            Console.WriteLine("\nHISTÓRICO\n");
            if (File.Exists(caminho))
            {
                Console.WriteLine(File.ReadAllText(caminho));
                Console.Write("Pressione ENTER para sair");
            }
            else
            {
                Console.Write("Nenhum histórico encontrado. Pressione ENTER para sair");
            }
            Console.ReadKey();
        }

        static void LimparHistorico(string caminho)
        {
            Console.Clear();
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
                Console.Write("Histótico apagado com sucesso. Pressione ENTER para sair");
            }
            else
            {
                Console.Write("Nenum histórico para apagar. Pressione ENTER para sair");
            }
            Console.ReadKey();
        }
    }
}
