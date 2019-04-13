using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.Base
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

    }
}
