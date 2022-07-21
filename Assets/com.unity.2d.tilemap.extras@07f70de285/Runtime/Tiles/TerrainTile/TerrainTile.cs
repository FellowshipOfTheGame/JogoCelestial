
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    /// <summary>
    /// Terrain Tiles, similar to Pipeline Tiles, are tiles which take into consideration its orthogonal and diagonal neighboring tiles and displays a sprite depending on whether the neighboring tile is the same tile.
    /// </summary>
    [Serializable]
    public class TerrainTile : TileBase
    {
        /// <summary>
        /// The Sprites used for defining the Terrain.
        /// </summary>
        [SerializeField]
        public Sprite[] m_Sprites;

        /// <summary>
        /// This method is called when the tile is refreshed.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            for (int yd = -1; yd <= 1; yd++)
                for (int xd = -1; xd <= 1; xd++)
                {
                    Vector3Int pos = new Vector3Int(position.x + xd, position.y + yd, position.z);
                    if (TileValue(tilemap, pos))
                        tilemap.RefreshTile(pos);
                }
        }

        /// <summary>
        /// Retrieves any tile rendering data from the scripted tile.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        /// <param name="tileData">Data to render the tile.</param>
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            UpdateTile(position, tilemap, ref tileData);
        }

        private void UpdateTile(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;

            bool[] mask = new bool[8];

            mask[0] = TileValue(tileMap, location + new Vector3Int(0, 1, 0)); // Above
            mask[1] = TileValue(tileMap, location + new Vector3Int(1, 1, 0)); // Above-Right
            mask[2] = TileValue(tileMap, location + new Vector3Int(1, 0, 0)); // Right
            mask[3] = TileValue(tileMap, location + new Vector3Int(1, -1, 0)); // Under-Right
            mask[4] = TileValue(tileMap, location + new Vector3Int(0, -1, 0)); // Under
            mask[5] = TileValue(tileMap, location + new Vector3Int(-1, -1, 0)); // Under-Left
            mask[6] = TileValue(tileMap, location + new Vector3Int(-1, 0, 0)); // Left
            mask[7] = TileValue(tileMap, location + new Vector3Int(-1, 1, 0)); // Above-Left

            // byte original = (byte)mask;
            // if ((original | 254) < 255) { mask = mask & 125; }
            // if ((original | 251) < 255) { mask = mask & 245; }
            // if ((original | 239) < 255) { mask = mask & 215; }
            // if ((original | 191) < 255) { mask = mask & 95; }

            int index = GetIndex(mask);
            if (index >= 0 && index < m_Sprites.Length && TileValue(tileMap, location))
            {
                tileData.sprite = m_Sprites[index];
                tileData.transform = GetTransform(mask);
                tileData.color = Color.white;
                tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
                tileData.colliderType = Tile.ColliderType.Sprite;
            }
        }

        private bool TileValue(ITilemap tileMap, Vector3Int position)
        {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

        private int GetIndex(bool[] mask)
        {
            if (!mask[0] && !mask[2] && !mask[4] && !mask[6]) { return 0; }
            if (!mask[0] && mask[2] && mask[4] && !mask[6] && mask[3]) { return 1; }
            if (!mask[0] && mask[2] && mask[4] && mask[6] && mask[3] && mask[5]) { return 2; }
            if (!mask[0] && !mask[2] && mask[4] && mask[6] && mask[5]) { return 3; }
            if (mask[0] && mask[2] && mask[4] && !mask[6] && mask[1] && mask[3]) { return 4; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && mask[3] && mask[5] && mask[7]) { return 5; }
            if (mask[0] && !mask[2] && mask[4] && mask[6] && mask[5] && mask[7]) { return 6; }
            if (mask[0] && mask[2] && !mask[4] && !mask[6] && mask[1]) { return 7; }
            if (mask[0] && mask[2] && !mask[4] && mask[6] && mask[1] && mask[7]) { return 8; }
            if (mask[0] && !mask[2] && !mask[4] && mask[6] && mask[7]) { return 9; }
            if (!mask[0] && !mask[2] && mask[4] && !mask[6]) { return 10; }
            if (mask[0] && !mask[2] && mask[4] && !mask[6]) { return 11; }
            if (mask[0] && !mask[2] && !mask[4] && !mask[6]) { return 12; }
            if (!mask[0] && mask[2] && !mask[4] && !mask[6]) { return 13; }
            if (!mask[0] && mask[2] && !mask[4] && mask[6]) { return 14; }
            if (!mask[0] && !mask[2] && !mask[4] && mask[6]) { return 15; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && !mask[3] && mask[5] && mask[7]) { return 16; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && mask[3] && !mask[5] && mask[7]) { return 17; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && mask[3] && mask[5] && mask[7]) { return 18; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && mask[3] && mask[5] && !mask[7]) { return 19; }
            if (!mask[0] && mask[2] && mask[4] && mask[6] && !mask[3] && mask[5]) { return 20; }
            if (!mask[0] && mask[2] && mask[4] && mask[6] && mask[3] && !mask[5]) { return 21; }
            if (mask[0] && mask[2] && mask[4] && !mask[6] && mask[1] && !mask[3]) { return 22; }
            if (mask[0] && !mask[2] && mask[4] && mask[6] && !mask[5] && mask[7]) { return 23; }
            if (mask[0] && mask[2] && mask[4] && !mask[6] && !mask[1] && mask[3]) { return 24; }
            if (mask[0] && !mask[2] && mask[4] && mask[6] && mask[5] && !mask[7]) { return 25; }
            if (mask[0] && mask[2] && !mask[4] && mask[6] && !mask[1] && mask[7]) { return 26; }
            if (mask[0] && mask[2] && !mask[4] && mask[6] && mask[1] && !mask[7]) { return 27; }
            if (!mask[0] && mask[2] && mask[4] && !mask[6] && !mask[3]) { return 28; }
            if (!mask[0] && !mask[2] && mask[4] && mask[6] && !mask[5]) { return 29; }
            if (mask[0] && mask[2] && !mask[4] && !mask[6] && !mask[1]) { return 30; }
            if (mask[0] && !mask[2] && !mask[4] && mask[6] && !mask[7]) { return 31; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && !mask[3] && !mask[5] && mask[7]) { return 32; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && !mask[3] && mask[5] && mask[7]) { return 33; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && mask[3] && !mask[5] && !mask[7]) { return 34; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && mask[3] && mask[5] && !mask[7]) { return 35; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && mask[3] && !mask[5] && mask[7]) { return 36; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && !mask[3] && mask[5] && !mask[7]) { return 37; }
            if (!mask[0] && mask[2] && mask[4] && mask[6] && !mask[3] && !mask[5]) { return 38; }
            if (mask[0] && mask[2] && mask[4] && !mask[6] && !mask[1] && !mask[3]) { return 39; }
            if (mask[0] && !mask[2] && mask[4] && mask[6] && !mask[5] && !mask[7]) { return 40; }
            if (mask[0] && mask[2] && !mask[4] && mask[6] && !mask[1] && !mask[7]) { return 41; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && !mask[3] && !mask[5] && mask[7]) { return 42; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && mask[1] && !mask[3] && !mask[5] && !mask[7]) { return 43; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && !mask[3] && mask[5] && !mask[7]) { return 44; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && mask[3] && !mask[5] && !mask[7]) { return 45; }
            if (mask[0] && mask[2] && mask[4] && mask[6] && !mask[1] && !mask[3] && !mask[5] && !mask[7]) { return 46; }

            return -1;
        }

        private Matrix4x4 GetTransform(bool[] mask)
        {
            return Matrix4x4.identity;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TerrainTile))]
    public class TerrainTileEditor : Editor
    {
        private TerrainTile tile { get { return (target as TerrainTile); } }

        /// <summary>
        /// OnEnable for TerrainTile.
        /// </summary>
        public void OnEnable()
        {
            if (tile.m_Sprites == null || tile.m_Sprites.Length != 47)
            {
                tile.m_Sprites = new Sprite[47];
                EditorUtility.SetDirty(tile);
            }
        }

        /// <summary>
        /// Draws an Inspector for the Terrain Tile.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Place sprites shown based on the contents of the sprite.");
            EditorGUILayout.Space();

            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 210;

            EditorGUI.BeginChangeCheck();
            tile.m_Sprites[0] = (Sprite) EditorGUILayout.ObjectField("Single", tile.m_Sprites[0], typeof(Sprite), false, null);
            tile.m_Sprites[1] = (Sprite) EditorGUILayout.ObjectField("Main - Top Left", tile.m_Sprites[1], typeof(Sprite), false, null);
            tile.m_Sprites[2] = (Sprite) EditorGUILayout.ObjectField("Main - Top", tile.m_Sprites[2], typeof(Sprite), false, null);
            tile.m_Sprites[3] = (Sprite) EditorGUILayout.ObjectField("Main - Top Right", tile.m_Sprites[3], typeof(Sprite), false, null);
            tile.m_Sprites[4] = (Sprite) EditorGUILayout.ObjectField("Main - Left", tile.m_Sprites[4], typeof(Sprite), false, null);
            tile.m_Sprites[5] = (Sprite) EditorGUILayout.ObjectField("Main - Center", tile.m_Sprites[5], typeof(Sprite), false, null);
            tile.m_Sprites[6] = (Sprite) EditorGUILayout.ObjectField("Main - Right", tile.m_Sprites[6], typeof(Sprite), false, null);
            tile.m_Sprites[7] = (Sprite) EditorGUILayout.ObjectField("Main - Down Left", tile.m_Sprites[7], typeof(Sprite), false, null);
            tile.m_Sprites[8] = (Sprite) EditorGUILayout.ObjectField("Main - Down", tile.m_Sprites[8], typeof(Sprite), false, null);
            tile.m_Sprites[9] = (Sprite) EditorGUILayout.ObjectField("Main - Down Right", tile.m_Sprites[9], typeof(Sprite), false, null);
            tile.m_Sprites[10] = (Sprite) EditorGUILayout.ObjectField("Column - Top", tile.m_Sprites[10], typeof(Sprite), false, null);
            tile.m_Sprites[11] = (Sprite) EditorGUILayout.ObjectField("Column - Center", tile.m_Sprites[11], typeof(Sprite), false, null);
            tile.m_Sprites[12] = (Sprite) EditorGUILayout.ObjectField("Column - Down", tile.m_Sprites[12], typeof(Sprite), false, null);
            tile.m_Sprites[13] = (Sprite) EditorGUILayout.ObjectField("Line - Left", tile.m_Sprites[13], typeof(Sprite), false, null);
            tile.m_Sprites[14] = (Sprite) EditorGUILayout.ObjectField("Line - Center", tile.m_Sprites[14], typeof(Sprite), false, null);
            tile.m_Sprites[15] = (Sprite) EditorGUILayout.ObjectField("Line - Right", tile.m_Sprites[15], typeof(Sprite), false, null);
            tile.m_Sprites[16] = (Sprite) EditorGUILayout.ObjectField("One Corner - Top Left", tile.m_Sprites[16], typeof(Sprite), false, null);
            tile.m_Sprites[17] = (Sprite) EditorGUILayout.ObjectField("One Corner - Top Right", tile.m_Sprites[17], typeof(Sprite), false, null);
            tile.m_Sprites[18] = (Sprite) EditorGUILayout.ObjectField("One Corner - Down Left", tile.m_Sprites[18], typeof(Sprite), false, null);
            tile.m_Sprites[19] = (Sprite) EditorGUILayout.ObjectField("One Corner - Down Right", tile.m_Sprites[19], typeof(Sprite), false, null);
            tile.m_Sprites[20] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Top Left Top", tile.m_Sprites[20], typeof(Sprite), false, null);
            tile.m_Sprites[21] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Top Right Top", tile.m_Sprites[21], typeof(Sprite), false, null);
            tile.m_Sprites[22] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Top Left Left", tile.m_Sprites[22], typeof(Sprite), false, null);
            tile.m_Sprites[23] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Top Right Right", tile.m_Sprites[23], typeof(Sprite), false, null);
            tile.m_Sprites[24] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Down Left Left", tile.m_Sprites[24], typeof(Sprite), false, null);
            tile.m_Sprites[25] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Down Right Right", tile.m_Sprites[25], typeof(Sprite), false, null);
            tile.m_Sprites[26] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Down Left Down", tile.m_Sprites[26], typeof(Sprite), false, null);
            tile.m_Sprites[27] = (Sprite) EditorGUILayout.ObjectField("One Corner One Side - Down Right Down", tile.m_Sprites[27], typeof(Sprite), false, null);
            tile.m_Sprites[28] = (Sprite) EditorGUILayout.ObjectField("One Corner Two Sides - Top Left", tile.m_Sprites[28], typeof(Sprite), false, null);
            tile.m_Sprites[29] = (Sprite) EditorGUILayout.ObjectField("One Corner Two Sides - Top Right", tile.m_Sprites[29], typeof(Sprite), false, null);
            tile.m_Sprites[30] = (Sprite) EditorGUILayout.ObjectField("One Corner Two Sides - Down Left", tile.m_Sprites[30], typeof(Sprite), false, null);
            tile.m_Sprites[31] = (Sprite) EditorGUILayout.ObjectField("One Corner Two Sides - Down Right", tile.m_Sprites[31], typeof(Sprite), false, null);
            tile.m_Sprites[32] = (Sprite) EditorGUILayout.ObjectField("Two Corners - Top", tile.m_Sprites[32], typeof(Sprite), false, null);
            tile.m_Sprites[33] = (Sprite) EditorGUILayout.ObjectField("Two Corners - Left", tile.m_Sprites[33], typeof(Sprite), false, null);
            tile.m_Sprites[34] = (Sprite) EditorGUILayout.ObjectField("Two Corners - Right", tile.m_Sprites[34], typeof(Sprite), false, null);
            tile.m_Sprites[35] = (Sprite) EditorGUILayout.ObjectField("Two Corners - Down", tile.m_Sprites[35], typeof(Sprite), false, null);
            tile.m_Sprites[36] = (Sprite) EditorGUILayout.ObjectField("Two Corners - DiagUp", tile.m_Sprites[36], typeof(Sprite), false, null);
            tile.m_Sprites[37] = (Sprite) EditorGUILayout.ObjectField("Two Corners - DiagDown", tile.m_Sprites[37], typeof(Sprite), false, null);
            tile.m_Sprites[38] = (Sprite) EditorGUILayout.ObjectField("Two Corners One Side - Top", tile.m_Sprites[38], typeof(Sprite), false, null);
            tile.m_Sprites[39] = (Sprite) EditorGUILayout.ObjectField("Two Corners One Side - Left", tile.m_Sprites[39], typeof(Sprite), false, null);
            tile.m_Sprites[40] = (Sprite) EditorGUILayout.ObjectField("Two Corners One Side - Right", tile.m_Sprites[40], typeof(Sprite), false, null);
            tile.m_Sprites[41] = (Sprite) EditorGUILayout.ObjectField("Two Corners One Side - Down", tile.m_Sprites[41], typeof(Sprite), false, null);
            tile.m_Sprites[42] = (Sprite) EditorGUILayout.ObjectField("Three Corners - Top Left", tile.m_Sprites[42], typeof(Sprite), false, null);
            tile.m_Sprites[43] = (Sprite) EditorGUILayout.ObjectField("Three Corners - Top Right", tile.m_Sprites[43], typeof(Sprite), false, null);
            tile.m_Sprites[44] = (Sprite) EditorGUILayout.ObjectField("Three Corners - Down Left", tile.m_Sprites[44], typeof(Sprite), false, null);
            tile.m_Sprites[45] = (Sprite) EditorGUILayout.ObjectField("Three Corners - Down Right", tile.m_Sprites[45], typeof(Sprite), false, null);
            tile.m_Sprites[46] = (Sprite) EditorGUILayout.ObjectField("Four Corners", tile.m_Sprites[46], typeof(Sprite), false, null);

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
#endif
}