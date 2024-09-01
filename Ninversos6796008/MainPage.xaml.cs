namespace Ninversos6796008
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDatabase _dbService;
        private int _editNumeroId;

        public MainPage(AppDatabase dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetNumero());

        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(numeroOriginalEntryField.Text, out int numeroOriginal))
            {
                var numeroInvertido = InvertirNumero(numeroOriginal);

                if (_editNumeroId == 0)
                {
                    await _dbService.Create(new Numero
                    {
                        NumeroOriginal = numeroOriginal,
                        NumeroInvertido = numeroInvertido
                    });
                }
                else
                {
                    await _dbService.Update(new Numero
                    {
                        Id = _editNumeroId,
                        NumeroOriginal = numeroOriginal,
                        NumeroInvertido = numeroInvertido
                    });

                    _editNumeroId = 0;
                }

                numeroOriginalEntryField.Text = string.Empty;

            }
            else
            {
                await DisplayAlert("Error", "Por favor, ingrese un número válido", "OK");
            }
            listview.ItemsSource = await _dbService.GetNumero();

        }

        private int InvertirNumero(int numero)
        {
            var numeroStr = numero.ToString();
            var numeroInvertidoStr = new string(numeroStr.Reverse().ToArray());
            return int.Parse(numeroInvertidoStr);
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var numero = (Numero)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editNumeroId = numero.Id;
                    numeroOriginalEntryField.Text = numero.NumeroOriginal.ToString();
                    break;

                case "Delete":
                    await _dbService.Delete(numero);
                    listview.ItemsSource = await _dbService.GetNumero();
                    break;
            }
        }
    }

}


