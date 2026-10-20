using Stockly.Models;
using Stockly.Services;
using Stockly.Singleton;
using Stockly.Views;
using Microsoft.Maui.Controls;

namespace Stockly.ViewModels
{
    public class UsuarioViewModel : BaseNotifyViewModel
    {
        private DatabaseService _dbService;

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; OnPropertyChanged(); }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(); }
        }

        private string _cpf;
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; OnPropertyChanged(); }
        }

        private string _tipoSelecionado;
        public string TipoSelecionado
        {
            get { return _tipoSelecionado; }
            set { _tipoSelecionado = value; OnPropertyChanged(); }
        }

        public Command EntrarCommand 
        {
            get
            {
                return new Command(() =>
                {
                    RealizarLogin();
                });
            }
        }
        public Command RegistrarCommand 
        {
            get
            {
                return new Command(() =>
                {
                    Registrar();
                });
            }
        }
        public Command SalvarCadastroCommand 
        {
            get
            {
                return new Command(() =>
                {
                    SalvarNovoUsuario();
                });
            }
        }

        public Command VoltarCommand
        {
            get
            {
                return new Command(() =>
                {
                    Voltar();
                });
            }
        }

        public UsuarioViewModel()
        {
            _dbService = new DatabaseService();

        }

        private void Registrar()
        {
            Application.Current.MainPage = new NavigationPage(new RegistroPage());
        }

        private void Voltar()
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void RealizarLogin()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Senha))
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Preencha e-mail e senha.", "OK");
                return;
            }

            Usuario usuarioEncontrado = _dbService.ValidarLogin(Email, Senha);

            if (usuarioEncontrado != null)
            {
                UsuarioAtual.Instancia.SetUsuario(usuarioEncontrado);

                if (usuarioEncontrado.Tipo == "Administrador")
                {
                    Application.Current.MainPage.DisplayAlert("Sucesso", "Bem-vindo, Admin!", "OK");
                    Application.Current.MainPage = new NavigationPage(new AdminPage());
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Sucesso", "Bem-vindo, Cliente!", "OK");
                    Application.Current.MainPage = new NavigationPage(new ClientePage());
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Erro", "E-mail ou senha incorretos.", "OK");
            }
        }

        private void SalvarNovoUsuario()
        {
            if (string.IsNullOrWhiteSpace(Nome) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Senha) || string.IsNullOrWhiteSpace(TipoSelecionado))
            {
                Application.Current.MainPage.DisplayAlert("Aviso", "Preencha todos os campos obrigatórios.", "OK");
                return;
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = this.Nome,
                CPF = this.CPF,
                Email = this.Email,
                Senha = this.Senha,
                Tipo = this.TipoSelecionado
            };

            _dbService.SalvarUsuario(novoUsuario);

            Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário cadastrado com sucesso!", "OK");
            Voltar();
        }
    }
}