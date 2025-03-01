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
        try
        {


            List<Produto> tmp = await App.Db.GetAll();//toda vez que a tela aparecer

            tmp.ForEach(i => lista.Add(i));//vai no sqlite buscar a lista e abastece-la
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }


    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {

            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops!", ex.Message, "Ok");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string Q = e.NewTextValue;

            List<Produto> tmp = await App.Db.Search(Q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops!", ex.Message, "Ok");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é{soma:C}";

        DisplayAlert("Total dos produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Produto P = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert("Tem certeza que deseja excluir?", $"Remover {P.Descricao}", "Sim", "Não");

            if (confirm) {

                await App.Db.Delete(P.Id);
                lista.Remove(P);
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto P = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto {

                BindingContext = P,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}