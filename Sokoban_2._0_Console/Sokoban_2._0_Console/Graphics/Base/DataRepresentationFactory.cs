using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Graphics.Base
{
    public interface DataRepresentationFactory
    {
        WorkerRepresentation Worker { get; }
        FieldRepresentation Field { get; }
    }
}
