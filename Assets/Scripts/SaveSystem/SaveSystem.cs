using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    #region//Scene
    //onde colocar os dados da cena
    private static string sceneDataPath = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".fun";


    //pega dados da cena e coloca no file
    public static void SaveScene(SceneSaveVariables scene)
    {
        //cria o file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(sceneDataPath, FileMode.Create);

        //pega dados
        SceneData data = new SceneData(scene);

        //coloca dados
        formatter.Serialize(stream, data);

        //fecha o file
        stream.Close();
    }


    //pega dados do file e devolve os dados
    public static SceneData LoadScene()
    {
        //ve se tem algo salvado
        if (!File.Exists(sceneDataPath))
        {
            Debug.LogError("File não existe no " + sceneDataPath);
            return null;
        }

        //abre o file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(sceneDataPath, FileMode.Open);

        //le dados
        SceneData data = formatter.Deserialize(stream) as SceneData;

        //fecha o file
        stream.Close();

        //devolve dados
        return data;
    }
    #endregion
}
