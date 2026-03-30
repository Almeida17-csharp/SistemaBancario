using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Banco
{
    internal class Program
    {
        class ContaBancaria
        {
            public string Nome;
            public string Cpf;
            public int NumeroConta;
            public string Senha;
            public decimal Saldo;
            public string Extrato;
        }

        static List<ContaBancaria> clientes = new List<ContaBancaria>();
        static void Main(string[] args)
        {

            int funcaobanco;
            string cabeca;
            string cont = "y";

            Console.Clear();
            string nomeadm;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Digite seu nome(administrador): ");
            nomeadm = Console.ReadLine();

            DateTime dateAtualmenu = DateTime.Now;
            DateTime horaAtualmenu = DateTime.Now;

            string saudacaomenu;

            if (horaAtualmenu.Hour >= 5 && horaAtualmenu.Hour < 12) saudacaomenu = "Bom dia";
            else if (horaAtualmenu.Hour >= 12 && horaAtualmenu.Hour < 18) saudacaomenu = "Boa tarde";
            else if (horaAtualmenu.Hour >= 0 && horaAtualmenu.Hour < 5) saudacaomenu = "Boa madrugada";
            else saudacaomenu = "Boa noite";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"============================================================");
            Console.WriteLine($"   {saudacaomenu} {nomeadm}, Seja bem vindo!");
            Console.WriteLine($"----------------------------------------------------------");
            Console.WriteLine("    Hoje é dia: " + dateAtualmenu.ToString("dd/MM/yyyy"));
            Console.WriteLine("    Agora são: " + horaAtualmenu.ToString("HH:mm:ss"));
            Console.WriteLine($"============================================================\n");

            Console.WriteLine($"Você é programador do Banco PathBuying? (S/N)");
            cabeca = Console.ReadLine().ToLower();

            bool exibirPerguntaFinal = true;

            do
            {
                exibirPerguntaFinal = true;

                try
                {
                    if (cabeca == "s" || cabeca == "sim")
                    {
                        Console.ResetColor();
                        Console.Clear();
                        Console.WriteLine(" 1. Adicionar Usuário no sistema");
                        Console.WriteLine(" 2. Ver Contas Adicionadas");
                        Console.WriteLine(" 3. Acessar Conta");
                        Console.WriteLine(" 4. Sair \n");
                        Console.WriteLine("Tire bom proveito do nosso sistema!");
                        Console.WriteLine("===========================================");
                        Console.Write("Digite a opção desejada: ");

                        funcaobanco = int.Parse(Console.ReadLine());

                        switch (funcaobanco)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Adicione o novo usuário.");
                                Console.WriteLine("===========================================");
                                AdicionarUsuario();
                                Console.WriteLine("===========================================");
                                Console.WriteLine("Adicionando Usuário...");
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Listando Contas existentes no banco.");
                                Console.WriteLine("===========================================");
                                VerContas();
                                Console.WriteLine("===========================================");
                                Console.WriteLine("Essas foram as contas encontradas...");
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Acesse a Conta Desejada.");
                                Console.WriteLine("===========================================");

                                Console.WriteLine("Acessando o portal de contas...\n");

                                AcessarConta();

                                exibirPerguntaFinal = false;
                                cont = "y";
                                break;
                            case 4:
                                Console.WriteLine("Tem certeza que deseja encerrar? (S/N)");
                                string certeza = Console.ReadLine().ToLower();
                                if (certeza == "s" || certeza == "sim")
                                {
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    exibirPerguntaFinal = false;
                                    cont = "y";
                                }
                                break;
                            default:
                                Console.WriteLine("Opção Inválida!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Você gostaria de se tornar? (S/N)");
                        string resposta = Console.ReadLine().ToLower();
                        if (resposta == "s")
                        {

                        }
                        else Environment.Exit(0);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Digite apenas números para as opções!");
                }
                finally
                {
                    if (exibirPerguntaFinal)
                    {
                        Console.WriteLine("\nDeseja realizar outra operação? (Y/N)");
                        cont = Console.ReadLine().ToLower();
                    }
                    Console.Clear();
                }
            } while (cont == "y");
        }
        static void AdicionarUsuario()
        {
            Random numconta = new Random();

            ContaBancaria novaConta = new ContaBancaria();

            Console.Write("Digite o nome do novo usuário: ");
            novaConta.Nome = Console.ReadLine();

            Console.Write("\nDigite o cpf desse usuário: ");
            novaConta.Cpf = LerCpfValido();

            Console.Write("\nDigite a senha desse usuário: ");
            novaConta.Senha = LerSenha();

            novaConta.NumeroConta = numconta.Next(99999);
            novaConta.Saldo = 0.0m;
            novaConta.Extrato = "";

            clientes.Add(novaConta);

            Console.WriteLine($"\nUsuário {novaConta.Nome} adicionado com sucesso!");
            Console.WriteLine($"Conta: {novaConta.NumeroConta} criada com sucesso!");
        }
        static void VerContas()
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum usuário foi cadastrado ainda.");
            }
            else
            {
                Console.WriteLine("Esses são os Usuários ativos no banco:\n");

                for (int i = 0; i < clientes.Count; i++)
                {
                    Console.WriteLine($"Nome: {clientes[i].Nome} | CPF: {clientes[i].Cpf} | Conta: {clientes[i].NumeroConta}");
                }
            }
        }
        static void AcessarConta()
        {
            try
            {
                Console.Write("Digite o número da conta que deseja acessar: ");
                int contaBuscada = int.Parse(Console.ReadLine());

                int posicao = clientes.FindIndex(c => c.NumeroConta == contaBuscada);

                if (posicao != -1)
                {
                    Console.Write("Digite a senha da conta: ");
                    string senhaBuscada = LerSenha();

                    if (clientes[posicao].Senha == senhaBuscada)
                    {
                        Console.WriteLine("\nConta encontrada com sucesso!");
                        Console.WriteLine($"Bem-vindo(a), {clientes[posicao].Nome}!");
                        Console.WriteLine($"CPF vinculado: {clientes[posicao].Cpf}\n");

                        bool sairDaConta = false;

                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"--- Menu da Conta: {clientes[posicao].Nome} ---");
                            Console.WriteLine($"Seu Valor na Conta: R$ {clientes[posicao].Saldo}");

                            Console.ResetColor();
                            Console.WriteLine("\n 1. Saque");
                            Console.WriteLine(" 2. Depósito");
                            Console.WriteLine(" 3. Transferência");
                            Console.WriteLine(" 4. Ver Extrato");
                            Console.WriteLine(" 5. Voltar ao Menu Principal \n");
                            Console.WriteLine("===========================================");
                            Console.Write("Digite a opção desejada: ");

                            int funcaoconta = int.Parse(Console.ReadLine());

                            switch (funcaoconta)
                            {
                                case 1:
                                    Console.Clear();
                                    Saque(posicao);
                                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Deposito(posicao);
                                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.Clear();
                                    Transferencia(posicao);
                                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();
                                    Console.WriteLine("Exibindo o extrato da conta...");
                                    VerExtrato(posicao);
                                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    sairDaConta = true;
                                    break;
                                default:
                                    Console.WriteLine("Opção Inválida!");
                                    Console.WriteLine("\nPressione qualquer tecla para tentar de novo...");
                                    Console.ReadKey();
                                    break;
                            }

                        } while (sairDaConta == false);
                    }
                    else
                    {
                        Console.WriteLine("\nSenha incorreta! Acesso negado.");
                    }
                }
                else
                {
                    Console.WriteLine("\nConta não encontrada! Verifique o número digitado.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Erro: Digite apenas números válidos para o valor!");
            }
            
        }
        static void Deposito(int posicao)
        {
            // A nossa função nova já faz a pergunta, tenta converter e segura os erros!
            decimal valoradepositar = LerValorFinanceiro("Digite o valor que deseja depositar: R$ ");

            if (valoradepositar > 0)
            {
                clientes[posicao].Saldo = clientes[posicao].Saldo + valoradepositar;

                Console.WriteLine($"\nDepósito de R$ {valoradepositar} realizado com sucesso!");
                Console.WriteLine($"Seu novo saldo é: R$ {clientes[posicao].Saldo}");
                clientes[posicao].Extrato += $"- Depósito (R$ {valoradepositar})\n";
            }
            else
            {
                Console.WriteLine("\nOperação negada: Você não pode depositar valores negativos ou zero!");
            }
        }
        static void Saque(int posicao)
        {
            // A nossa máquina mágica já faz todo o trabalho sujo aqui!
            decimal valorasacar = LerValorFinanceiro("Digite o valor que deseja sacar: R$ ");

            if (valorasacar > 0)
            {
                if (valorasacar <= clientes[posicao].Saldo)
                {
                    clientes[posicao].Saldo = clientes[posicao].Saldo - valorasacar;
                    Console.WriteLine($"\nSaque de R$ {valorasacar} realizado com sucesso!");
                    Console.WriteLine($"Seu novo saldo é: R$ {clientes[posicao].Saldo}");
                    clientes[posicao].Extrato += $"- Saque (R$ {valorasacar})\n";
                }
                else
                {
                    Console.WriteLine("\nOperação negada: Saldo insuficiente para realizar este saque!");
                    Console.WriteLine($"Seu saldo atual é apenas: R$ {clientes[posicao].Saldo}");
                }
            }
            else
            {
                Console.WriteLine("\nOperação negada: Você não pode sacar valores negativos ou zero!");
            }
        }
        static void Transferencia(int posicao)
        {
            // Usando a nossa função limpa para o dinheiro
            decimal valoratransferir = LerValorFinanceiro("Digite o valor que deseja transferir: R$ ");

            if (valoratransferir > 0)
            {
                try
                {
                    Console.Write("Para qual número de conta gostaria de transferir? ");
                    int contaBuscadaF = int.Parse(Console.ReadLine());

                    if (contaBuscadaF == clientes[posicao].NumeroConta)
                    {
                        Console.WriteLine("\nOperação negada: Você não pode transferir dinheiro para si mesmo!");
                        return;
                    }

                    int posicaofinal = clientes.FindIndex(c => c.NumeroConta == contaBuscadaF);

                    if (posicaofinal != -1)
                    {
                        if (valoratransferir <= clientes[posicao].Saldo)
                        {
                            clientes[posicao].Saldo = clientes[posicao].Saldo - valoratransferir;
                            clientes[posicaofinal].Saldo = clientes[posicaofinal].Saldo + valoratransferir;

                            clientes[posicao].Extrato += $"- Transferência enviada (R$ {valoratransferir}) para {clientes[posicaofinal].Nome}\n";
                            clientes[posicaofinal].Extrato += $"+ Transferência recebida (R$ {valoratransferir}) de {clientes[posicao].Nome}\n";

                            Console.WriteLine($"\nTransferência de R$ {valoratransferir} realizada com sucesso!");
                            Console.WriteLine($"Seu novo saldo é: R$ {clientes[posicao].Saldo}");
                        }
                        else
                        {
                            Console.WriteLine("\nOperação negada: Saldo insuficiente para esta transferência!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nConta de destino não encontrada! Transferência cancelada.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nErro: Digite apenas números inteiros para buscar a conta de destino!");
                }
            }
            else
            {
                Console.WriteLine("\nOperação negada: Você não pode transferir valores negativos ou zero!");
            }
        }
        static void VerExtrato(int posicao)
        {
                Console.WriteLine($"--- Extrato de {clientes[posicao].Nome} ---");

                // Se a gaveta estiver vazia, avisa que não tem movimentação
                if (clientes[posicao].Extrato == "")
                {
                    Console.WriteLine("Nenhuma movimentação registrada nesta conta ainda.");
                }
                else
                {
                    // Se tiver texto, imprime tudo!
                    Console.WriteLine(clientes[posicao].Extrato);
                }
        }
        static string LerSenha()
        {
            string senhaSecreta = "";

            // O loop infinito (while true) vai rodar até a pessoa apertar ENTER
            while (true)
            {
                // Pega a tecla digitada sem mostrar na tela
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                // 1. Se a pessoa apertou ENTER, acabou a senha!
                if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // Pula para a próxima linha para o console não ficar bagunçado
                    break; // Quebra o loop
                }
                // 2. O grande truque: Se a pessoa apertou BACKSPACE (apagar)
                else if (tecla.Key == ConsoleKey.Backspace)
                {
                    // Só apaga se já tiver alguma letra na senha
                    if (senhaSecreta.Length > 0)
                    {
                        // Remove a última letra da variável
                        senhaSecreta = senhaSecreta.Substring(0, senhaSecreta.Length - 1);

                        // Magia no console: volta o cursor um espaço (\b), escreve um espaço em branco para apagar o asterisco, e volta o cursor de novo (\b)
                        Console.Write("\b \b");
                    }
                }
                // 3. Se for qualquer outra tecla normal (letras, números...)
                else
                {
                    senhaSecreta = senhaSecreta + tecla.KeyChar; // Guarda a letra de verdade
                    Console.Write("*"); // Imprime o asterisco de mentira
                }
            }

            return senhaSecreta; // Devolve a senha real para o seu programa usar
        }
        static decimal LerValorFinanceiro(string mensagem)
        {
            while (true) // Fica repetindo para sempre...
            {
                try
                {
                    Console.Write(mensagem);
                    decimal valor = decimal.Parse(Console.ReadLine());
                    return valor; // ...ATÉ a pessoa digitar certo. Aí ele devolve o valor e sai do loop!
                }
                catch (FormatException)
                {
                    // Se der erro, ele avisa e o 'while' faz ele tentar de novo
                    Console.WriteLine("Erro: Digite apenas números válidos para o valor! (Use vírgula para centavos)\n");
                }
            }
        }
        static string LerCpfValido()
        {
            while (true)
            {
                Console.Write("\nDigite o CPF (apenas números ou com pontuação): ");
                string cpfDigitado = Console.ReadLine();

                // 1. O TRUQUE MÁGICO DO REGEX: "[^0-9]" significa "tudo que NÃO for número"
                // Ele acha os pontos, traços e espaços e substitui por "" (nada), apagando a sujeira.
                string apenasNumeros = Regex.Replace(cpfDigitado, "[^0-9]", "");

                // 2. Confere se, depois de limpar, sobraram os 11 números certinhos
                if (apenasNumeros.Length == 11)
                {
                    // 3. O C# recorta os pedaços (Substring) e costura o formato perfeito para salvar na lista!
                    string cpfFormatado = $"{apenasNumeros.Substring(0, 3)}.{apenasNumeros.Substring(3, 3)}.{apenasNumeros.Substring(6, 3)}-{apenasNumeros.Substring(9, 2)}";

                    return cpfFormatado; // Devolve o CPF impecável
                }
                else
                {
                    Console.WriteLine("Erro: CPF inválido! Certifique-se de digitar os 11 números do documento.");
                }
            }
        }
    }
}
