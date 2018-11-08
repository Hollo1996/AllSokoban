using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    public class Button : Feature, MoveableVisitor
    {
        private bool isPushed;
        private List<Switchable> traps = new List<Switchable>();

        // Adds a switchable type (trap) to the list
        public void AddSwitchable(Switchable switchable)
        {
            traps.Add(switchable);
        }

        public string GetFeatureString()
        {
            throw new NotImplementedException();
        }

        // Interacting with the Moveable object
        // Initiating the Visitor pattern
        public void Interact(Moveable Moveable)
        {
            Moveable.Accept(this);
        }

        public void Print(string s, int x, int y)
        {
            if (s.Equals("bu"))
            {
                Console.WriteLine("(" + x + ";" + y + ") " + isPushed);
            }
            else if (s.Equals("buttonConnectives"))
            {
                foreach (Switchable switchable in traps)
                {
                    FallTrap ft = (FallTrap)switchable;
                    Console.WriteLine("(" + x + ";" + y + ")-(" + ft.GetMasterField().coordX + ";" + ft.GetMasterField().coordY + ")");
                }
            }
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a Box
        // If the button was not pushed before, we push it and switch on every trap
        public void Visit(Box b)
        {
            if (!isPushed)
            {
                traps.ForEach(x=>x.SwitchOn());
                isPushed = true;
            }
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a Worker, nothing happens
        public void Visit(Worker w)
        {
            if (isPushed)
            {
                traps.ForEach(x => x.SwitchOff());
                isPushed = false;
            }
        }
    }
}

/*
 package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.entities.*;

import java.io.File;
import java.util.List;

public class Button implements Feature, MoveableVisitor {






    public string GetFeatureString(){
        return "imageSet/button.png";
    }
}*/
