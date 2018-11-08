using Sokoban_2._0_Console.Graphics.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Graphics.LittleConsole
{
    public class LittleConsoleDataRepresentationFactory : DataRepresentationFactory
    {
        public WorkerRepresentation Worker => new LittleWorkerRepresentation();
        public FieldRepresentation Field => new LittleFieldRepresentation();
    }
}
