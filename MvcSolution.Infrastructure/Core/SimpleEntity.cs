using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution
{
    public interface ISimpleEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public class SimpleEntity : ISimpleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SimpleEntity()
        {
            
        }

        public SimpleEntity(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
