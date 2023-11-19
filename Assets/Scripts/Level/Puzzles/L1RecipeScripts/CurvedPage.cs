using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CurvedPage : MonoBehaviour {
    private float _curveHeight = 0.2f;
    private float _sineWaveStartPercent = 0;
    private float _sineWaveEndPercent = 0.5f;

    private void CreateCurvedPage() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Mesh meshCurvePage = new Mesh();
        meshCurvePage = mesh;
        meshCurvePage.name = "CurvePage";
        Vector3[] vertices = meshCurvePage.vertices;

        // Map the sine wave range from a scale of [0,1] to a scale of [0,2pi]
        float sineRange = (_sineWaveEndPercent - _sineWaveStartPercent) * Mathf.PI * 2;
        float sineOffset = _sineWaveStartPercent * Mathf.PI * 2;

        for (int i = 0; i < vertices.Length; i++) {
            // Determine the percentage position of the vertex wrt the bounding box
            float xPosPercent = (vertices[i].x - meshCurvePage.bounds.min.x) / meshCurvePage.bounds.size.x;

            // Map the percentage position to the sine wave range
            float sinePercent = xPosPercent * sineRange + sineOffset;

            // Set the height of the vertex based on the point on the sine wave
            vertices[i].y += Mathf.Sin(sinePercent) * _curveHeight;
        }

        meshCurvePage.vertices = vertices;
        mesh = meshCurvePage;
    }
    
    private void Start() {
        CreateCurvedPage();
    }
}
