using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console
{
    public interface MoveableVisitor
    {
        void Visit(Box b);
        void Visit(Worker b);
    }
}
