using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Common
{
    public class Entity:IEntity<Guid>
    {
        [Key] 
      public  Guid Id { get; set; }
      public DateTimeOffset Created { get; set; }
    }
}
