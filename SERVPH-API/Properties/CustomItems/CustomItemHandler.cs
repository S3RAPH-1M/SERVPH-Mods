using System;
using System.Collections.Generic;
using System.Reflection;
using EFT.InventoryLogic;
using EFT.UI;
using SERVPHAPI.Properties.CustomItems;
using UnityEngine;
using static EFT.UI.SessionEnd.SessionResultStatistics;

namespace SERVPH_API.Properties.CustomItems {
	public abstract class CustomItemHandler {
		
		public abstract string[] TemplateIds { get; }
		public virtual IReadOnlyList<EItemInfoButton> AvailableInteractions { get; }
		public virtual Dictionary<EItemInfoButton, string> CustomNames { get; }
		
		protected GClass2100 controller;
		protected object context;
		private static ValueTuple<bool, string> _defaultInteractiveResult = new ValueTuple<bool, string>(true, string.Empty);
		
		public Item GetItem()
		{
			object obj = this.context;
			object obj2 = obj;
			Item result;
			if (obj2 != null)
			{
				Item item = obj2 as Item;
				if (item == null)
				{
					FieldInfo field = this.context.GetType().GetField("item_0", BindingFlags.Instance | BindingFlags.NonPublic);
					result = (((field != null) ? field.GetValue(this.context) : null) as Item);
				}
				else
				{
					result = item;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}
		
		public virtual ValueTuple<bool, string> IsInteractive(GClass2417 instance, EItemInfoButton button, EItemUiContextType contextType)
		{
			return CustomItemHandler._defaultInteractiveResult;
		}
		
		public virtual bool IsActive(GClass2417 instance, EItemInfoButton button)
		{
			return true;
		}
		
		public virtual Sprite GetIcon(GClass2417 instance, EItemInfoButton button)
		{
			return null;
		}
		/*
		public virtual List<GClass2293<EItemInfoButton>> GetSubInteractions(EItemInfoButton button)
		{
			return null;
		}
		*/
		public void SetContext(object context, GClass2100 controller)
		{
			this.context = context;
			this.controller = controller;
		}
		
		public static Dictionary<EItemInfoButton, Action> GenerateCallbacks(CustomItemHandler handler, ItemUiContext itemUiContext)
		{
			MethodInfo[] methods = handler.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
			Dictionary<EItemInfoButton, Action> dictionary = new Dictionary<EItemInfoButton, Action>();
			foreach (MethodInfo methodInfo in methods)
			{
				CustomItemButtonCallback customAttribute = methodInfo.GetCustomAttribute<CustomItemButtonCallback>();
				bool flag = customAttribute != null && customAttribute.contextType == itemUiContext.ContextType && !dictionary.ContainsKey(customAttribute.itemInfoButton);
				if (flag)
				{
					dictionary.Add(customAttribute.itemInfoButton, (Action)Delegate.CreateDelegate(typeof(Action), handler, methodInfo, true));
				}
			}
			return dictionary;
		}
		
		
	}
}