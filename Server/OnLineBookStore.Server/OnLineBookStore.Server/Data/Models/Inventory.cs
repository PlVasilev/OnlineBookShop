namespace OnLineBookStore.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Inventory
    {
        [Key]
        public string Id { get; set; }

        [Range(0,int.MaxValue)]
        public int QantityLimitTreshhold { get; set; }
    }
}
