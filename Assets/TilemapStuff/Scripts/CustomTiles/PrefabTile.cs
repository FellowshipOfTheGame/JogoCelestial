using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/PrefabTile")]
public class PrefabTile : TileBase
{
    public GameObject tilePrefab;
    public Sprite tileSprite;
    public Tile.ColliderType tileCollisionType;

    public List<Node> objNode;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject gameObject)
    {
        if (gameObject is null)
            return false;
        
        Matrix4x4 matrix = tilemap.GetTransformMatrix(position);
        gameObject.transform.position += matrix.ExtractPosition();
        gameObject.transform.rotation = matrix.ExtractRotation();
        gameObject.transform.localScale = matrix.ExtractScale();

        return true;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = tileSprite;
        tileData.gameObject = tilePrefab;
        tileData.colliderType = tileCollisionType;
    }
}

[System.Serializable]
public class Node
{
    public string nome;
}
