using UnityEngine;

public class MaterialSelector : MonoBehaviour {

	[SerializeField]
	Material[] materials = default;
    [SerializeField]
    int matIndex = 0;
	[SerializeField]
	MeshRenderer meshRenderer = default;

	public void Select (int index) {
		if (
			meshRenderer && materials != null &&
			index >= 0 && index < materials.Length
		) {
            Material[] mats = meshRenderer.materials;
            mats[matIndex] = materials[index];
            meshRenderer.materials = mats;

        }
	}
}