using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

		
		public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        // Carrega todos os produtos inicialmente
        List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach( i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		} catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "ok");
		}
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string query = e.NewTextValue?.ToLower(); // Obtém o texto da busca em minúsculo

        // Limpa a lista de produtos antes de buscar os resultados filtrados
        lista.Clear();

        if (string.IsNullOrEmpty(query))
        {
            // Se o texto da busca estiver vazio, carrega todos os produtos
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }
        else
        {
            // Caso contrário, faz a busca filtrada com a consulta digitada
            List<Produto> tmp = await App.Db.Search(query);
            tmp.ForEach(i => lista.Add(i));
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg,"ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {


    }
}