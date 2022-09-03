using BepInEx;

namespace SERVPHAPI {
	[BepInPlugin("com.servph.servphapi", "SERVPH-API", "1.0.0")]
	public class APIEntry : BaseUnityPlugin 
	{
		public static APIEntry Instance { get; private set; }
		//public CustomItemHandlerManager CustomItemHandlerManager { get; private set; }
		public void Awake() {
			APIEntry.Instance = this;
			//this.CustomItemHandlerManager = new CustomItemHandlerManager();
			//this.CustomInteractionsHandlerManager = new CustomInteractionsHandlerManager();
			//new CustomItemHandlerPatch().Enable();
			//new CustomInteractionsPatch().Enable();
			//new CustomHideoutInteractionsPatch().Enable();
		}
	}
}