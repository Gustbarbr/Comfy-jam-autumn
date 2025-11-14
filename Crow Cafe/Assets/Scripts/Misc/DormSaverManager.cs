using System.Collections.Generic;
using UnityEngine;

public class DormSaveManager : MonoBehaviour
{
    public static DormSaveManager Instance;

    public List<DecorationItem> allItems;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadDecorations();
    }

    [System.Serializable]
    public class SavedDecoration
    {
        public string itemName;
        public float x, y, z;
        public float rot;
        public float scaleX, scaleY;
    }

    [System.Serializable]
    public class SaveWrapper
    {
        public List<SavedDecoration> objects = new List<SavedDecoration>();
    }

    public void SaveDecorations()
    {
        var saveData = new SaveWrapper();

        GameObject[] decos = GameObject.FindGameObjectsWithTag("Decoration");

        foreach (var deco in decos)
        {
            DecorationObject decoObj = deco.GetComponent<DecorationObject>();
            if (decoObj == null) continue;

            var data = new SavedDecoration();

            data.itemName = decoObj.item.itemName;

            data.x = deco.transform.position.x;
            data.y = deco.transform.position.y;
            data.z = deco.transform.position.z;

            data.rot = deco.transform.eulerAngles.z;

            data.scaleX = deco.transform.localScale.x;
            data.scaleY = deco.transform.localScale.y;

            saveData.objects.Add(data);
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("DormDecorations", json);
        PlayerPrefs.Save();
    }

    public void LoadDecorations()
    {
        if (!PlayerPrefs.HasKey("DormDecorations")) return;

        string json = PlayerPrefs.GetString("DormDecorations");
        SaveWrapper data = JsonUtility.FromJson<SaveWrapper>(json);

        foreach (var obj in data.objects)
        {
            DecorationItem item = allItems.Find(i => i.itemName == obj.itemName);
            if (item == null)
            {
                Debug.LogWarning("Item não encontrado: " + obj.itemName);
                continue;
            }

            GameObject newObj = Instantiate(item.prefab);

            newObj.transform.position = new Vector3(obj.x, obj.y, obj.z);
            newObj.transform.eulerAngles = new Vector3(0, 0, obj.rot);
            newObj.transform.localScale = new Vector3(obj.scaleX, obj.scaleY, 1);

            newObj.tag = "Decoration";

            var decoComp = newObj.AddComponent<DecorationObject>();
            decoComp.item = item;
        }
    }
}
