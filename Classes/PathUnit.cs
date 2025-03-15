using UnityEngine;

public class PathUnit
{
    public HexCell cellFrom;
    public HexCell cellTo;

    public float totalDis;
    public float currentDis;

    public string stage = "up";

    private GameObject cylinderObject; // GameObject to hold the cylinder

    public PathUnit(HexCell cellFrom, HexCell cellTo, float totalDis)
    {
        this.cellFrom = cellFrom;
        this.cellTo = cellTo;
        this.totalDis = totalDis;
        this.currentDis = 0;
        this.stage = "up";

        InitializeCylinder(); // Initialize the cylinder when creating a PathUnit
    }

    // Method to initialize and configure the cylinder
    private void InitializeCylinder()
    {
        // Create a new GameObject for the cylinder
        cylinderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        // Calculate the midpoint between the two cells
        Vector3 midpoint = (cellFrom.coordinates.xyz_world + cellTo.coordinates.xyz_world) / 2f;
        midpoint = midpoint + new Vector3(0, 4.5f, 0);

        // Position the cylinder at the midpoint
        cylinderObject.transform.position = midpoint;

        // Calculate the direction from cellFrom to cellTo
        Vector3 direction = cellTo.coordinates.xyz_world - cellFrom.coordinates.xyz_world;

        // Rotate the cylinder to align with the direction
        cylinderObject.transform.rotation = Quaternion.LookRotation(direction);
        cylinderObject.transform.Rotate(90f, 0f, 0f); // Adjust rotation for cylinder orientation

        // Scale the cylinder to match the distance between the cells
        float distance = Vector3.Distance(cellFrom.coordinates.xyz_world, cellTo.coordinates.xyz_world);
        float thickness = 0.3f;
        cylinderObject.transform.localScale = new Vector3(thickness, distance / 2f, thickness); // Adjust thickness and length

        // Optional: Set a material for the cylinder
        Renderer cylinderRenderer = cylinderObject.GetComponent<Renderer>();
        cylinderRenderer.material = new Material(Shader.Find("Standard"));
        cylinderRenderer.material.color = Color.blue; // Set color
    }

    public void moveForward(float delta_dis)
    {
        currentDis += delta_dis;
        if (currentDis / totalDis > 50f)
        {
            stage = "down";
        }
    }

    public void setMover(playerMove mover)
    {
        Vector3 direction = cellTo.coordinates.xyz_world - cellFrom.coordinates.xyz_world;
        mover.transform.localPosition = cellFrom.coordinates.xyz_world + direction * (currentDis / totalDis);
        mover.transform.localPosition = mover.transform.localPosition + new Vector3(0, 3.7f, 0);
    }

    public override string ToString()
    {
        return $"{cellFrom}->{cellTo}";
    }

    // Optional: Method to destroy the cylinder when no longer needed
    public void DestroyCylinder()
    {
        if (cylinderObject != null)
        {
            GameObject.Destroy(cylinderObject);
        }
    }
}