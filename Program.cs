using System;
using System.IO;

namespace LoginSystem
{
    class Program
    {
        static string usersFile = "users.txt";
        static void Main(string[] args)
        {

            Console.WriteLine("+-----------------------------+");
            Console.Write("Sistema de Cadastro");
            Console.WriteLine("\n+-----------------------------+");


            while (true)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1. Cadastrar novo usuário\n2. Fazer login\n3. Sair");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        registerUser();
                        break;
                    case "2":
                        MakeLogin();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida! Tente novamente");
                        break;
                }
            }


            static void registerUser()
            {
                Console.WriteLine("\n--- Cadastro de Usuário ---\n");
            writeEmail:
                Console.Write("Digite seu email: ");

                string email = Console.ReadLine();

                if (UserExists(email))
                {
                    Console.Clear();
                    Console.WriteLine("Usuário já existe! Digite novamente.\n");
                    goto writeEmail;
                }

                Console.Write("Escolha sua senha: ");
                string password = ReadHiddenPassword();

                SaveUser(email, password);
                Console.Clear();
                Console.WriteLine("Usuário cadastrado com sucesso!\n");
            }

            static void MakeLogin()
            {
                Console.WriteLine("\n--- Login ---");

                Console.Write("E-mail: ");
                string email = Console.ReadLine();

                Console.Write("Senha: ");
                string password = ReadHiddenPassword();

                if (VerifyCredentials(email, password))
                {
                    Console.Clear();
                    Console.WriteLine("Acesso permitido! Bem-vindo(a) ao programa!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Acesso negado!");
                }
            }

            static bool VerifyCredentials(string email, string password)
            {
                if (!File.Exists(usersFile)) return false;

                string[] lines = File.ReadAllLines(usersFile);
                foreach (string line in lines)
                {
                    string[] data = line.Split(";");
                    if (data[0] == email && data[1] == password) return true;
                }
                return false;
            }

            static bool UserExists(string email)
            {
                if (!File.Exists(usersFile)) return false;

                string[] lines = File.ReadAllLines(usersFile);

                foreach (string line in lines)
                {
                    string[] data = line.Split(";");
                    if (data[0] == email) return true;
                }
                return false;
            }

            static void SaveUser(string user, string password)
            {
                using (StreamWriter sw = File.AppendText(usersFile))
                {
                    sw.WriteLine($"{user};{password}");
                }
            }

            static string ReadHiddenPassword()
            {
                string password = "";
                ConsoleKeyInfo tecla;

                do
                {
                    tecla = Console.ReadKey(true);

                    if (tecla.Key != ConsoleKey.Enter)
                    {
                        password += tecla.KeyChar;
                        Console.Write("*");
                    }
                } while (tecla.Key != ConsoleKey.Enter);

                Console.WriteLine();
                return password;
            }

        }
    }
}
