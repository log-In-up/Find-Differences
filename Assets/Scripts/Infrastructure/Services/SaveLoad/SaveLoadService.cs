using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SAVES_FOLDER = "Saves";
        private readonly string _dataDirPath, _dataFileName, _ecriptionCodeWord;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly bool _useIncription;

        public SaveLoadService(IPersistentProgressService persistentProgressService,
            string dataDirPath,
            string dataFileName,
            string ecriptionCodeWord)
        {
            _persistentProgressService = persistentProgressService;

            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;

            _useIncription = !string.IsNullOrEmpty(ecriptionCodeWord);
            _ecriptionCodeWord = ecriptionCodeWord;
        }

        public Task Initialize()
        {
            return Task.Run(() =>
            {
                string savesDirectory = Path.Combine(_dataDirPath, SAVES_FOLDER);

                if (!Directory.Exists(savesDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(savesDirectory);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError($"Error occured when trying to create save directory.\n{exception}");
                    }
                }
            });
        }

        public Task<GameData> Load()
        {
            return Task.Run(() =>
            {
                string fullPath = Path.Combine(_dataDirPath, SAVES_FOLDER, _dataFileName);
                GameData loadedData = null;

                if (File.Exists(fullPath))
                {
                    try
                    {
                        string dataToLoad = string.Empty;

                        using (FileStream stream = new(fullPath, FileMode.Open))
                        {
                            using StreamReader reader = new(stream);
                            dataToLoad = reader.ReadToEnd();
                        }

                        dataToLoad = ApplyIncription(dataToLoad);

                        loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError($"Error occured when trying to load data from file: {fullPath}.\n{exception}");
                    }
                }

                return loadedData;
            });
        }

        public Task Save()
        {
            return Task.Run(() =>
            {
                string fullPath = Path.Combine(_dataDirPath, SAVES_FOLDER, _dataFileName);
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                    string dataToStore = JsonUtility.ToJson(_persistentProgressService.CurrentGameData, true);
                    dataToStore = ApplyIncription(dataToStore);

                    WriteDataToFile(fullPath, dataToStore);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Error occured when trying to save data to file: {fullPath}.\n{exception}");
                }
            });
        }

        private string ApplyIncription(string dataToIncription)
        {
            if (_useIncription)
            {
                dataToIncription = EncryptDecrypt(dataToIncription);
            }

            return dataToIncription;
        }

        private string EncryptDecrypt(string data)
        {
            string modifiedData = string.Empty;

            for (int index = 0; index < data.Length; index++)
            {
                modifiedData += (char)(data[index] ^ _ecriptionCodeWord[index % _ecriptionCodeWord.Length]);
            }

            return modifiedData;
        }

        private void WriteDataToFile(string fullPath, string dataToStore)
        {
            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
    }
}