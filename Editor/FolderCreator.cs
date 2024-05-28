using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class FolderCreator : EditorWindow
{
    private static List<(string, int)> folderStruct = new List<(string, int)>()
        {
              ("Scenes",0),
                  ("Loading",1),
                  ("Login",1),
              ("Art",0),
                  ("Characters",1),
                  ("Effects",1),
                  ("Environments",1),
                  ("Props",1),
              ("UI",0),
                  ("Loading",1),
                  ("Login",1),
              ("Scripts",0),
              ("Localization",0),
              ("Media",0),
              ("Bundles",0),
              ("Config",0),
              ("ThirdParty",0),
              ("Prefabs",0),
              ("Temp",0),
              ("Resources",-1),
              ("StreamingAssets",-1),
              ("URP",-1),
              ("XR",-1),
              ("XRI",-1),
      };

    [MenuItem("Tools/��ʼ�������ļ�Ŀ¼�ṹ")]
    private static void Creat()
    {
        if (EditorUtility.DisplayDialog("��ʼ�������ļ�Ŀ¼�ṹ", "�Զ����ɹ����ļ�Ŀ¼�ṹ", "ȷ��", "ȡ��"))
        {
            pathStack.Clear();
            lastDepath = 0;
            headIndex = 0;
            GenerateFolderByStruct();
        }
    }

    private static void GenerateFolderByStruct()
    {
        for (int i = 0; i < folderStruct.Count; i++)
        {
            pathStack.Clear();
            string path = folderStruct[i].Item1;
            int depath = folderStruct[i].Item2;
            pathStack.Push(path);
            if (depath == 0)
            {
                headIndex++;
            }
            RecursiveFolderName(i);
            string creatPath = Application.dataPath;
            bool isFirst = true;
            while (pathStack.Count > 0)
            {
                string folderItemPath = pathStack.Pop();
                if (isFirst)
                {
                    isFirst = false;
                    //��˳�����ǰ׺ string.Format���ַ�����׼�������������ܶ�����ı�׼
                    //folderItemPath = depath >= 0 ? string.Format("{0}{1}{2}", headIndex.ToString("D2"), ".", folderItemPath) : folderItemPath;
                }
                creatPath = Path.Combine(creatPath, folderItemPath);
            }
            if (CreatFolder(creatPath))
            {
                Debug.Log("CreatFolderSucceed: " + creatPath);
            }
            else
            {
                Debug.Log("�Ѵ���ͬ���ļ���: " + creatPath);
            }
        }
        AssetDatabase.Refresh();
    }
    static Stack<string> pathStack = new Stack<string>();
    static int lastDepath;
    static int headIndex = 0;
    //���ϵݹ飬ֱ���ҵ�0Ϊֹ
    private static void RecursiveFolderName(int currentIndex)
    {
        //��ǰindex�����
        int currentDepath = folderStruct[currentIndex].Item2;
        //��¼һ��
        lastDepath = currentDepath;
        //�����ǰ�����>0��������,ֱ�����<=0Ϊֹ
        while (currentDepath > 0)
        {
            //��ǰ��index��Depath���>=��һ�μ�¼�������,��--Index
            while (folderStruct[--currentIndex].Item2 >= lastDepath)
            {

            }
            lastDepath = folderStruct[currentIndex].Item2;
            pathStack.Push(folderStruct[currentIndex].Item1);
            RecursiveFolderName(currentIndex);
            return;
        }
    }

    private static bool CreatFolder(string fullPath)
    {
        //��ѯ�Ƿ��Ѿ�����ͬ���ļ���
        if (Directory.Exists(fullPath))
            return false;
        Directory.CreateDirectory(fullPath);
        return true;
    }


    #region �ҵ�д��

/*

    static string[] assetsFolderNames = { "Scenes", "Art", "UI", "Scripts", "Localization", "Media", "Bundles", "Config", "ThirdParty", "Prefabs", "Temp", "Resources", "StreamingAssets", "URP", "XR", "XRI", "ZFramework", "Bulid" };

    static string[] scenesFolderNames = { "Loading", "Login" };

    static string[] artFolderNames = { "Characters", "Effects", "Environments", "Props" };

    static string[] uIFolderNames = { "Loading", "Login" };


    private static List<string> folderNamesList;
    [MenuItem("Tools/��ʼ���ļ���")]
    private static void CreateWindow()
    {
        InitializeFolder("Assets", assetsFolderNames);
        InitializeFolder("Assets/Scenes", scenesFolderNames);
        InitializeFolder("Assets/Art", artFolderNames);
        InitializeFolder("Assets/UI", uIFolderNames);
    }

    private static void InitializeFolder(string path, string[] folderNames)
    {
        //InitFolderNameList(folderNames);
        //��ʼ���ļ��������б�
        folderNamesList = new List<string>(folderNames);
        List<string> newFolderNameList = ExcludeTheSameFolder(path, folderNamesList);

        for (int i = 0; i < newFolderNameList.Count; i++)
        {
            AssetDatabase.CreateFolder(path, newFolderNameList[i]);
        }
        AssetDatabase.Refresh();

    }

    //��ֹ�½�ͬ���ļ���
    private static List<string> ExcludeTheSameFolder(string path, List<string> folderNames)
    {
        string[] folders = AssetDatabase.GetSubFolders(path);
        foreach (string folder in folders)
        {
            string folderName = Path.GetFileName(folder);
            for (int i = 0; i < folderNames.Count; i++)
            {
                if (folderName == folderNames[i])
                {
                    folderNames.Remove(folderNames[i]);
                }
            }
        }
        return folderNames;
    }*/
    #endregion
}