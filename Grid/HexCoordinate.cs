using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct HexCoordinates {

	[SerializeField]
	// public int x,y,z;
	public Vector3 xyz, xyz_world, xyz_world_offset;
	[SerializeField]
	// public float x_world,y_world,z_world;


	public HexCoordinates (int x, int z, float cellgap=0, float sq3=0) {
		// this.x_world = (x + z * 0.5f - z / 2) * cellgap;
		// this.y_world = 0f;
		// this.z_world = z * sq3 * cellgap / 2;
		int x_ = x - z / 2;
		int z_ = z;
		int y_ = -(x_+z_);
		this.xyz = new Vector3(x_, y_, z_);
		this.xyz_world = new Vector3((x + z * 0.5f - z / 2) * cellgap, 0f, z * sq3 * cellgap / 2);
		this.xyz_world_offset = this.xyz_world + new Vector3(0, 3.7f, 0);
	}
    public static HexCoordinates FromOffsetCoordinates (int x, int z, float cellgap, float sq3) {
        return new HexCoordinates(x, z, cellgap, sq3);
    }
    public override string ToString () {
		return "(" +
			xyz.x.ToString() + ", " + xyz.y.ToString() + ", " + xyz.z.ToString() + ")";
	}

	public string ToStringOnSeparateLines () {
		return xyz.x.ToString() + "\n" + xyz.y.ToString() + "\n" + xyz.z.ToString();
	}
}