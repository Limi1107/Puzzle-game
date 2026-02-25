using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject blockPrefab; // InspectorにBlockプレハブをセット

    void Awake() => Instance = this;
    void Start() => SpawnPair();

    public void SpawnPair()
    {
        GameObject pair = new GameObject("BlockPair");
        pair.AddComponent<BlockPair>();

        int colorA = Random.Range(1, 8);
        int colorB = Random.Range(1, 8);

        GameObject bottom = Instantiate(blockPrefab, pair.transform);
        GameObject top = Instantiate(blockPrefab, pair.transform);

        bottom.GetComponent<Block>().colorId = colorA;
        top.GetComponent<Block>().colorId = colorB;

        // SpriteRenderer の色も差し替え
        bottom.GetComponent<SpriteRenderer>().sprite = GetSpriteByColor(colorA);
        top.GetComponent<SpriteRenderer>().sprite = GetSpriteByColor(colorB);

        // 縦2連に配置
        bottom.transform.localPosition = Vector2.zero;
        top.transform.localPosition = new Vector2(0, 1);

        // 盤面上部中央に出現
        pair.transform.position = new Vector2(1, 11);
    }

    Sprite GetSpriteByColor(int colorId)
    {
        // 仮：Inspectorで配列に7色スプライトをセット
        return colorSprites[colorId - 1];
    }

    public Sprite[] colorSprites; // Inspectorで7色素材をセット
}