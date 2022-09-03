using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.Ballistics;
using UnityEngine;

namespace VisceralRagdolls {
	public class BodyPatch : ModulePatch {
		private static String[] TargetBones = {"thigh", "calf", "foot", "spine3", "forearm", "head"};

		protected override MethodBase GetTargetMethod() {
			return typeof(Player).GetMethod("CreateCorpse", BindingFlags.NonPublic | BindingFlags.Instance, null, Array.Empty<Type>(), null);
		}

		[PatchPostfix]
		private static void Postfix(Player __instance) {
			if (__instance.IsYourPlayer) {
				return;
			}
			foreach (Transform child in EnumerateHierarchyCore(__instance.Transform.Original).Where(t => TargetBones.Any(u => t.name.ToLower().Contains(u)))) {
				child.gameObject.layer = 6;
			}
		}
		
		private static IEnumerable<Transform> EnumerateHierarchyCore(Transform root) {
			Queue<Transform> transformQueue = new Queue<Transform>();
			transformQueue.Enqueue(root);

			while (transformQueue.Count > 0) {
				Transform parentTransform = transformQueue.Dequeue();

				if (!parentTransform) {
					continue;
				}

				for (Int32 i = 0; i < parentTransform.childCount; i++) {
					transformQueue.Enqueue(parentTransform.GetChild(i));
				}

				yield return parentTransform;
			}
		}
	}
}