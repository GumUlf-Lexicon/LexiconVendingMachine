using System;

namespace LexiconVendingMachine.Model.Products
{
	class Tool: Product
	{
		// A description of the tool's color
		private string _color;
		public string Color
		{
			get => _color;

			private set
			{
				if(string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("The tool must have a color specified");
				}
				_color = value;
			}
		}

		// What kind of tool is it?
		private ToolType _typeOfTool;
		public ToolType TypeOfTool
		{
			get => _typeOfTool;

			private set => _typeOfTool = value;
		}

		// Basic contructor for the class
		public Tool(string description, int price, string color)
		{
			Description = description;
			Price = price;
			Color = color;
			UsingAction = "You use the tool to do the work needed to be done!";
		}

		// Full constructor for the class
		public Tool(string description, int price, string color, string usingAction):
			this(description, price, color)
		{
			UsingAction = usingAction;
		}

		// Return a string that describes the tool and its price.
		public override string Examine()
		{
			return $"{$"{Description}, {Color}",-50} {Price,5} SEK";
		}

		// The tool types
		public enum ToolType
		{
			HandTool,
			PowerTool,
			Other
		}


	}

}
