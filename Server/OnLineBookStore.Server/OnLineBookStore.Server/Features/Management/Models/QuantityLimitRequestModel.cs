using System.ComponentModel.DataAnnotations;

namespace OnLineBookStore.Server.Features.Management.Models
{
    public class QuantityLimitRequestModel
    {
        [Range(0, int.MaxValue)]
        public int Limit { get; set; }
    }
}
