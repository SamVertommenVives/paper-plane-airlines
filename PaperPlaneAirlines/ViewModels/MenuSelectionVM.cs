namespace PaperPlaneAirlines.ViewModels;

public class MenuSelectionVM
{
    public BookingOptionVM BookingOption { get; set; }
    public List<MenuVM>? StandardMenus { get; set; }
    public List<MenuVM>? LocalMenus { get; set; }
    
    public int SelectedMenuId { get; set; }
}