namespace Guerrero_TareaMVVM.DG_Views;

public partial class DG_TodasNotas : ContentPage
{
	public DG_TodasNotas()
	{
		InitializeComponent();
	}
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}