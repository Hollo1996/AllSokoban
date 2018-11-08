using System;
using System.Collections.Generic;
using Sokoban_2._0_Console.Graphics;

namespace Sokoban_2._0_Console.UpperLayer.Map
{
    public class FieldContainer
	{

        public Dictionary<int, Dictionary<int, Field>> fieldsMap = new Dictionary<int, Dictionary<int, Field>>();
		public Field this[Position position]
		{
			get{
				return fieldsMap[position.Column][position.Line];
			}
		}

		//private singleton
		private static FieldContainer onlyInstance=new FieldContainer();
        private FieldContainer() { }
        
		public static void Send(){
			Field.FieldContainer=onlyInstance;
			Graphic.FieldContainer=onlyInstance;
			GameField.FieldContainer=onlyInstance;
		}

    }
}
