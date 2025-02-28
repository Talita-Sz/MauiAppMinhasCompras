using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{ //
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;

	}

    protected async override void OnAppearing()
    {
		List<Produto> tmp = await App.Db.GetAll();//toda vez que a tela aparecer

		tmp.ForEach(i => lista.Add(i));//vai no sqlite buscar a lista e abastece-la
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{

			Navigation.PushAsync(new Views.NovoProduto());

		}catch (Exception ex) 
		{
			DisplayAlert("Ops!", ex.Message, "Ok");
		}
    }
}