using System;
using Microsoft.Maui.Controls;
using Stockly.ViewModels;
using Stockly.Models;

namespace Stockly.Views
{
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            var viewModel = new ProdutoViewModel();
            viewModel.ProdutoSelecionado = new Produto();
            BindingContext = viewModel;
        }
    }
}