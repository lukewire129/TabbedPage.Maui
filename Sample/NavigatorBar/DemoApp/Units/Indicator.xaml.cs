using TabbedPage.Maui.UI.Units;

namespace DemoApp.Units;

public partial class Indicator : BottomNavigationBarIndicator
{
	public Indicator()
	{
		InitializeComponent();
	}

    public override void SelectionItem(int idx)
    {
        PART_Circle.TranslateTo (idx * 80, 0);
    }
}