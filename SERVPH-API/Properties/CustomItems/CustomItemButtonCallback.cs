using System;
using EFT.InventoryLogic;
using EFT.UI;

namespace SERVPHAPI.Properties.CustomItems {
	[AttributeUsage(AttributeTargets.Method)]
	public class CustomItemButtonCallback : Attribute {
		public readonly EItemUiContextType contextType;
		public readonly EItemInfoButton itemInfoButton;
		
		public CustomItemButtonCallback(EItemInfoButton itemInfoButton, EItemUiContextType contextType = EItemUiContextType.InventoryScreen)
		{
			this.itemInfoButton = itemInfoButton;
			this.contextType = contextType;
		}
		
	}
}