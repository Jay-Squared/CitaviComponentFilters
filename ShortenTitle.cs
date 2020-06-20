using System;
using System.Linq;
using System.Collections.Generic;
using SwissAcademic.Citavi;
using SwissAcademic.Citavi.Metadata;
using SwissAcademic.Collections;
using SwissAcademic.Drawing;

namespace SwissAcademic.Citavi.Citations
{
	public class ComponentPartFilter
		:
		IComponentPartFilter
	{
		public IEnumerable<ITextUnit> GetTextUnits(ComponentPart componentPart, Template template, Citation citation, out bool handled)
		{
			handled = true;
						
			var output = componentPart.GetTextUnitsUnfiltered(citation, template); 
			var index = 0;
			foreach (ITextUnit textUnit in output)
			{
				string text = textUnit.Text;
				List<string> textList = new List<string>();
				for (int i = 0; i < text.Split().Count(); i++)
				{
					if ((i == 0 || i == text.Split().Count() -1) && ignoreList.Contains(text.Split()[i])) continue;
					textList.Add(text.Split()[i]);					
				}
				text = string.Join(" ", textList);
				text = TruncateAtWord(text, 35);
				text = FirstCharToUpper(text);
				textUnit.Text = text;
			}
			
			return output;
		}
		List<String> ignoreList = new List<String>()
        {
			"Des",
			"des",
			"Der",
			"der",
			"Die",
			"die",
			"Das",
			"das",
			"The",
			"the",
        };
		static string TruncateAtWord(string input, int length)
		{
		      if (input == null || input.Length < length) return input;
		    int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
		    return string.Format("{0}", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
		}
		static string FirstCharToUpper(string input)
	    {
	        switch (input)
	        {
	            default: return input.First().ToString().ToUpper() + input.Substring(1);
	        }
	    }
	}

}