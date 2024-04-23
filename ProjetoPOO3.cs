using System;
using System.Collections.Generic;

namespace Vacinas
{
    class Program
    {
        static List<Pessoa> _pessoas = new List<Pessoa>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Bem-vindo ao sistema de vacinação!");
                Console.WriteLine("Você é um cidadão ou funcionário? (Digite 'c' para cidadão ou 'f' para funcionário)");
                char choice = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (choice == 'c')
                {
                    while (true)
                    {
                        if (CidadaoMenu())
                        {
                            break; // Se o login for bem-sucedido, saia do loop
                        }
                        else
                        {
                            Console.WriteLine("Usuário não encontrado. Tente novamente.");
                        }
                    }
                    break;
                }
                else if (choice == 'f')
                {
                    while (true)
                    {
                        if (FuncionarioMenu())
                        {
                            break; // Se o login for bem-sucedido, saia do loop
                        }
                        else
                        {
                            Console.WriteLine("Usuário não encontrado. Tente novamente.");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                }
            }
        }

        static bool CidadaoMenu()
        {
            Console.Clear();
            Console.WriteLine("Criação de conta:");
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite sua idade: ");
            int idade = int.Parse(Console.ReadLine());
            Console.Write("Digite seu CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Digite seu Telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("Digite seu e-mail: ");
            string email = Console.ReadLine();

            Cidadao novoCidadao = new Cidadao(nome, cpf, idade, false, telefone, email);
            _pessoas.Add(novoCidadao);

            Console.WriteLine("Login:");
            Console.Write("Digite seu nome: ");
            string loginNome = Console.ReadLine();
            Console.Write("Digite seu CPF: ");
            string loginCpf = Console.ReadLine();

            Pessoa pessoaLogada = EncontrarCidadao(loginNome, loginCpf);
            if (pessoaLogada == null)
            {
                Console.WriteLine("Usuário não encontrado. Tente novamente.");
                return false;
            }

            Console.WriteLine("Você foi vacinado? (s/n)");
            bool vacinado = char.ToLower(Console.ReadKey().KeyChar) == 's';
            Console.WriteLine();

            Console.WriteLine($"Nome: {pessoaLogada.Nome}");
            Console.WriteLine($"Idade: {pessoaLogada.Idade}");
            Console.WriteLine($"CPF: {pessoaLogada.CPF}");
            Console.WriteLine($"Vacinado: {(vacinado ? "Sim" : "Não")}");

            Console.WriteLine("Deseja agendar sua vacinação? (s/n)");
            if (char.ToLower(Console.ReadKey().KeyChar) == 's')
            {
                Console.WriteLine("\nDigite a data da vacinação (formato: DD/MM/AAAA):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
                {
                    ((Cidadao)pessoaLogada).AgendarVacinacao(data);
                    Console.WriteLine("Vacinação agendada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Data inválida. Tente novamente.");
                }
            }

            return true;
        }

        static bool FuncionarioMenu()
        {
            Console.Clear();
            Console.WriteLine("Criação de conta:");
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite sua matrícula: ");
            string matricula = Console.ReadLine();
            Console.Write("Digite o CNPJ da empresa: ");
            string cnpj = Console.ReadLine();
            Console.Write("Digite seu número de telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("Digite seu e-mail: ");
            string email = Console.ReadLine();
            Console.WriteLine();

            Funcionario novoFuncionario = new Funcionario(nome, matricula, cnpj, telefone, email);
            _pessoas.Add(novoFuncionario);

            Console.WriteLine("Login:");
            Console.Write("Digite seu nome: ");
            string loginNome = Console.ReadLine();
            Console.Write("Digite sua matrícula: ");
            string loginMatricula = Console.ReadLine();

            Pessoa pessoaLogada = EncontrarFuncionario(loginNome, loginMatricula);
            if (pessoaLogada == null)
            {
                Console.WriteLine("Usuário não encontrado. Tente novamente.");
                return false;
            }

            if (pessoaLogada is Funcionario funcionarioLogado)
            {
                Console.WriteLine($"Nome: {funcionarioLogado.Nome}");
                Console.WriteLine($"Matrícula: {funcionarioLogado.Matricula}");
                Console.WriteLine($"CNPJ da empresa: {funcionarioLogado.CNPJ}");
            }

            Console.WriteLine("Deseja adicionar um cidadão à lista de vacinados? (s/n)");
            if (char.ToLower(Console.ReadKey().KeyChar) == 's')
            {
                AdicionarCidadao();
            }

            Console.WriteLine("Deseja ver a lista de cidadãos vacinados? (s/n)");
            if (char.ToLower(Console.ReadKey().KeyChar) == 's')
            {
                MostrarVacinados();
            }

            return true;
        }

        static void AdicionarCidadao()
        {
            Console.Clear();
            Console.WriteLine("\nAdicionar cidadão à lista de vacinados:");
            Console.Write("Digite o nome do cidadão: ");
            string cidadaoNome = Console.ReadLine();
            Console.Write("Digite o CPF do cidadão: ");
            string cidadaoCpf = Console.ReadLine();

            Cidadao novoCidadao = new Cidadao(cidadaoNome, cidadaoCpf);
            novoCidadao.Vacinado = true;
            _pessoas.Add(novoCidadao);

            Console.WriteLine($"{cidadaoNome} adicionado à lista de vacinados com sucesso!");
        }


        static void MostrarVacinados()
        {
            Console.Clear();
            Console.WriteLine("\nLista de cidadãos vacinados:");
            foreach (var pessoa in _pessoas)
            {
                if (pessoa is Cidadao cidadao && cidadao.Vacinado)
                {
                    Console.WriteLine($"Nome: {pessoa.Nome}, CPF: {pessoa.CPF}");
                }
            }
        }

        static Pessoa EncontrarCidadao(string nome, string cpf)
        {
            foreach (var pessoa in _pessoas)
            {
                if (pessoa.Nome == nome && pessoa.CPF == cpf)
                {
                    return pessoa;
                }
            }
            return null;
        }
        
        static Pessoa EncontrarFuncionario(string nome, string matricula)
        {
            foreach (var pessoa in _pessoas)
            {
                if (pessoa is Funcionario funcionario && funcionario.Nome == nome && funcionario.Matricula == matricula)
                {
                    return funcionario;
                }
            }
            return null;
        }
    }

    class Pessoa
    {
        public string Nome { get; }
        public int Idade { get; }
        public string CPF { get; }
        public string Telefone { get; }
        public string Email { get; }

        public Pessoa(string nome, string cpf, int idade, string telefone, string email)
        {
            Nome = nome;
            CPF = cpf;
            Idade = idade;
            Telefone = telefone;
            Email = email;
        }
    }

    class Cidadao : Pessoa
    {
        public bool Vacinado { get; set; }
        public DateTime Agendamento { get; set; }

        public Cidadao(string nome, string cpf, int idade, bool vacinado, string telefone, string email)
            : base(nome, cpf, idade, telefone, email)
        {
            Vacinado = vacinado;
        }

        public Cidadao(string nome, string cpf) : this(nome, cpf, 0, false, " ", " ") { }

        public void AgendarVacinacao(DateTime data)
        {
            Agendamento = data;
        }
    }

    class Funcionario : Pessoa
    {
        public string Matricula { get; }
        public string CNPJ { get; }

        public Funcionario(string nome, string matricula, string cnpj, string telefone, string email)
            : base(nome, "", 0, telefone, email)
        {
            Matricula = matricula;
            CNPJ = cnpj;
        }
    }
}
