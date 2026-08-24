using System;

namespace Calculadora;

public static class Program
{
    public static void Main()
    {
        bool continuarPrograma = true;
        double? resultadoAnterior = null;
        double primeiroNumero;
        while (continuarPrograma)
        {
            if (resultadoAnterior.HasValue)
            {
                Console.WriteLine("Você deseja usar o resultado anterior como primeiro número?");
                Console.WriteLine("Pressione ENTER para sim e ESC para não");
                ConsoleKeyInfo teclaUsarResultadoAnterior = Console.ReadKey(true);
                while ((teclaUsarResultadoAnterior.Key != ConsoleKey.Enter) && (teclaUsarResultadoAnterior.Key != ConsoleKey.Escape))
                {
                    MostrarErro("Você pressionou uma tecla inválida!");
                    Console.WriteLine("Pressione ENTER para usar o resultado anterior ou ESC para não usar.");
                    teclaUsarResultadoAnterior = Console.ReadKey(true);
                }
                if (teclaUsarResultadoAnterior.Key == ConsoleKey.Enter)
                {
                    primeiroNumero = resultadoAnterior.Value;
                    Console.Clear();
                    Titulo();
                    LinhaDivisoria();
                    Console.WriteLine($"Primeiro número: {primeiroNumero:0.##}");
                }
                else
                {
                    Console.Clear();
                    Titulo();
                    LinhaDivisoria();
                    Console.WriteLine("Digite o primeiro número: ");
                    primeiroNumero = LerNumero("primeiro número");
                }
            }
            else
            {
                Console.Clear();
                Titulo();
                LinhaDivisoria();
                Console.WriteLine("Digite o primeiro número: ");
                primeiroNumero = LerNumero("primeiro número");
            }

            Console.Write("Digite o segundo número: ");
            double segundoNumero = LerNumero("segundo número");

            Console.WriteLine();

            Console.WriteLine("Qual operação você precisa realizar?");
            Console.WriteLine("[1] Adição");
            Console.WriteLine("[2] Subtração");
            Console.WriteLine("[3] Multiplicação");
            Console.WriteLine("[4] Divisão");
            Console.Write("Escolha o valor correspondente a operação: ");

            int operacao = LerOperacao();
            MostrarOperacao(operacao);

            double? resultado = CalcularResultado(operacao, primeiroNumero, segundoNumero);
            if (resultado.HasValue)
            {
                MostrarResultado(operacao, primeiroNumero, segundoNumero, resultado.Value);
            }
            else
            {
                MostrarErro("Divisão por 0 não é permitida!");
            }

            resultadoAnterior = resultado;

            LinhaDivisoria();

            Console.WriteLine("Você deseja continuar ou encerrar programa?");
            Console.WriteLine("Pressione ENTER para continuar ou ESC para sair.");

            ConsoleKeyInfo teclaContinuarPrograma = Console.ReadKey(true);

            while ((teclaContinuarPrograma.Key != ConsoleKey.Enter) && (teclaContinuarPrograma.Key != ConsoleKey.Escape))
            {
                MostrarErro("Você pressionou uma tecla inválida!");
                Console.WriteLine("Pressione ENTER para continuar ou ESC para sair.");
                teclaContinuarPrograma = Console.ReadKey(true);
            }
            if (teclaContinuarPrograma.Key == ConsoleKey.Enter)
            {
                continuarPrograma = true;
            }
            else
            {continuarPrograma = false;}
        }
    }
    private static void LinhaDivisoria()
    {
        Console.WriteLine("\n-------------------------------------------------\n");
    }
    private static void Titulo()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("=====================");
        Console.WriteLine("| Calculadora em C# |");
        Console.WriteLine("=====================");
        Console.WriteLine("Versão 1.3");
        Console.ResetColor();
    }
    private static double LerNumero(string contexto)
    {
        double numeroValido;
        while (!double.TryParse(Console.ReadLine(), out numeroValido))
        {
            MostrarErro("Você digitou um valor inválido!");
            Console.Write($"Digite o {contexto} novamente: ");
        }
        return numeroValido;
    }
    private static int LerOperacao()
    {
        int operacaoValida;
        while (!int.TryParse(Console.ReadLine(), out operacaoValida) || (operacaoValida < 1) || (operacaoValida > 4))
            {
                MostrarErro("Você digitou um valor inválido!");
                Console.Write("Digite o valor da operação novamente: ");
            }
        return operacaoValida;
    }
    private static void MostrarOperacao (int operacao)
    {
        switch (operacao)
        {
            case 1:
                MostrarInfo("Operação escolhida: Adição");
                break;
            case 2:
                MostrarInfo("Operação escolhida: Subtração");
                break;
            case 3:
                MostrarInfo("Operação escolhida: Multiplicação");
                break;
            case 4:
                MostrarInfo("Operação escolhida: Divisão");
                break;
        }
    }
    private static double? CalcularResultado(int operacao, double primeiroNumero, double segundoNumero)
    {
        double? resultado;
        switch (operacao)
        {
            case 1:
                resultado = primeiroNumero + segundoNumero;
                break;
            case 2:
                resultado = primeiroNumero - segundoNumero;
                break;
            case 3:
                resultado = primeiroNumero * segundoNumero;
                break;
            case 4:
                if (segundoNumero != 0)
                {
                    resultado = primeiroNumero / segundoNumero;
                }
                else
                {
                    resultado = null;
                }
                break;
            default:
                resultado = null;
                break;
        }
        return resultado;
    }
    private static void MostrarResultado (int operacao, double primeiroNumero, double segundoNumero, double resultado)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        switch (operacao)
        {
            case 1:
                Console.WriteLine($"[RESULTADO] {primeiroNumero:0.##} + {segundoNumero:0.##} = {resultado:0.##}");
                break;
            case 2:
                Console.WriteLine($"[RESULTADO] {primeiroNumero:0.##} - {segundoNumero:0.##} = {resultado:0.##}");
                break;
            case 3:
                Console.WriteLine($"[RESULTADO] {primeiroNumero:0.##} * {segundoNumero:0.##} = {resultado:0.##}");
                break;
            case 4:
                Console.WriteLine($"[RESULTADO] {primeiroNumero:0.##} / {segundoNumero:0.##} = {resultado:0.##}");
                break;
        }
        Console.ResetColor();
    }
    private static void MostrarErro (string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\n[ERRO] {mensagem}");
        Console.ResetColor();
    }
    private static void MostrarInfo(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"\n[INFO] {mensagem}\n");
        Console.ResetColor();
    }
}