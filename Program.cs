
class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Informe o seu nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe a sua idade: ");
            int idade = ReadInt();

            int riskPercentage = 0;

            riskPercentage += AskQuestion("Seu cartão de vacina está em dia? (SIM/NAO) ") == "NAO" ? 10 : 0;
            riskPercentage += AskQuestion("Teve algum dos sintomas recentemente? (dor de cabeça, febre, náusea, dor articular, gripe) (SIM/NAO) ") == "SIM" ? 30 : 0;
            riskPercentage += AskQuestion("Teve contato com pessoas com sintomas gripais nos últimos dias? (SIM/NAO) ") == "SIM" ? 30 : 0;
            bool isReturningFromTrip = AskQuestion("Está retornando de viagem realizada no exterior? (SIM/NAO) ") == "SIM";
            riskPercentage += isReturningFromTrip ? 30 : 0;

            string[] orientations = {
                    "Paciente sob observação. Caso apareça algum sintoma, gentileza buscar assistência médica.",
                    "Paciente com risco de estar infectado. Gentileza aguardar em lockdown por 02 dias para ser acompanhado.",
                    "Paciente com alto risco de estar infectado. Gentileza aguardar em lockdown por 05 dias para ser acompanhado.",
                    "Paciente crítico! Gentileza aguardar em lockdown por 10 dias para ser acompanhado."
                };

            int orientationIndex = riskPercentage switch
            {
                <= 30 => 0,
                <= 60 => 1,
                <= 89 => 2,
                _ => 3
            };

            Console.WriteLine($"\nNome: {nome}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Probabilidade de infecção: {riskPercentage}%");
            Console.WriteLine($"Orientação: {orientations[orientationIndex]}");

            if (isReturningFromTrip)
            {
                Console.WriteLine("Você ficará sob observação por 05 dias.");
            }
        }
        catch (InvalidResponseException)
        {
            Console.WriteLine("Não foi possível realizar o diagnóstico.\nGentileza procurar ajuda médica caso apareça algum sintoma.");
        }
    }

    static string AskQuestion(string question)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Write(question);
            string answer = Console.ReadLine().ToUpper();

            if (answer == "SIM" || answer == "NAO")
            {
                return answer;
            }
            else if (i < 2)
            {
                Console.WriteLine("Resposta inválida. Por favor, responda com SIM ou NAO.");
            }
        }

        throw new InvalidResponseException();
    }

    static int ReadInt()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                return number;
            }
            else
            {
                Console.Write("Entrada inválida. Por favor, insira um número válido: ");
            }
        }
    }
}

class InvalidResponseException : Exception { }

