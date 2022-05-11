using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace RpnApi.Models
{
    public class StackRequest
    {
        [Required]
        [SwaggerSchema("The buyer candidate Id used to create account.")]
        public IEnumerable<Entry> stacks { get; set; }
    }
}
