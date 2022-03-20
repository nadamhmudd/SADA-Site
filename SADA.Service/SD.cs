namespace SADA.Service;

//Static - Constants Details
public static class SD
{
    public const string Ascending   = "ASC";
    public const string Descending  = "DESC";

    public const string Role_User   = "User";
    public const string Role_Admin  = "Admin";

    public const string SessionCart       = "SessionShoppingCart";
    public const string SessionLoggedUser = "SessionLoggedUser";

    public enum Status
    {
        Pending, Approved, Processing, Shipped, Delivered, Cancelled, Refunded
    };
}
